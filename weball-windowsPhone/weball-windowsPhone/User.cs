﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weball_windowsPhone
{
    public class User
    {
        public string email { get; private set; }
        public string password { get; set; }
        public string fullName { get; private set; }
        public string birthday { get; private set; }
        public string photo { get; set; }
        public class statMatch
        {
            public int win { get; set; }
            public int loose { get; set; }
            public int nul { get; set; }
            public int total { get; set; }
        }
        public class relations
        {
            public int nRelations { get; set; }
            public int nRequests { get; set; }
        }
        public relations relationShip { get; set; }
        public statMatch _nMatches { get; set; }

        public float[] gps;
        public bool ShouldSerializePhoto()
        {
            if (photo == "")
                return false;
            return true;
        }
        public User(string email, string password, string fullName,
                    string birthday, string photo, float[] coords)
        {
            this.email = email;
            this.password = password;
            this.fullName = fullName;
            this.birthday = birthday;
            this.photo = photo;
            this.gps = coords;
        }
    }
}
