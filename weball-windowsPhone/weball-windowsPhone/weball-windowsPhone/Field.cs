using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weball_windowsPhone
{
    public class Field
    {

        public string _id { get; set; }
        public bool available { get; set; }
        public string five { get; set; }
        public string name { get; set; }
        public class Timing
        {
            [JsonProperty(PropertyName = "9")]
            public int morning { get; set; }
            [JsonProperty(PropertyName = "12")]
            public int lunch { get; set; }
            [JsonProperty(PropertyName = "14")]
            public int afternoon { get; set; }
            [JsonProperty(PropertyName = "23")]
            public int evening { get; set; }
        }
        public class Schedule
        {
            [JsonProperty(PropertyName = "0")]
            public Timing monday { get; set; }
            [JsonProperty(PropertyName = "1")]
            public Timing tuesday { get; set; }
            [JsonProperty(PropertyName = "2")]
            public Timing wednesday { get; set; }
            [JsonProperty(PropertyName = "3")]
            public Timing thursday { get; set; }
            [JsonProperty(PropertyName = "4")]
            public Timing friday { get; set; }
            [JsonProperty(PropertyName = "5")]
            public Timing saturday { get; set; }
            [JsonProperty(PropertyName = "6")]
            public Timing sunday { get; set; }
        }
        public Schedule pricePerHour { get; set; }
    }
}
