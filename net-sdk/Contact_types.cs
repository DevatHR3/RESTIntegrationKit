using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using System.Xml.Serialization;
using System.Runtime.Serialization;


namespace HR3Weblinks.Examples
{
    
    public class Update_datedInfo
    {
        public DateTime? Update_Date { get; set; }
    }

    public class PKandUpdateDate
    {
        public int ID { get; set; }
        public DateTime? Update_Date { get; set; }
    }

    public class ContactTypeInfo 
    {
        public ContactTypeInfo() { }

        // Properties
        public int Contact_Type_Id { get; set; }
        [XmlElementAttribute(IsNullable = true)]
        public string Contact_Type_Desc { get; set; }
        public DateTime? Update_Date { get; set; }
    }


    public class Contact_types_class
    {
        // Perform a connection to wsPing - just so we can see the REST service is up and running. 
        // wsPing does not require Authorisation access. 
        public void wsPingURL()
        {
        
            using (var client = new HttpClient())
            {  
                client.BaseAddress = Helperfunctions.getURI();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync("wsPing").Result;
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception("/wsPing doesn't return expected value ");
                }

                // Test that the string = 'ping' was returned. 
                string testObject = (string)response.Content.ReadAsAsync<string>().Result;
                Console.WriteLine("wsPing() : " + testObject);

                if (!(testObject.Contains("/Date")))  // should look at todays date!
                    throw new Exception("wsPing doesn't return expected value. " + testObject);
            }
        } // end wsPingURL


