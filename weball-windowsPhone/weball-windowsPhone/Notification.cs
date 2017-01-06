using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weball_windowsPhone
{
    public class littleUser
    {
        public string _id { get; set; }
        public string fullName { get; set; }
        public string photo { get; set; }
    }
    public class Notification
    {
        public string _id { get; set; }
        public string type { get; set; }
        public string content { get; set; }
        public littleUser from { get; set; }
        public string date { get; set; }
    }
    public class Request
    {
        public littleUser user { get; set; }
        public string _id { get; set; }
        public string date { get; set; }
    }

    public class NotifHandler
    {
        public List<Notification> notifications { get; set; }
        public List<Request> requests { get; set; }
    }
}
