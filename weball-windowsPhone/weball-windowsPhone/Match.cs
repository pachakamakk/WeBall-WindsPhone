using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weball_windowsPhone
{
    public class Match
    {
        public string _id { get; set; }
        public string name { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public int maxPlayers { get; set; }
        public string field { get; set; }
        public string five { get; set; }
        public int amount { get; set; }
        public DateTime createdAt { get; set; }
        public List<Teams> teams { get; set; }
        public string createdBy { get; set; }
        public string createdWith { get; set; }
        public int currentPlayers { get; set; }
        public string __v { get; set; }
        public string status { get; set; }
    }
}
