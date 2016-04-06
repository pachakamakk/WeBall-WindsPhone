using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weball_windowsPhone
{
    public class Five
    {
        public int _v { get; set;}
        public string siren { get; set; }
        public string name { get; set; }
        public string zipCode { get; set; }
        public string country { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string _id { get; set; }
        public string city { get; set; }
        public string date { get; set; }
        public float[] gps { get; set; }
        public class Photo
        {
            public string url {get; set;}
            public string x480Base64 {get; set;}
        }
        public Photo photo {get; set;}
        public List<string> managers { get; set; }
        public List<Field> fields { get; set; }
        public int nTotalMatchs { get; set; }
        public int nWaitingMatchs { get; set; }
        public List<Match> matchs { get; set; }
        public Five(int _v, string siren, string name, string zipCode, string country,
                    string address, string phone, string _id, List<string> managers,
                    List<Field> fields, float[] gps, string city, Photo photo)
        {
            this._v = _v;
            this.siren = siren;
            this.name = name;
            this.zipCode = zipCode;
            this.country = country;
            this.address = address;
            this.phone = phone;
            this._id = _id;
            this.managers = managers;
            this.fields = fields;
            this.gps = gps;
            this.city = city;
            this.photo = photo;
        }

        public override string ToString()
        {
            return this.name + "\n" + this.city;
        }
    }
}
