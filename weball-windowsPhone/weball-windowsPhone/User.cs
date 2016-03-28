using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weball_windowsPhone
{
    public class User
    {
        public string username { get; private set; }
        public string email { get; private set; }
        public string password { get; private set; }
        public string firstName { get; private set; }
        public string lastName { get; private set; }
        public string birthday { get; private set; }
        public string photo;
        public float[] gps;
        public bool ShouldSerializePhoto()
        {
            if (photo == "")
                return false;
            return true;
        }
        public User(string username, string email, string password, string firstName,
                    string lastName, string birthday, string photo, float[] coords)
        {
            this.username = username;
            this.email = email;
            this.password = password;
            this.firstName = firstName;
            this.lastName = lastName;
            this.birthday = birthday;
            this.photo = photo;
            this.gps = coords;
        }
    }
}
