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
using System.Windows;

namespace weball_windowsPhone
{
    /* Classe principale pour l'API */
    public class WeBallAPI
    {
        private static string token = "";
        private static string baseUri = "https://api.weball.fr";
        public static User currentUser;
        public static string selectedFive;
        public static NotifHandler notifs;
        public static User profileUser;
        public static List<Relation> relations;
        public static List<littleUser> search;
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

        /* Route reset mot de passe */
        public static async Task resetPassword(string email)
        {
            try
            {
                using (HttpClient hc = new HttpClient())
                {
                    hc.DefaultRequestHeaders.Add("x-access-token", WeBallAPI.token);
                    var method = new HttpMethod("PATCH");
                    HttpResponseMessage msg;
                    var keyValuePairs = new Dictionary<string, string>();
                    keyValuePairs.Add("email", email);
                    var content = new FormUrlEncodedContent(keyValuePairs);
                    var request = new HttpRequestMessage(method, WeBallAPI.baseUri + "/resetpassword")
                    {
                        Content = content
                    };
                    msg = await hc.SendAsync(request);
                    if (msg.IsSuccessStatusCode)
                    {
                        MessageBoxResult result =
                            MessageBox.Show("Email envoyé!",
                                "Erreur",
                         MessageBoxButton.OK);
                        success = true;
                    }
                    else
                    {
                        MessageBoxResult result =
                            MessageBox.Show("Erreur. Mauvais e-mail?",
                                "Erreur",
                        MessageBoxButton.OK);
                        success = false;
                    }
                }
            }
            catch (Exception e)
            {
                success = false;
                MessageBoxResult result =
                    MessageBox.Show("Erreur innatendue. Réessayez ultérieurement!",
                        "Erreur",
                MessageBoxButton.OK);
            }
        }
        
