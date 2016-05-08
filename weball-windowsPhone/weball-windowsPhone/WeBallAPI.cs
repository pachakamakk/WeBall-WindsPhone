using System;
using System.Collections.Generic;
using Windows.Foundation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using System.Dynamic;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using Windows.Devices.Geolocation;
using System.IO.IsolatedStorage;
using System.Net;
using System.Windows.Controls;

namespace weball_windowsPhone
{
    public class WeBallAPI
    {
        private static string token = "";
        private static string baseUri = "http://api.weball.fr";
        public static User currentUser;
        private static List<Five> fiveList = null;
        public static List<Five> FiveList
        {
            get { return fiveList; }
            set { fiveList = value;}
        }
        public static string Token
        {
            get { return token; }
            set { token = value; }
        }

        private static bool success = true;
        private static string error;
        public static bool Success
        {
            get { return success; }
            set { success = value; }
        }

        private static string username = "";
        public static string Username
        {
            get { return username; }
            set { username = value; }
        }

        private static string password = "";
        public static string Password
        {
            get { return password; }
            set { password = value; }
        }

        private static string email = "";
        public static string Email
        {
            get { return email; }
            set { email = value; }
        }
 
        private static string firstName = "";
        public static string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }
 
        private static string lastName = "";
        public static string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }
 
        private static string birthday = "";
        public static string Birthday
        {
            get { return birthday; }
            set { birthday = value; }
        }

        private static int points = 0;
        public static int Points
        {
            get { return points; }
            set { points = value; }
        }
        public static byte[] ImageToBytes(BitmapImage img)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                WriteableBitmap btmMap = new WriteableBitmap(img);
                System.Windows.Media.Imaging.Extensions.SaveJpeg(btmMap, ms, img.PixelWidth, img.PixelHeight, 0, 100);
                img = null;
                return ms.ToArray();
            }
        }
        public static List<T> DeserializeToList<T>(string jsonString)
        {
            try
            {
                var array = JArray.Parse(jsonString);
                List<T> objectsList = new List<T>();

                foreach (var item in array)
                {
                    System.Diagnostics.Debug.WriteLine("Item: " + item.ToString());
                    try
                    {
                        objectsList.Add(item.ToObject<T>());
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("Error invalid elem found: " + ex.Message);
                    }
                }
                return objectsList;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error invalid json found: " + ex.Message);
                return null;
            }
        }
        public static async Task getFives()
        {
            using (HttpClient hc = new HttpClient())
            {
                hc.DefaultRequestHeaders.IfModifiedSince = new DateTimeOffset(DateTime.Now);
                HttpResponseMessage msg;
                Uri connectionUri = new Uri(WeBallAPI.baseUri + "/five?lgt=" + /* currentUser.gps[0].ToString().Replace(',','.') */ "12" +
                                            "&lat=" + /*currentUser.gps[1].ToString().Replace(',', '.')*/ "12");
                hc.DefaultRequestHeaders.Add("x-access-token", WeBallAPI.token);
                System.Diagnostics.Debug.WriteLine("URL: " + connectionUri);
                msg = await hc.GetAsync(connectionUri);
                System.Diagnostics.Debug.WriteLine("step 2");
                if (msg.IsSuccessStatusCode)
                {
                    success = true;
                    string response = msg.Content.ReadAsStringAsync().Result;
                    System.Diagnostics.Debug.WriteLine("step 3");
                    //System.Diagnostics.Debug.WriteLine("return: " + response);
                    WeBallAPI.fiveList = DeserializeToList<Five>(response);
                }
                else
                    System.Diagnostics.Debug.WriteLine("Error " + msg.StatusCode + " and " + msg.Content.ReadAsStringAsync().Result);
            }
        }

        public static async Task joinMatch(string teamId)
        {
            using (HttpClient hc = new HttpClient())
            {
                hc.DefaultRequestHeaders.IfModifiedSince = new DateTimeOffset(DateTime.Now);
                var method = new HttpMethod("PATCH");
                var request = new HttpRequestMessage(method, WeBallAPI.baseUri + "/matches/join/" + teamId);
                hc.DefaultRequestHeaders.Add("x-access-token", WeBallAPI.token);
                System.Diagnostics.Debug.WriteLine("Sending: " + request.RequestUri);
                HttpResponseMessage msg;
                msg = await hc.SendAsync(request);
                if (msg.IsSuccessStatusCode)
                {
                    string response = msg.Content.ReadAsStringAsync().Result;
                    System.Diagnostics.Debug.WriteLine("Match joined! Received: " + response);
                    success = true;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Error " + msg.StatusCode + "cant join: " + msg.Content.ReadAsStringAsync().Result);
                    success = false;
                }
            }
        }
        public static async Task addMatch(string name, DateTime start, DateTime end,
                                            int maxPlayers, string field, string[] invited = null)
        {
            using (HttpClient hc = new HttpClient())
            {
                hc.DefaultRequestHeaders.IfModifiedSince = new DateTimeOffset(DateTime.Now);
                MatchTmp match = new MatchTmp();
                HttpResponseMessage msg;
                match.name = name;
                match.startDate = start.ToString("o").Split('+')[0];
                match.endDate = end.ToString("o").Split('+')[0];
                match.maxPlayers = maxPlayers;
                match.field = field;
                hc.DefaultRequestHeaders.Add("x-access-token", WeBallAPI.token);
                hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                Uri connectionUri = new Uri(WeBallAPI.baseUri + "/matches/");
                string parsed = "{ \"match\" :" + JsonConvert.SerializeObject(match) + "}";
                msg = await hc.PostAsync(connectionUri, new StringContent(parsed, Encoding.UTF8, "application/json"));
                System.Diagnostics.Debug.WriteLine("Sending: " + parsed);
                if (msg.IsSuccessStatusCode)
                {
                    string response = msg.Content.ReadAsStringAsync().Result;
                    System.Diagnostics.Debug.WriteLine("Matches created! Received: " + response);
                    success = true;
                    JToken parsedResponse = JObject.Parse(response);
                }
                else
                {
                    success = false;
                    System.Diagnostics.Debug.WriteLine("Error " + msg.StatusCode + " and " + msg.Content.ReadAsStringAsync().Result);
                }
            }

        }
        public static async Task getMatches(string fiveId)
        {
            using (HttpClient hc = new HttpClient())
            {
                HttpResponseMessage msg;
                hc.DefaultRequestHeaders.IfModifiedSince = new DateTimeOffset(DateTime.Now);
                string dateParam = "&startDate=" + DateTime.Today.AddMonths(-2).ToString("o").Split('+')[0] + 
                                    "&endDate=" + DateTime.Today.AddMonths(4).ToString("o").Split('+')[0];
                string query = "?sort=startDate" + dateParam;
                Uri connectionUri = new Uri(WeBallAPI.baseUri + "/matches/five/" + fiveId + query);
                hc.DefaultRequestHeaders.Add("x-access-token", WeBallAPI.token);
                System.Diagnostics.Debug.WriteLine("URL: " + connectionUri);
                msg = await hc.GetAsync(connectionUri);
                if (msg.IsSuccessStatusCode)
                {
                    success = true;
                    string response = msg.Content.ReadAsStringAsync().Result;
                    if (response.Length > 2)
                        System.Diagnostics.Debug.WriteLine("Matches received!");
                    var five = WeBallAPI.fiveList.FirstOrDefault(s => s._id == fiveId);
                    var index = WeBallAPI.fiveList.IndexOf(five);
                    var newFive = JToken.Parse(response);
                    WeBallAPI.FiveList[index].matchs = DeserializeToList<Match>(response);
                }
                else
                    System.Diagnostics.Debug.WriteLine("Error " + msg.StatusCode + " and " + msg.Content.ReadAsStringAsync().Result);
            }
        }
        public static async Task getFive(string id)
        {
            using (HttpClient hc = new HttpClient())
            {
                HttpResponseMessage msg;
                hc.DefaultRequestHeaders.IfModifiedSince = new DateTimeOffset(DateTime.Now);
                Uri connectionUri = new Uri(WeBallAPI.baseUri + "/five/" + id);
                hc.DefaultRequestHeaders.Add("x-access-token", WeBallAPI.token);
                System.Diagnostics.Debug.WriteLine("URL: " + connectionUri);
                msg = await hc.GetAsync(connectionUri);
                if (msg.IsSuccessStatusCode)
                {
                    success = true;
                    string response = msg.Content.ReadAsStringAsync().Result;
                    System.Diagnostics.Debug.WriteLine("step 3 getfive");
                    System.Diagnostics.Debug.WriteLine("return: " + response);
                    var five = WeBallAPI.fiveList.FirstOrDefault(s => s._id == id);
                    var index = WeBallAPI.fiveList.IndexOf(five);
                    var newFive = JToken.Parse(response);
                    var gps = WeBallAPI.FiveList[index].gps;
                    WeBallAPI.FiveList[index] = newFive.ToObject<Five>();
                    WeBallAPI.FiveList[index].gps = gps;
//                    WeBallAPI.fiveList.ElementAt<index> = DeserializeToList<Five>(response);
                }
                else
                    System.Diagnostics.Debug.WriteLine("Error " + msg.StatusCode + " and " + msg.Content.ReadAsStringAsync().Result);
            }
        }
        public static async Task updatePos()
        {
            Geolocator geolocator = new Geolocator();
            geolocator.DesiredAccuracyInMeters = 50;
            float[] coord = new float[2];
            try
            {
                Geoposition geoposition = await geolocator.GetGeopositionAsync(
                    maximumAge: TimeSpan.FromMinutes(5),
                    timeout: TimeSpan.FromSeconds(10)
                    );
                coord[0] = float.Parse(geoposition.Coordinate.Latitude.ToString("0.00"));
                coord[1] = float.Parse(geoposition.Coordinate.Longitude.ToString("0.00"));
            }
            catch (Exception ex)
            {
                if ((uint)ex.HResult == 0x80004004)
                {
                    System.Diagnostics.Debug.WriteLine("Cant get location");
                    coord[0] = 50.0f;
                    coord[1] = 50.0f;
                    throw new Exception();
                }
                else
                {
                    if (IsolatedStorageSettings.ApplicationSettings.Contains("LastLocation"))
                        coord = (float[])IsolatedStorageSettings.ApplicationSettings["LastLocation"];
                    else
                        throw new Exception();
                }
            }
            WeBallAPI.currentUser.gps = coord;
            IsolatedStorageSettings.ApplicationSettings["LastLocation"] = coord;
            await WeBallAPI.updateUser();
        }
        public static async Task updateUser()
        {
            using (HttpClient hc = new HttpClient())
            {
                hc.DefaultRequestHeaders.IfModifiedSince = new DateTimeOffset(DateTime.Now);
                string buffer = WeBallAPI.currentUser.photo;
                WeBallAPI.currentUser.photo = "";
                string parsed = "{ \"user\" :" + JsonConvert.SerializeObject(WeBallAPI.currentUser) + "}";
                WeBallAPI.currentUser.photo = buffer;
                var method = new HttpMethod("PATCH");
                var request = new HttpRequestMessage(method, WeBallAPI.baseUri + "/users/me")
                {
                    Content = new StringContent(parsed, Encoding.UTF8, "application/json")
                };
                hc.DefaultRequestHeaders.Add("x-access-token", WeBallAPI.token);
                HttpResponseMessage msg;
                msg = await hc.SendAsync(request);
                if (msg.IsSuccessStatusCode)
                {
                    success = true;
                    System.Diagnostics.Debug.WriteLine("Success");
                }
                else
                    System.Diagnostics.Debug.WriteLine("Error " + msg.StatusCode + " and " + msg.Content.ReadAsStringAsync().Result);
            }
        }
        public static async Task register(string password, string email, string fullname,
                                        string birthday, ImageSource photo, float[] gps)
        {
            using (HttpClient hc = new HttpClient())
            {
                hc.DefaultRequestHeaders.IfModifiedSince = new DateTimeOffset(DateTime.Now);
                User newUser = new User(email, password, fullname, 
                                        birthday, "data:image/bitmap;base64," + Convert.ToBase64String(ImageToBytes(photo as BitmapImage)), 
                                        gps);
                hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string parsed = JsonConvert.SerializeObject(newUser);
                HttpResponseMessage msg;
                Uri connectionUri = new Uri(WeBallAPI.baseUri + "/users");
                msg = await hc.PostAsync(connectionUri, new StringContent("{ \"user\" :" + parsed + "}", Encoding.UTF8, "application/json"));
                if (msg.IsSuccessStatusCode)
                {
                    WeBallAPI.currentUser = newUser;
                    success = true;
                    System.Diagnostics.Debug.WriteLine("Success");
                }
                else
                    System.Diagnostics.Debug.WriteLine("Error " + msg.StatusCode + " and " + msg.Content.ReadAsStringAsync().Result);
            }
        }
        public static async Task login(string username, string password)
        {
            WeBallAPI.username = username;
            System.Diagnostics.Debug.WriteLine("Step 1");
            using (HttpClient hc = new HttpClient())
            {
                var keyValuePairs = new Dictionary<string, string>();
                keyValuePairs.Add("login", username);
                keyValuePairs.Add("password", password);
                var content = new FormUrlEncodedContent(keyValuePairs);
                HttpResponseMessage msg;
                Uri connectionUri = new Uri(WeBallAPI.baseUri + "/login/users");
                System.Diagnostics.Debug.WriteLine("URI: " + connectionUri);
                msg = await hc.PostAsync(connectionUri, content);
                if (msg.IsSuccessStatusCode)
                {
                    success = true;
                    string response = msg.Content.ReadAsStringAsync().Result;
                    WeBallAPI.password = password;
                    JToken parsedResponse = JObject.Parse(response);
                    WeBallAPI.token = (string) parsedResponse.SelectToken("token");
                    System.Diagnostics.Debug.WriteLine("Token: " + WeBallAPI.token);
                }
                else
                    System.Diagnostics.Debug.WriteLine("Error " + msg.StatusCode + " and " + msg.Content.ReadAsStringAsync().Result);
            }
        }

        public static async Task me()
        {
            using (HttpClient hc = new HttpClient())
            {
                hc.DefaultRequestHeaders.IfModifiedSince = new DateTimeOffset(DateTime.Now);
                HttpResponseMessage msg;
                Uri connectionUri = new Uri(WeBallAPI.baseUri + "/users/me?token=" + token);
                msg = await hc.GetAsync(connectionUri);
                if (msg.IsSuccessStatusCode)
                {
                    string response = msg.Content.ReadAsStringAsync().Result;
                    if (String.IsNullOrEmpty(response))
                        return;
                    JToken parsedResponse = JObject.Parse(response);
                    success = true;
                    float[] test = new float[2];
                    test[0] = 0f;
                    test[1] = 0f;
                    WeBallAPI.currentUser = parsedResponse.ToObject<User>();
                    WeBallAPI.currentUser.password = WeBallAPI.password;
                    System.Diagnostics.Debug.WriteLine("return: " + response);
                }
                else
                    System.Diagnostics.Debug.WriteLine("Error " + msg.StatusCode);
            }
        }
    }
}
