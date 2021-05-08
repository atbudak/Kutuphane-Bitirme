using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace MvcKutuphane.Models
{
    class Employee
    {
        private string ConnectionId;

        public Dictionary<string, string> users;
        public Employee(string connectionId)
        {
            this.ConnectionId = connectionId;
            users = new Dictionary<string, string>(3);
        }

        internal void AddUser(KeyValuePair<string, string> keyValuePair, dynamic clients)
        {
            users.Add(keyValuePair.Key, keyValuePair.Value);
            clients.Client(ConnectionId).GetUser(keyValuePair.Value);
            clients.Client(keyValuePair.Key).GetMessage("Merhaba size nasıl yardımcı olabilirim ?");
        }

        internal void CutOffConnections(dynamic clients)
        {
            foreach (var item in users)
            {
                clients.Client(item.Key).HasLostConnection();
            }
        }
    }

    public class ChatHub : Hub
    {
        static Queue<KeyValuePair<string, string>> users = new Queue<KeyValuePair<string, string>>();
        static Dictionary<string, Employee> employers = new Dictionary<string, Employee>();


        //-->>>>> ***** Receive Request From Client [  Connect  ] *****
        [AllowAnonymous]
        public void Connect(string name, string email)
        {
            users.Enqueue(new KeyValuePair<string, string>(Context.ConnectionId, email));

            if (employers.Count == 0)
            {
                Clients.Caller.NoExistEmployee();
            }
            else
            {
                foreach (var emp in employers)
                {
                    if (emp.Value.users.Count < 3)
                    {
                        emp.Value.AddUser(users.Dequeue(), Clients);

                        break;
                    }
                }
            }
        }

        [System.Web.Mvc.Authorize(Roles ="Yetkili")]
        public void Connect(string emailOfEmployee)
        {
            Employee emp = new Employee(Context.ConnectionId);
            employers.Add(Context.ConnectionId, emp);

            while (emp.users.Count != 3 && users.Count > 0)
            {
                emp.AddUser(users.Dequeue(), Clients);
            }
        }

        public void SendMessageToEmployee(string text)
        {
            foreach (var emp in employers)
            {
                foreach (var usr in emp.Value.users)
                {
                    if (usr.Key == Context.ConnectionId)
                    {
                        Clients.Client(emp.Key).GetMessageFromUser(usr.Value, text);
                    }
                }
            }
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            if (employers.Count > 0 && employers.Where(x => x.Key == Context.ConnectionId).Any())
            {
                var emp =  employers.Where(x => x.Key == Context.ConnectionId).First();

                emp.Value.CutOffConnections(Clients);

                employers.Remove(emp.Key);
            }
            else //if (users.Count > 0 && users.Where(x => x.Key == Context.ConnectionId).Any())
            {

                foreach (var emp in employers)
                {
                    foreach (var usr in emp.Value.users)
                    {
                        if (usr.Key == Context.ConnectionId)
                        {
                            Clients.Client(emp.Key).CloseUserChat(usr.Value);
                            emp.Value.users.Remove(usr.Key);

                            if (emp.Value.users.Count != 3 || users.Count != 0)
                            {
                                emp.Value.AddUser(users.Dequeue(), Clients);
                            }
                            break;
                        }
                    }
                }

                var user = users.Where(x => x.Key == Context.ConnectionId).First();

                if (!String.IsNullOrEmpty(user.Key))
                {
                    users = new Queue<KeyValuePair<string, string>>(users.Where(x => x.Key != user.Key));
                }


            }
            return base.OnDisconnected(stopCalled);
        }

    }
}