        /* Route récupération fives */
        public static async Task getFives()
        {
            try
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
                    {
                        success = false;
                        System.Diagnostics.Debug.WriteLine("Error " + msg.StatusCode + " and " + msg.Content.ReadAsStringAsync().Result);
                    }
                  }
            }
            catch (Exception e)
            {
                success = false;
                MessageBoxResult result =
                    MessageBox.Show("Erreur innatendue. Réessayez ultérieurement!",
                        "Erreur",
                MessageBoxButton.OK);
            }
        }

        public static async Task inviteMatch(string matchId, string friendId)
        {
            try
            {
                using (HttpClient hc = new HttpClient())
                {
                    hc.DefaultRequestHeaders.Add("x-access-token", WeBallAPI.token);
                    var method = new HttpMethod("PATCH");
                    HttpResponseMessage msg;
                    var keyValuePairs = new Dictionary<string, string>();
                    keyValuePairs.Add("id", matchId);
                    System.Diagnostics.Debug.WriteLine("firendId: " + friendId);
                    keyValuePairs.Add("guests", "[" + friendId + "]");
                    var content = new FormUrlEncodedContent(keyValuePairs);

                    var request = new HttpRequestMessage(method, WeBallAPI.baseUri + "/matches/invit/" + matchId)
                    {
                        Content = new StringContent("{ \"guests\": [\"" + friendId + "\"] }", Encoding.UTF8, "application/json")
                    };
                    msg = await hc.SendAsync(request);
                    if (msg.IsSuccessStatusCode)
                    {
                        MessageBoxResult result =
                            MessageBox.Show("Joueur invité!",
                                "OK",
                         MessageBoxButton.OK);
                        success = true;
                    }
                    else
                    {
                        MessageBoxResult result =
                            MessageBox.Show("Erreur: " + msg.Content.ReadAsStringAsync().Result,
                                "Erreur",
                        MessageBoxButton.OK);
                        success = false;
                    }
                }
            }
            catch (Exception e)
            {
                success = false;
                MessageBoxResult result =
                    MessageBox.Show("Erreur innatendue. Réessayez ultérieurement!",
                        "Erreur",
                MessageBoxButton.OK);
            }

        }
        public static async Task updateMatch(Match match, int length)
        {
            try
            {
                using (HttpClient hc = new HttpClient())
                {
                    hc.DefaultRequestHeaders.Add("x-access-token", WeBallAPI.token);
                    var method = new HttpMethod("PATCH");
                    HttpResponseMessage msg;
                    var keyValuePairs = new Dictionary<string, string>();
                    MatchTmp matchTmp = new MatchTmp();
                    matchTmp.name = match.name;
                    matchTmp.field = match.field;
                    matchTmp.startDate = match.startDate.ToString("o").Split('+')[0];
                    matchTmp.endDate = match.startDate.AddHours(length).ToString("o").Split('+')[0];
                    matchTmp.maxPlayers = match.maxPlayers;
                    keyValuePairs.Add("match", JsonConvert.SerializeObject(matchTmp));
//                    keyValuePairs.Add("team", );
                    var content = new FormUrlEncodedContent(keyValuePairs);
                    var request = new HttpRequestMessage(method, WeBallAPI.baseUri + "/matches/" + match._id)
                    {
                        Content = content
                    };
                    msg = await hc.SendAsync(request);
                    if (msg.IsSuccessStatusCode)
                    {
                        MessageBoxResult result =
                            MessageBox.Show("Match modifié!",
                                "OK",
                         MessageBoxButton.OK);
                        success = true;
                    }
                    else
                    {
                        MessageBoxResult result =
                            MessageBox.Show("Erreur. Mauvaise horaire/durée? " + msg.Content.ReadAsStringAsync().Result,
                                "Erreur",
                        MessageBoxButton.OK);
                        success = false;
                    }
                }
            }
            catch (Exception e)
            {
                success = false;
                MessageBoxResult result =
                    MessageBox.Show("Erreur innatendue. Réessayez ultérieurement!",
                        "Erreur",
                MessageBoxButton.OK);
            }
        }


        /* Route récupération notifications */
        public static async Task getNotifications()
        {
            try
            {
                using (HttpClient hc = new HttpClient())
                {
                    hc.DefaultRequestHeaders.IfModifiedSince = new DateTimeOffset(DateTime.Now);
                    HttpResponseMessage msg;
                    Uri connectionUri = new Uri(WeBallAPI.baseUri + "/notifications");
                    hc.DefaultRequestHeaders.Add("x-access-token", WeBallAPI.token);
                    System.Diagnostics.Debug.WriteLine("URL: " + connectionUri);
                    msg = await hc.GetAsync(connectionUri);
                    if (msg.IsSuccessStatusCode)
                    {
                        success = true;
                        string response = msg.Content.ReadAsStringAsync().Result;
                        System.Diagnostics.Debug.WriteLine("step 3");
                        System.Diagnostics.Debug.WriteLine("return: " + response);
                        JToken parsedResponse = JObject.Parse(response);
                        WeBallAPI.notifs = parsedResponse.ToObject<NotifHandler>();
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("Error " + msg.StatusCode + " and " + msg.Content.ReadAsStringAsync().Result);
                        success = false;
                    }
                }
            }
            catch (Exception e)
            {
                success = false;
                MessageBoxResult result =
                    MessageBox.Show("Erreur innatendue. Réessayez ultérieurement!",
                        "Erreur",
                MessageBoxButton.OK);
            }

        }

        /* Route récupération relations */
        public static async Task getRelations()
        {
            try
            {
                using (HttpClient hc = new HttpClient())
                {
                    hc.DefaultRequestHeaders.IfModifiedSince = new DateTimeOffset(DateTime.Now);
                    HttpResponseMessage msg;
                    Uri connectionUri = new Uri(WeBallAPI.baseUri + "/relationship/" + WeBallAPI.currentUser._id);
                    hc.DefaultRequestHeaders.Add("x-access-token", WeBallAPI.token);
                    System.Diagnostics.Debug.WriteLine("URL: " + connectionUri);
                    msg = await hc.GetAsync(connectionUri);
                    if (msg.IsSuccessStatusCode)
                    {
                        success = true;
                        string response = msg.Content.ReadAsStringAsync().Result;
                        System.Diagnostics.Debug.WriteLine("step 3");
                        System.Diagnostics.Debug.WriteLine("return: " + response);
                        WeBallAPI.relations = DeserializeToList<Relation>(response);
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("Error " + msg.StatusCode + " and " + msg.Content.ReadAsStringAsync().Result);
                        success = false;
                    }
                }
            }
            catch (Exception e)
            {
                success = false;
                MessageBoxResult result =
                    MessageBox.Show("Erreur innatendue. Réessayez ultérieurement!",
                        "Erreur",
                MessageBoxButton.OK);
            }
        }

        /* Route envoi requête ami */

        public static async Task sendRequest(string userId)
        {
            try
            {
                using (HttpClient hc = new HttpClient())
                {
                    hc.DefaultRequestHeaders.Add("x-access-token", WeBallAPI.token);
                    var method = new HttpMethod("POST");
                    var request = new HttpRequestMessage(method, WeBallAPI.baseUri + "/relationship/request/" + userId);
                    HttpResponseMessage msg;
                    msg = await hc.SendAsync(request);
                    if (msg.IsSuccessStatusCode)
                    {
                        MessageBoxResult result =
                            MessageBox.Show("Requête envoyée!",
                                "Requete",
                         MessageBoxButton.OK);
                        success = true;
                    }
                    else
                    {
                        success = false;
                        System.Diagnostics.Debug.WriteLine("Could not send request");
                    }
                }
            }
            catch (Exception e)
            {
                success = false;
                MessageBoxResult result =
                    MessageBox.Show("Erreur innatendue. Réessayez ultérieurement!",
                        "Erreur",
                MessageBoxButton.OK);
            }
        }

        /* Route acceptation requête d'ami */
        public static async Task acceptRequest(string userId)
        {
            try
            {
                using (HttpClient hc = new HttpClient())
                {
                    hc.DefaultRequestHeaders.Add("x-access-token", WeBallAPI.token);
                    var method = new HttpMethod("PATCH");
                    var request = new HttpRequestMessage(method, WeBallAPI.baseUri + "/relationship/request/" + userId);
                    HttpResponseMessage msg;
                    msg = await hc.SendAsync(request);
                    if (msg.IsSuccessStatusCode)
                    {
                        System.Diagnostics.Debug.WriteLine("Request sent!");
                        MessageBoxResult result =
                            MessageBox.Show("Requête acceptée!",
                                "Requete",
                         MessageBoxButton.OK);
                        success = true;
                    }
                    else
                    {
                        success = false;
                        System.Diagnostics.Debug.WriteLine("Could not send request");
                    }
                }
            }
            catch (Exception e)
            {
                success = false;
                MessageBoxResult result =
                    MessageBox.Show("Erreur innatendue. Réessayez ultérieurement!",
                        "Erreur",
                MessageBoxButton.OK);
            }
        }

        /* Route refus demande ami */
        public static async Task denyRequest(string userId)
        {
            try
            {
                using (HttpClient hc = new HttpClient())
                {
                    hc.DefaultRequestHeaders.Add("x-access-token", WeBallAPI.token);
                    var method = new HttpMethod("DELETE");
                    var request = new HttpRequestMessage(method, WeBallAPI.baseUri + "/relationship/request/" + userId);
                    HttpResponseMessage msg;
                    msg = await hc.SendAsync(request);
                    if (msg.IsSuccessStatusCode)
                    {
                        System.Diagnostics.Debug.WriteLine("Request denied!");
                        MessageBoxResult result =
                            MessageBox.Show("Requête refusée!",
                                "Erreur",
                         MessageBoxButton.OK);
                        success = true;
                    }
                    else
                    {
                        success = false;
                        System.Diagnostics.Debug.WriteLine("Could not send request");
                    }
                }
            }
            catch (Exception e)
            {
                success = false;
                MessageBoxResult result =
                    MessageBox.Show("Erreur innatendue. Réessayez ultérieurement!",
                        "Erreur",
                MessageBoxButton.OK);
            }
        }

        /* Route supprimer ami */
        public static async Task eraseRelationship(string userId)
        {
            try
            {
                using (HttpClient hc = new HttpClient())
                {
                    hc.DefaultRequestHeaders.Add("x-access-token", WeBallAPI.token);
                    var method = new HttpMethod("DELETE");
                    var request = new HttpRequestMessage(method, WeBallAPI.baseUri + "/relationship/" + userId);
                    HttpResponseMessage msg;
                    msg = await hc.SendAsync(request);
                    if (msg.IsSuccessStatusCode)
                    {
                        success = true;
                    }
                    else
                    {
                        success = false;
                        System.Diagnostics.Debug.WriteLine("Could not send request");
                    }
                }
            }
            catch (Exception e)
            {
                success = false;
                MessageBoxResult result =
                    MessageBox.Show("Erreur innatendue. Réessayez ultérieurement!",
                        "Erreur",
                MessageBoxButton.OK);
            }
        }

        /* Route rejoindre match */
        public static async Task joinMatch(string teamId)
        {
            try
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
            catch (Exception e)
            {
                success = false;
                MessageBoxResult result =
                    MessageBox.Show("Erreur innatendue. Réessayez ultérieurement!",
                        "Erreur",
                MessageBoxButton.OK);
            }
        }

        /* Route ajout match */
        public static async Task addMatch(string name, DateTime start, DateTime end,
                                            int maxPlayers, string field, string[] invited = null)
        {
            try
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
            catch (Exception e)
            {
                success = false;
                MessageBoxResult result =
                    MessageBox.Show("Erreur innatendue. Réessayez ultérieurement!",
                        "Erreur",
                MessageBoxButton.OK);
            }
        }

        /* Route recherche utilisateur */
        public static async Task searchUser(string name)
        {
            try
            {
                using (HttpClient hc = new HttpClient())
                {
                    HttpResponseMessage msg;
                    hc.DefaultRequestHeaders.IfModifiedSince = new DateTimeOffset(DateTime.Now);
                    Uri connectionUri = new Uri(WeBallAPI.baseUri + "/users/search/" + name);
                    hc.DefaultRequestHeaders.Add("x-access-token", WeBallAPI.token);
                    System.Diagnostics.Debug.WriteLine("URL: " + connectionUri);
                    msg = await hc.GetAsync(connectionUri);
                    if (msg.IsSuccessStatusCode)
                    {
                        success = true;
                        string response = msg.Content.ReadAsStringAsync().Result;
                        System.Diagnostics.Debug.WriteLine("Result: " + response);
                        WeBallAPI.search = DeserializeToList<littleUser>(response);
                    }
                    else
                    {
                        success = false;
                        System.Diagnostics.Debug.WriteLine("Error " + msg.StatusCode + " and " + msg.Content.ReadAsStringAsync().Result);
                    }
                }
            }
            catch (Exception e)
            {
                success = false;
                MessageBoxResult result =
                    MessageBox.Show("Erreur innatendue. Réessayez ultérieurement!",
                        "Erreur",
                MessageBoxButton.OK);
            }

        }
        /* Route détails matchs */
        public static async Task getMatch(string matchId)
        {
            try
            {
                using (HttpClient hc = new HttpClient())
                {
                    HttpResponseMessage msg;
                    hc.DefaultRequestHeaders.IfModifiedSince = new DateTimeOffset(DateTime.Now);
                    Uri connectionUri = new Uri(WeBallAPI.baseUri + "/matches/" + matchId);
                    hc.DefaultRequestHeaders.Add("x-access-token", WeBallAPI.token);
                    System.Diagnostics.Debug.WriteLine("URL: " + connectionUri);
                    msg = await hc.GetAsync(connectionUri);
                    if (msg.IsSuccessStatusCode)
                    {
                        success = true;
                        string response = msg.Content.ReadAsStringAsync().Result;
                        if (response.Length > 2)
                            System.Diagnostics.Debug.WriteLine("Match received!\n" + response);
                        JToken parsedResponse = JObject.Parse(response);
                        var match = parsedResponse.ToObject<Match>();
                        var five = WeBallAPI.fiveList.FirstOrDefault(s => s._id == match.five._id);
                        var index = WeBallAPI.fiveList.IndexOf(five);
                        var oldMatch = five.matchs.FirstOrDefault(s => s._id == matchId);
                        var indexMatch = five.matchs.IndexOf(oldMatch);
                        five.matchs[indexMatch] = match;
                        WeBallAPI.FiveList[index] = five;
                    }
                    else
                    {
                        success = false;
                        System.Diagnostics.Debug.WriteLine("Error " + msg.StatusCode + " and " + msg.Content.ReadAsStringAsync().Result);
                    }
                }
            }
            catch (Exception e)
            {
                success = false;
                MessageBoxResult result =
                    MessageBox.Show("Erreur innatendue. Réessayez ultérieurement!",
                        "Erreur",
                MessageBoxButton.OK);
            }
        }

        /* Route récupérations matchs d'un five */
        public static async Task getMatches(string fiveId)
        {
            try
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
                    {
                        success = false;
                        System.Diagnostics.Debug.WriteLine("Error " + msg.StatusCode + " and " + msg.Content.ReadAsStringAsync().Result);
                    }
                }
            }
            catch (Exception e)
            {
                success = false;
                MessageBoxResult result =
                    MessageBox.Show("Erreur innatendue. Réessayez ultérieurement!",
                        "Erreur",
                MessageBoxButton.OK);
            }
        }

        /* Route quitter match */
        public static async Task leaveMatch(string id)
        {
            try
            {
                using (HttpClient hc = new HttpClient())
                {
                    HttpResponseMessage msg;
                    hc.DefaultRequestHeaders.IfModifiedSince = new DateTimeOffset(DateTime.Now);
                    hc.DefaultRequestHeaders.Add("x-access-token", WeBallAPI.token);
                    var method = new HttpMethod("DELETE");
                    var request = new HttpRequestMessage(method, WeBallAPI.baseUri + "/matches/leave/" + id);
                    msg = await hc.SendAsync(request);
                    if (msg.IsSuccessStatusCode)
                    {
                        success = true;
                        string response = msg.Content.ReadAsStringAsync().Result;
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("Error " + msg.StatusCode + " and " + msg.Content.ReadAsStringAsync().Result);
                        success = false;
                    }
                }
            }
            catch (Exception e)
            {
                success = false;
                MessageBoxResult result =
                    MessageBox.Show("Erreur innatendue. Réessayez ultérieurement!",
                        "Erreur",
                MessageBoxButton.OK);
            }
        }

        /* Route récupérations détails d'un five */
        public static async Task getFive(string id)
        {
            try
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
                    {
                        success = false;
                        MessageBoxResult result =
                            MessageBox.Show("Erreur: " + msg.Content.ReadAsStringAsync().Result,
                                "Erreur",
                        MessageBoxButton.OK);
                        System.Diagnostics.Debug.WriteLine("Error " + msg.StatusCode + " and " + msg.Content.ReadAsStringAsync().Result);
                    }
                }
            }
            catch (Exception e)
            {
                success = false;
                MessageBoxResult result =
                    MessageBox.Show("Erreur innatendue. Réessayez ultérieurement!",
                        "Erreur",
                MessageBoxButton.OK);
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

        /* Route update profil utilisateur */
        public static async Task updateUser()
        {
            try
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
                    {
                        success = false;
                        MessageBoxResult result =
                            MessageBox.Show("Erreur: " + msg.Content.ReadAsStringAsync().Result,
                                "Erreur",
                        MessageBoxButton.OK);
                        System.Diagnostics.Debug.WriteLine("Error " + msg.StatusCode + " and " + msg.Content.ReadAsStringAsync().Result);
                    }
                }
            }
            catch (Exception e)
            {
                success = false;
                MessageBoxResult result =
                    MessageBox.Show("Erreur innatendue. Réessayez ultérieurement!",
                        "Erreur",
                MessageBoxButton.OK);
            }
        }

        /* Route inscription */
        public static async Task register(string password, string email, string fullname,
                                        string birthday, ImageSource photo, float[] gps)
        {
            try
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
                    {
                        MessageBoxResult result =
                            MessageBox.Show("Erreur: " + msg.Content.ReadAsStringAsync().Result,
                                "Erreur",
                        MessageBoxButton.OK);
                        success = false;
                        System.Diagnostics.Debug.WriteLine("Error " + msg.StatusCode + " and " + msg.Content.ReadAsStringAsync().Result);
                    }
                }
            }
            catch (Exception e)
            {
                success = false;
                MessageBoxResult result =
                    MessageBox.Show("Erreur innatendue. Réessayez ultérieurement!",
                        "Erreur",
                MessageBoxButton.OK);
            }
        }

        /* Route login */
        public static async Task login(string username, string password)
        {
            try
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
                        WeBallAPI.token = (string)parsedResponse.SelectToken("token");
                        System.Diagnostics.Debug.WriteLine("Token: " + WeBallAPI.token);
                    }
                    else
                    {
                        success = false;
                        MessageBoxResult result =
                            MessageBox.Show("Erreur: " + msg.Content.ReadAsStringAsync().Result,
                                "Erreur",
                        MessageBoxButton.OK);
                        System.Diagnostics.Debug.WriteLine("Error " + msg.StatusCode + " and " + msg.Content.ReadAsStringAsync().Result);
                    }
                }
            }
            catch (Exception e)
            {
                success = false;
                MessageBoxResult result =
                    MessageBox.Show("Erreur innatendue. Réessayez ultérieurement!",
                        "Erreur",
                MessageBoxButton.OK);
            }
        }

        /* Route récupérations détails utilisateur */
        public static async Task getUser(string userId)
        {
            try
            {
                using (HttpClient hc = new HttpClient())
                {
                    hc.DefaultRequestHeaders.IfModifiedSince = new DateTimeOffset(DateTime.Now);
                    HttpResponseMessage msg;
                    Uri connectionUri = new Uri(WeBallAPI.baseUri + "/users/" + userId);
                    hc.DefaultRequestHeaders.Add("x-access-token", WeBallAPI.token);
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
                        WeBallAPI.profileUser = parsedResponse.ToObject<User>();
                        System.Diagnostics.Debug.WriteLine("return: " + response);
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("Error " + msg.StatusCode);
                        success = false;
                    }
                }
            }
            catch (Exception e)
            {
                success = false;
                MessageBoxResult result =
                    MessageBox.Show("Erreur innatendue. Réessayez ultérieurement!",
                        "Erreur",
                MessageBoxButton.OK);
            }
        }

        /* Route récupération infos utilisateur courant */
        public static async Task me()
        {
            try
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
                    {
                        success = false;
                        System.Diagnostics.Debug.WriteLine("Error " + msg.StatusCode);
                    }
                }
            }
            catch (Exception e)
            {
                success = false;
                MessageBoxResult result =
                    MessageBox.Show("Erreur innatendue. Réessayez ultérieurement!",
                        "Erreur",
                MessageBoxButton.OK);
            }
        }
    }
}
