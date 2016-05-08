using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weball_windowsPhone
{
    public class Teams
    {
        public string _id { get; set; }
        public string name { get; set; }
        public string match { get; set; }
        public DateTime date { get; set; }
        public string __v { get; set; }
        public string status { get; set; }
        public string result { get; set; }
        public int buts { get; set; }
        public int currentPlayers { get; set; }
        public class Player
        {
            public string uid { get; set; }
            public DateTime date { get; set; }
            public string _id { get; set; }
        }
        public List<string> users { get; set; }
    }
}
