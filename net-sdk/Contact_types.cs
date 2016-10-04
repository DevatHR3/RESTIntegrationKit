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

    public class PostResult
    {
        public PostResult() { }

        public PostResult(string rmessage, int newId)
        {
            message = rmessage;
            id = newId;
        }

        public string message { get; set; }
        public int id { get; set; }
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
        protected string fsContact_type_id;


        // Perform a connection to wsPing - just so we can see the REST service is up and running. 
        // wsPing does not require Authorisation access. 
        public void wsPingURL()
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = Helperfunctions.getURI();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync("wsping").Result;
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception("/wsPing doesn't return expected value ");

                }

                // Test that the string = 'ping' was returned. 
                string testObject = (string)response.Content.ReadAsAsync<string>().Result;
                Console.WriteLine("wsPing() : " + testObject);

                // Test that we get Todays date. 
                if (!(testObject.Contains(DateTime.Now.ToString("yyyy-MM-dd"))))  // should look at todays date!
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
                    lsSessiontoken = Helperfunctions.RequestSessionToken(Common.Username, Common.Password, Common.DefaultAccessToken);
                }
                catch (System.Exception e)
                {
                    throw new Exception(string.Format("Error Message:{0}", e.Message));
                    // if token is invalid  - revoked access apply for a new token
                }

                // Header Details - software key 
                client.DefaultRequestHeaders.Add("cmkey", lsSessiontoken);
                // Who is the user of that is making the call. 
                //01 client.DefaultRequestHeaders.Add("username", Common.Username);
                // Both must be supplied otherwise call will fail with a 401. 

                // Doc: Request a List of all the Contact types that are available. 
                HttpResponseMessage response = client.GetAsync("ContactType").Result;
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return "/ContactType doesn't return expected value ";
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
                    if (Common.Instance().SessionToken == "")
                    {
                        Helperfunctions.RequestSessionToken(Common.Username, Common.Password, Common.DefaultAccessToken);
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
                client.DefaultRequestHeaders.Add("cmkey", Common.Instance().SessionToken);
                // Who is the user of that is making the call. 
                //01client.DefaultRequestHeaders.Add("username", Common.Username);
                // Both must be supplied otherwise call will fail with a 401. 

                //Doc: Request a List of all the Contact types that are available. 
                HttpResponseMessage response = client.GetAsync("contacttype/0").Result;

                switch (response.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return "/Contact_types path failed with : 404 Not Found ";
                    case HttpStatusCode.Forbidden:    //   AuthenticationFailed
                        // Sessiontoken possible failure  - attempt with a new one. 
                        Common.Instance().SessionToken = "";  // wipe session token 
                        Helperfunctions.RequestSessionToken(Common.Username, Common.Password, Common.DefaultAccessToken);

                        response = client.GetAsync("contacttype/0").Result;
                        break;
                    case HttpStatusCode.OK:
                        break; // Worked fine. 
                }
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return "/contacttype doesn't return expected value ";
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
                    Helperfunctions.RequestSessionToken(Common.Username, Common.Password, Common.DefaultAccessToken);
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
                RequestUri = Helperfunctions.getURI("contacttype/1,3,4,5"),
                Method = HttpMethod.Get
            };

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Header Details - Session token
            request.Headers.Add("cmkey", Common.Instance().SessionToken);
            // Who is the user of that is making the call. 
            request.Headers.Add("username", Common.Username);

            Console.WriteLine("getContacttypesAsync : Sending Asysnc");
            var task = client.SendAsync(request).ContinueWith((taskwithmsg) =>
            {
                var response = taskwithmsg.Result;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();

                    try
                    {
                        Items = JsonConvert.DeserializeObject<List<ContactTypeInfo>>(jsonString.Result);
                        Console.WriteLine("getContacttypesAsync : " + jsonString.Result);
                        for (int i = 0; i < Items.Count; i++)
                        {
                            Console.WriteLine("Item : " + Items[i].Contact_Type_Id + ", " + Items[i].Contact_Type_Desc);
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
                    Helperfunctions.RequestSessionToken(Common.Username, Common.Password, Common.DefaultAccessToken);
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
                RequestUri = Helperfunctions.getURI("contacttType/1"),
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
                        Console.WriteLine("Item : " + loItem.Contact_Type_Id + ", " + loItem.Contact_Type_Desc);

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
                            lsResult = lsResult + "Error : /contacttype/2 path failed with : 404 Not Found ";
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


        public string insContact_type()
        {
            ContactTypeInfo loItem = new ContactTypeInfo() 
            { 
                Contact_Type_Id = 0, 
                Contact_Type_Desc = "Serialized type", 
                Update_Date = null 
            };

            string lsResult = "";
            fsContact_type_id = "";
            // id will be assigned and returned  
            try
            {
                HttpResponseMessage response = Common.RequestaPOSTClient("ContactType", loItem);
                if (response != null)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Insert Contact type : Ok");

                        var jsonString = response.Content.ReadAsStringAsync();
                        jsonString.Wait();

                        PKandUpdateDate loResult = JsonConvert.DeserializeObject<PKandUpdateDate>(jsonString.Result);

                        Console.WriteLine("Insert Contact type : " + jsonString.Result);
                        Console.WriteLine("Insert Contact type : id= " + loResult.ID.ToString() + " - Message= " + loResult.ToString());

                        fsContact_type_id = loResult.ID.ToString();

                    }
                    else
                    {
                        switch (response.StatusCode)
                        {
                            case HttpStatusCode.BadRequest:
                                // Error has occured read the message
                                var jsonString = response.Content.ReadAsStringAsync();
                                jsonString.Wait();

                                // Read the result we want to read the message as to what went wrong. 
                                PostResult loResult = JsonConvert.DeserializeObject<PostResult>(jsonString.Result);
                                return string.Format("Insert contact type : Returned a Badrequest Message {0} ", loResult.message);
                            default:
                                return string.Format("Insert contact type : Returned a Status of {0}", response.StatusCode); // Worked fine.                     
                        }
                        // throw new Exception(asUri + string.Format(" doesn't return expected value. Returned a Status of {0} ",response.StatusCode) );

                    }
                }
            }
            catch (System.Exception e)
            {
                // throw new Exception(string.Format("Error Message:{0}", e.Message));
                return string.Format("Error Message:{0}", e.Message);
            }

            return lsResult;

        }

        

        // The Problem with this is we need to find an Existing Item then update rather than just perform an update. 
        public string updContact_typesAsync()
        {
            PKandUpdateDate loResult = null;

            string lsResult = "";

            if (fsContact_type_id == "") {
                return "Error Message: Contact_type 14 id is not available.";
            }

            try
            {
                if (Common.Instance().SessionToken == "")
                {
                    Helperfunctions.RequestSessionToken(Common.Username, Common.Password, Common.DefaultAccessToken);
                }
            }
            catch (System.Exception e)
            {
                // throw new Exception(string.Format("Error Message:{0}", e.Message));
                return string.Format("Error Message:{0}", e.Message);
            }

            // id will be assigned and returned  
            // TODO: This needs to be rechecked as PK(9) is not acceptable. 
            ContactTypeInfo loItem01 = new ContactTypeInfo() { Contact_Type_Id = Int32.Parse(fsContact_type_id), Contact_Type_Desc = "Type is" };

            var client = new HttpClient();
            var request = new System.Net.Http.HttpRequestMessage()
            {
                RequestUri = Helperfunctions.getURI("ContactType"),
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
                            lsResult = lsResult + "Error : /contacttype/2 path failed with : 404 Not Found ";
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


        // delContact_typesAsync() returns an error 
        /// <summary>
        ///  Delete any of the fsCTXX_id vars that have been set. 
        ///  
        /// </summary>
        /// <returns></returns>
        public string delContact_typesAsync()
        {
            string lsError = "";

            if (fsContact_type_id != "") {
                lsError = delContact_typesEach(fsContact_type_id);
            }
            
            return lsError;
        }

        public string delContact_typesEach(string Value)
        {
            // Use the service with the software GUID        
            string lsResult = "";

            // To perform a Delete we need to know what record we are deleting. 
            // Insert has put aside the PK that we created earlier. 
            Console.WriteLine("Delete Contact type : Attempting ");

            try
            {
                // DELETE /api/v1-0/contact/{contactId}/phones/{contactPhoneId}
                HttpResponseMessage response = Common.RequestaDeleteClient(string.Format("contacttype/{0}", Value));
                if (response != null)
                {
                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();

                    PostResult loResult = JsonConvert.DeserializeObject<PostResult>(jsonString.Result);

                    Console.WriteLine("Delete Contact type : " + jsonString.Result);
                    Console.WriteLine("Delete Contact type : id= " + loResult.id.ToString() + " - Message= " + loResult.message);
                }
            }
            catch (System.Exception e)
            {
                // throw new Exception(string.Format("Error Message:{0}", e.Message));
                return string.Format("Error Message:{0}", e.Message);
            }

            return "";

        }


        // call is contact_type
        /*
         * The call is to request what is the lastest timestamp of the last modify record that is stored for this record type.
         *
         * Scenario, I have the timestamp of 2012-11-25 06:00(ldtLastupdated) stored from the last time I performed a sync process.
         * Make the call.
         * ie:  contact_types/update_date   
         *  Returns the value "2012-12-20 06:30"
         *  if ldtLastupdated = "2012-12-20 06:30"
         *     Nothing to update
         *  else
         *     // call to get a filtered list based on updated date.
         *     Contacttype/updateddata?lastUpdatedDt=2012-11-25

         *     // Now update or ammend new records.
         *
        */
        public string get_Contact_type_update_date()
        {
            // Use the service with the software GUID       
            Update_datedInfo loItem = null;
            string lsResult = "";

            // Assuming Australian DD-MM-YYYY
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-AU", true);
            culture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            DateTime ldtLastUpdated = Convert.ToDateTime("25/11/2012", culture);

            var client = new HttpClient();
            var request = new System.Net.Http.HttpRequestMessage()
            {
                RequestUri = Helperfunctions.getURI("contacttype/update_date"),
                Method = HttpMethod.Get
            };
            // /Hr3RestService/getContacttype/1
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Header Details - Session token
            request.Headers.Add("cmkey", Common.Instance().SessionToken);
            // Who is the user of that is making the call.
            request.Headers.Add("username", Common.Username);

            Console.WriteLine("Contact type : Sending Asysnc");
            var task = client.SendAsync(request).ContinueWith((taskwithmsg) =>
            {
                var response = taskwithmsg.Result;

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();

                    // if we provide an incorrect URL lets make sure that the code handles itself correctly.
                    try
                    {

                        loItem = JsonConvert.DeserializeObject<Update_datedInfo>(jsonString.Result);

                        if (loItem.Update_Date >= ldtLastUpdated)
                        {
                            // No nothing
                        }
                        else
                        {
                            // call to get a filtered list based on updated date.
                            get_Contact_type_on(loItem.Update_Date);
                            // Now update or ammend new records.
                        }

                        //loItem = JsonConvert.DeserializeObject<ContactTypeInfo>(jsonString.Result);
                        //Console.WriteLine("Contact_types : " + jsonString.Result);
                        //Console.WriteLine("Item : " + loItem.Contact_Type_Id.ToString() + ", " + loItem.Contact_Type_Desc);

                    }
                    catch (Exception ex)
                    {
                        //Console.WriteLine("Error reading converting json data " + ex.Message);
                        lsResult = "Error : " + ex.Message;
                    }

                }

            });

            task.Wait();
            return lsResult;
        }

        // GET_Contact_type_on(
        public string get_Contact_type_on(DateTime? adtLastUpdated)
        {
            //     // call to get a filtered list based on updated date.
            //     contact_types?sort=update_date&update>=2012-11-25
            //     // Now update or ammend new records.
            // Use the service with the software GUID       
            List<ContactTypeInfo> Items = null;

            string lsResult = "";
            string lsURL = "";


            // Pass in a format that the REST would like.  Json IS08601 Format? 
            // Datetime can be accepted a number of formats but I am going to do iso8601 format.

            // GET /api/v1-0/contacttype/updateddata
            lsURL = String.Format("Contacttype/updateddata?lastUpdatedDt={0:yyyy-MM-ddTHH:mm} ", adtLastUpdated);
            var client = new HttpClient();
            var request = new System.Net.Http.HttpRequestMessage()
            {
                RequestUri = Helperfunctions.getURI(lsURL),
                Method = HttpMethod.Get
            };
            // /Hr3RestService/getcontacttype/1
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Header Details - Session token
            request.Headers.Add("cmkey", Common.Instance().SessionToken);
            // Who is the user of that is making the call.
            request.Headers.Add("username", Common.Username);

            Console.WriteLine("Contact_types : Sending Asysnc");
            var task = client.SendAsync(request).ContinueWith((taskwithmsg) =>
            {
                var response = taskwithmsg.Result;

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();

                    // if we provide an incorrect URL lets make sure that the code handles itself correctly.
                    try
                    {
                        Items = JsonConvert.DeserializeObject<List<ContactTypeInfo>>(jsonString.Result);
                        Console.WriteLine("Contact_types : " + jsonString.Result);
                        for (int i = 0; i < Items.Count; i++)
                        {
                            Console.WriteLine("Item : " + Items[i].Contact_Type_Id.ToString() + ", " + Items[i].Contact_Type_Desc);
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error reading converting json data " + ex.Message);
                        lsResult = "Error : " + ex.Message;
                    }

                }

            });

            task.Wait();

            return lsResult;
        }


    } // class
}