        // wsBuildNoURL - when executes performs a check to the services connected database. 
        public void wsBuildNoURL()
        {

            using (var client = new HttpClient())
            {   
                client.BaseAddress = Helperfunctions.getURI();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync("wsBuildNo").Result;
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception("/wsBuildNo doesn't return expected value ");
                }

                // Test that the string = 'ping' was returned. 
                string testObject = (string)response.Content.ReadAsAsync<string>().Result;
                Console.WriteLine("wsBuildNo() : " + testObject);
            }
        } // end wsBuildNoURL


        // todo: Rename accordingly. 
        // - show how to make a Autho access call then perform a API call Contact_types GET 
        public string getAuthoContact_types()
        {
            List<ContactTypeInfo> Items = null;
            string lsSessiontoken; 

            using (var client = new HttpClient())
            {   
                client.BaseAddress = Helperfunctions.getURI();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    lsSessiontoken = Helperfunctions.RequestSessionToken(Common.Email, Common.Password, Common.DefaultAccessToken);
                }
                catch (System.Exception e)
                {
                    throw new Exception(string.Format("Error Message:{0}", e.Message));
                    // if token is invalid  - revoked access apply for a new token
                }

                // Header Details - software key 
                client.DefaultRequestHeaders.Add("cmkey", lsSessiontoken);
                // Who is the user of that is making the call. 
                client.DefaultRequestHeaders.Add("username", Common.Username);
                // Both must be supplied otherwise call will fail with a 401. 

                // Doc: Request a List of all the Contact types that are available. 
                HttpResponseMessage response = client.GetAsync("ContactTypes/1").Result;
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return "/ContactTypes doesn't return expected value ";
                }

                var jsonString = response.Content.ReadAsStringAsync();
                jsonString.Wait();
                Items = JsonConvert.DeserializeObject<List<ContactTypeInfo>>(jsonString.Result);

                Console.WriteLine("getContacttypes : " + jsonString.Result);
                for (int i = 0; i < Items.Count; i++)
                {
                    Console.WriteLine(Items[i].Contact_Type_Id + ", " + Items[i].Contact_Type_Desc);
                };
            }

            return "";
        } // getAuthoContact_types() 


        /// Retrieve a List of Contact_types 
        /// This makes a call assuming you have a Sessiontoken if not it will go threw the process making sure it is ok. 
        public string getContact_types()
        {
            List<ContactTypeInfo> Items = null;

            using (var client = new HttpClient())
            {   
                client.BaseAddress = Helperfunctions.getURI();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    if (Common.Instance().SessionToken == "") {
                        Helperfunctions.RequestSessionToken(Common.Email, Common.Password, Common.DefaultAccessToken);
                    }
                    // if the token is not valid or expired an error will be raised,
                    // which inturn means we will need to asking for a new token. 
                }
                catch (System.Exception e)
                {
                    // throw new Exception(string.Format("Error Message:{0}", e.Message));
                    return string.Format("Error Message:{0}", e.Message);
                }

                // Header Details - Session token
                client.DefaultRequestHeaders.Add("cmkey", Common.Instance().SessionToken );
                // Who is the user of that is making the call. 
                client.DefaultRequestHeaders.Add("username", Common.Username);
                // Both must be supplied otherwise call will fail with a 401. 

                //Doc: Request a List of all the Contact types that are available. 
                HttpResponseMessage response = client.GetAsync("ContactTypes/0").Result;

                switch (response.StatusCode)
                {
                    case HttpStatusCode.NotFound :
                        return "/Contact_types path failed with : 404 Not Found ";
                    case HttpStatusCode.Forbidden :    //   AuthenticationFailed
                        // Sessiontoken possible failure  - attempt with a new one. 
                        Common.Instance().SessionToken = "";  // wipe session token 
                        Helperfunctions.RequestSessionToken(Common.Email, Common.Password, Common.DefaultAccessToken);

                        response = client.GetAsync("ContactTypes/0").Result;
                        break;
                    case HttpStatusCode.OK :
                        break; // Worked fine. 
                }
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return "/ContactTypes doesn't return expected value ";
                }

                // Given the Json Returned - write out the return values
                var jsonString = response.Content.ReadAsStringAsync();
                jsonString.Wait();
                Items = JsonConvert.DeserializeObject<List<ContactTypeInfo>>(jsonString.Result);

                Console.WriteLine("getContacttypes : " + jsonString.Result);
                for (int i = 0; i < Items.Count; i++)
                {
                    Console.WriteLine(Items[i].Contact_Type_Id + ", " + Items[i].Contact_Type_Desc);
                };

            }

            return "";
        } // getContact_types() 

        // Request a set of Contact_types
        public string getContact_typesAsync()
        {
            // Use the service with the software GUID        
            List<ContactTypeInfo> Items = null;

            string lsResult = "";

            try
            {
                if (Common.Instance().SessionToken == "")
                {
                    Helperfunctions.RequestSessionToken(Common.Email, Common.Password, Common.DefaultAccessToken);
                }
                // if the token is not valid or expired an error will be raised,
                // which inturn means we will need to asking for a new token. 
            }
            catch (System.Exception e)
            {
                // throw new Exception(string.Format("Error Message:{0}", e.Message));
                return string.Format("Error Message:{0}", e.Message);
            }

            var client = new HttpClient();
            var request = new System.Net.Http.HttpRequestMessage() {
                RequestUri = Helperfunctions.getURI("ContactTypes/1,3,4,5"), 
                Method = HttpMethod.Get
            };

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Header Details - Session token
            request.Headers.Add("cmkey", Common.Instance().SessionToken );
            // Who is the user of that is making the call. 
            request.Headers.Add("username", Common.Username);

            Console.WriteLine("getContacttypesAsync : Sending Asysnc");
            var task = client.SendAsync(request).ContinueWith((taskwithmsg) => 
            {
                var response = taskwithmsg.Result;

                if (response.StatusCode == HttpStatusCode.OK) {
                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();

                    try 
                    {
                        Items = JsonConvert.DeserializeObject<List<ContactTypeInfo>>(jsonString.Result);
                        Console.WriteLine("getContacttypesAsync : " + jsonString.Result);
                        for (int i = 0; i < Items.Count; i++)
                        {
                            Console.WriteLine("Item : "+Items[i].Contact_Type_Id + ", " + Items[i].Contact_Type_Desc );
                        }

                    }
                    catch (Exception ex)
                    {
                        lsResult = lsResult + "Error : " + ex.Message + ", ";
                    }

                }
                // else report errors 

            });
        
            task.Wait();
            return lsResult;

        }


        public string getaContact_typeAsync()
        {
            // Use the service with the software GUID        
            ContactTypeInfo loItem = null;

            string lsResult = "";

            try
            {
                if (Common.Instance().SessionToken == "")
                {
                    Helperfunctions.RequestSessionToken(Common.Email, Common.Password, Common.DefaultAccessToken);
                }
                // if the token is not valid or expired an error will be raised,
                // which inturn means we will need to asking for a new token. 
            }
            catch (System.Exception e)
            {
                // throw new Exception(string.Format("Error Message:{0}", e.Message));
                return string.Format("Error Message:{0}", e.Message);
            }

            var client = new HttpClient();
            var request = new System.Net.Http.HttpRequestMessage()
            {
                RequestUri = Helperfunctions.getURI("ContactTypes/1"), 
                Method = HttpMethod.Get
            };

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Header Details - Session token
            request.Headers.Add("cmkey", Common.Instance().SessionToken);
            // Who is the user of that is making the call. 
            request.Headers.Add("username", Common.Username);

            Console.WriteLine("getaContacttypeAsync : Sending Asysnc");
            var task = client.SendAsync(request).ContinueWith((taskwithmsg) =>
            {
                var response = taskwithmsg.Result;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();

                    try
                    {
                        loItem = JsonConvert.DeserializeObject<ContactTypeInfo>(jsonString.Result);
                        Console.WriteLine("getaContacttypesAsync : " + jsonString.Result);
                        Console.WriteLine("Item : " + loItem.Contact_Type_Id+ ", " + loItem.Contact_Type_Desc );

                    }
                    catch (Exception ex)
                    {
                        //Console.WriteLine("Error reading converting json data " + ex.Message);
                        // How do I dump the error msg for processing later it's failed. 
                        lsResult = "Error : " + ex.Message;
                    }

                }
                else
                {
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.NotFound:
                            lsResult = lsResult + "Error : /ContactTypes/2 path failed with : 404 Not Found ";
                            break;
                        case HttpStatusCode.Forbidden:    //   AuthenticationFailed
                            // Sessiontoken possible failure  - attempt with a new one. 
                            lsResult = lsResult + "Error : AuthenticationFailed.";
                            break;
                        default :
                            lsResult = lsResult + string.Format("Error Message:{0}", response.StatusCode);
                            break; // Worked fine. 
                    }
                }
                

            });

            task.Wait();
            return lsResult;

        }


        public string insContact_type()
        {
            //Dictionary<string, string> args = new Dictionary<string, string>();
            ContactTypeInfo loType01 = new ContactTypeInfo() { Contact_Type_Id = 0, Contact_Type_Desc = "Serialized type", Update_Date = null };

            string str = JsonConvert.SerializeObject(loType01);

            string targetUri = Helperfunctions.getURI("ContactTypes").ToString(); // "http://localhost:64398/Hr3RestService/contacts/datasource?format=json";
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(targetUri);

            request.ContentType = "application/json; charset=utf-8";
            request.Method = "POST";
            // Header Details - Session token
            request.Headers.Add("cmkey", Common.Instance().SessionToken);
            // Who is the user of that is making the call. 
            request.Headers.Add("username", Common.Username);

            using (System.IO.Stream writer = request.GetRequestStream())
            {
                byte[] data = Encoding.UTF8.GetBytes(str);
                writer.Write(data, 0, data.Length);

                //request.
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream()))
                    {
                        string tResponse = reader.ReadToEnd();
                        Console.WriteLine(tResponse);
                    }
                }
            }

            return "";

        }

        // insContact_typesAsync() returns an error 
        // todo: finish this off. 
        public async Task<string> insContact_typesAsync()
        {
            // Use the service with the software GUID        
            PKandUpdateDate loResult = null;

            string lsResult = "";
            // id will be assigned and returned  

            try
            {
                if (Common.Instance().SessionToken == "")
                {
                    Helperfunctions.RequestSessionToken(Common.Email, Common.Password, Common.DefaultAccessToken);
                }
                // if the token is not valid or expired an error will be raised,
                // which inturn means we will need to asking for a new token. 
            }
            catch (System.Exception e)
            {
                // throw new Exception(string.Format("Error Message:{0}", e.Message));
                return string.Format("Error Message:{0}", e.Message);
            }

            ContactTypeInfo loType01 = new ContactTypeInfo() { Contact_Type_Id = 0, Contact_Type_Desc = "Async type", Update_Date = null};

            var client = new HttpClient();
            var request = new System.Net.Http.HttpRequestMessage()
            {
                RequestUri = Helperfunctions.getURI("ContactTypes"),
                
                Method = HttpMethod.Post
            };

            // Setup Headers
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            // Header Details - Session token
            client.DefaultRequestHeaders.Add("cmkey", Common.Instance().SessionToken);
            // Who is the user of that is making the call. 
            client.DefaultRequestHeaders.Add("username", Common.Username);

            Console.WriteLine("insContacttypesAsync : Sending Asysnc");
            
            var response = await client.PostAsJsonAsync(request.RequestUri, loType01);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                // Read the response back as we may want the PK that was assigned to it. 
                // Now transpose the Results. 
                var jsonString = response.Content.ReadAsStringAsync();
                jsonString.Wait();

                try
                {
                    loResult = JsonConvert.DeserializeObject<PKandUpdateDate>(jsonString.Result);
                    Console.WriteLine("insContacttypesAsync : " + jsonString.Result);
                    Console.WriteLine("insContacttypesAsync : id= " + loResult.ID.ToString() + " - Update Date = "+loResult.Update_Date.ToString());
                }
                catch (Exception ex)
                {
                    lsResult = "Error : " + ex.Message;
                }
            }
            else
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        lsResult = lsResult + "Error : /ContactTypes path failed with : 404 Not Found ";
                        break;
                    case HttpStatusCode.Forbidden:    //   AuthenticationFailed
                        // Sessiontoken possible failure  - attempt with a new one. 
                        lsResult = lsResult + "Error : AuthenticationFailed.";
                        break;
                    default:
                        lsResult = lsResult + string.Format("Error Message:{0}", response.StatusCode);
                        break; // Worked fine. 
                }
            }
            
            return lsResult;
        }

        // Posts a series of Records via Async into the service. 
        public string insContact_typesAsync02()
        {
            // Use the service with the software GUID        
            List<ContactTypeInfo> loItems = new List<ContactTypeInfo>();
            ContactTypeInfo loItem = null;

            loItems.Add( new ContactTypeInfo { Contact_Type_Desc = "Eleven" }  );
            loItems.Add( new ContactTypeInfo { Contact_Type_Desc = "Twelve" });
            loItems.Add( new ContactTypeInfo { Contact_Type_Desc = "Thirteen" });
            loItems.Add( new ContactTypeInfo { Contact_Type_Desc = "Fourteen" }  );
            loItems.Add( new ContactTypeInfo { Contact_Type_Desc = "Fifthteen" }  );
            loItems.Add( new ContactTypeInfo { Contact_Type_Desc = "Sixteen" }  );
            loItems.Add( new ContactTypeInfo { Contact_Type_Desc = "Seventeen" }  );
            loItems.Add( new ContactTypeInfo { Contact_Type_Desc = "Eightteen" }  );
            loItems.Add( new ContactTypeInfo { Contact_Type_Desc = "Nineteen" }  );
            loItems.Add( new ContactTypeInfo { Contact_Type_Desc = "Twenty" }  );

            PKandUpdateDate loResult = null;

            string lsResult = "";
            // id will be assigned and returned  

            var client = new HttpClient();
            var request = new System.Net.Http.HttpRequestMessage()
            {
                RequestUri = Helperfunctions.getURI("ContactTypes"), 

                Method = HttpMethod.Post
            };

            // Setup Headers
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            // Header Details - Session token
            client.DefaultRequestHeaders.Add("cmkey", Common.Instance().SessionToken);
            // Who is the user of that is making the call. 
            client.DefaultRequestHeaders.Add("username", Common.Username);

            Console.WriteLine("insContacttypesAsync : Sending Asysnc");
            // todo: fire off a series of insert calls. 

            // todo: loItems.AsParallel comeback to this later. 

            while (loItems.Count > 0) 
            {
                loItem = loItems[0];
                loItems.Remove(loItem);

                var task = client.PostAsJsonAsync(request.RequestUri, loItem).ContinueWith((taskwithmsg) =>
                {
                    var response = taskwithmsg.Result;
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        // We now need to read the response back as we may want the PK that was assigned to it. 
                        // Now transpose the Results. 
                        var jsonString = response.Content.ReadAsStringAsync();
                        jsonString.Wait();

                        // if we provide an incorrect URL lets make sure that the code handles itself correctly. 
                        try
                        {
                            loResult = JsonConvert.DeserializeObject<PKandUpdateDate>(jsonString.Result);
                            Console.WriteLine("insContacttypesAsync : " + jsonString.Result);
                            Console.WriteLine("insContacttypesAsync : id= " + loResult.ID.ToString() + " - Message= " + loResult.ToString());

                            // I need to know the format for what hr3rest will be posting back or will it be in a header. 
                        }
                        catch (Exception ex)
                        {
                            lsResult = "Error : " + ex.Message;
                        }
                    }
                    else
                    {
                        switch (response.StatusCode)
                        {
                            case HttpStatusCode.NotFound:
                                lsResult = lsResult + "Error : /ContactType path failed with : 404 Not Found ";
                                break;
                            case HttpStatusCode.Forbidden:    //   AuthenticationFailed
                                // Sessiontoken possible failure  - attempt with a new one. 
                                lsResult = lsResult + "Error : AuthenticationFailed.";
                                break;
                            default:
                                lsResult = lsResult + string.Format("Error Message:{0}", response.StatusCode);
                                break; // Worked fine. 
                        }
                    }


                });

            };

            // task.Wait();  // wait on the last task ??
            return lsResult;

        }


        // The Problem with this is we need to find an Existing Item then update rather than just perform an update. 
        public string updContact_typesAsync()
        {
            PKandUpdateDate loResult = null;

            string lsResult = "";

            try
            {
                if (Common.Instance().SessionToken == "")
                {
                    Helperfunctions.RequestSessionToken(Common.Email, Common.Password, Common.DefaultAccessToken);
                }
            }
            catch (System.Exception e)
            {
                // throw new Exception(string.Format("Error Message:{0}", e.Message));
                return string.Format("Error Message:{0}", e.Message);
            }

            // id will be assigned and returned  
            // TODO: This needs to be rechecked as PK(9) is not acceptable. 
            ContactTypeInfo loItem01 = new ContactTypeInfo() { Contact_Type_Id = 17, Contact_Type_Desc = "Seventeen is 17" };

            var client = new HttpClient();
            var request = new System.Net.Http.HttpRequestMessage()
            {
                RequestUri = Helperfunctions.getURI("ContactTypes/"+loItem01.Contact_Type_Id.ToString()),  
                Method = HttpMethod.Put
            };

            // Setup Headers
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            // Header Details - Session token
            client.DefaultRequestHeaders.Add("cmkey", Common.Instance().SessionToken);
            // Who is the user of that is making the call. 
            client.DefaultRequestHeaders.Add("username", Common.Username);

            Console.WriteLine("updContacttypesAsync : Sending Asysnc");

            // Ok there is a couple ways of posting data I just need the items placed into Content. 
            var task = client.PutAsJsonAsync(request.RequestUri, loItem01).ContinueWith((taskwithmsg) =>
            {
                var response = taskwithmsg.Result;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    // We now need to read the response back as we may want the PK that was assigned to it. 
                    // Now transpose the Results. 
                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();

                    try
                    {
                        loResult = JsonConvert.DeserializeObject<PKandUpdateDate>(jsonString.Result);
                        Console.WriteLine("updContacttypesAsync : " + jsonString.Result);
                        Console.WriteLine("updContacttypesAsync : id= " + loResult.ID.ToString() + " - Message= " + loResult.Update_Date.ToString());

                        // todo: I need to know the format for what hr3rest will be posting back or will it be in a header. 
                    }
                    catch (Exception ex)
                    {
                        lsResult = "Error : " + ex.Message;
                    }
                }
                else
                {
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.NotFound:
                            lsResult = lsResult + "Error : /ContactTypes/2 path failed with : 404 Not Found ";
                            break;
                        case HttpStatusCode.Forbidden:    //   AuthenticationFailed
                            // Sessiontoken possible failure  - attempt with a new one. 
                            lsResult = lsResult + "Error : AuthenticationFailed.";
                            break;
                        default:
                            lsResult = lsResult + string.Format("Error Message:{0}", response.StatusCode);
                            break; // Worked fine. 
                    }
                }

            });

            task.Wait();
            return lsResult;

        }


        // updContact_typesAsync() returns an error 
        public string delContact_typesAsync()
        {
            // Use the service with the software GUID        
            //List<Ranges> Items = null;
            string lsResult = "";

            try
            {
                if (Common.Instance().SessionToken == "")
                {
                    Helperfunctions.RequestSessionToken(Common.Email, Common.Password, Common.DefaultAccessToken);
                }
                // if the token is not valid or expired an error will be raised,
                // which inturn means we will need to asking for a new token. 
            }
            catch (System.Exception e)
            {
                // throw new Exception(string.Format("Error Message:{0}", e.Message));
                return string.Format("Error Message:{0}", e.Message);
            }

            // id will be assigned and returned  
            ContactTypeInfo loItem02 = new ContactTypeInfo() { Contact_Type_Id = 18 };

            var client = new HttpClient();
            var request = new System.Net.Http.HttpRequestMessage()
            {
                RequestUri = Helperfunctions.getURI("ContactTypes/"+loItem02.Contact_Type_Id.ToString()),  

                Method = HttpMethod.Delete
            };

            // Setup Headers
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            // Header Details - Session token
            client.DefaultRequestHeaders.Add("cmkey", Common.Instance().SessionToken);
            // Who is the user of that is making the call. 
            client.DefaultRequestHeaders.Add("username", Common.Username);

            Console.WriteLine("delContacttypesAsync : Sending Asysnc");
            // todo: fire off a series of insert calls. 

            // Ok there is a couple ways of posting data I just need the items placed into Content. 
            var task = client.DeleteAsync(request.RequestUri).ContinueWith((taskwithmsg) =>
            {
                var response = taskwithmsg.Result;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    // We now need to read the response back as we may want the PK that was assigned to it. 
                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();

                    try
                    {
                        lsResult = JsonConvert.DeserializeObject<string>(jsonString.Result);
                        Console.WriteLine("delContacttypesAsync : " + lsResult);
                        lsResult = "";
                        //Console.WriteLine("delContacttypesAsync : id= " + loResult.Id.ToString() + " - Message= " + loResult.Message);
                    }
                    catch (Exception ex)
                    {
                        lsResult = "Error : " + ex.Message;
                    }
                }
                else
                {
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.NotFound:
                            lsResult = lsResult + "Error : /ContactTypes/ path failed with : 404 Not Found ";
                            break;
                        case HttpStatusCode.Forbidden:    //   AuthenticationFailed
                            // Sessiontoken possible failure  - attempt with a new one. 
                            lsResult = lsResult + "Error : AuthenticationFailed.";
                            break;
                        default:
                            lsResult = lsResult + string.Format("Error Message:{0}", response.StatusCode);
                            break; // Worked fine. 
                    }
                }

            });

            task.Wait();
            return lsResult;

        }


    } // class
}


