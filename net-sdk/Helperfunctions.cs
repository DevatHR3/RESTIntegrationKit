using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
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
    
    public static class Helperfunctions
    {
        /// <summary>
        /// Returns a List<string> of the tables column names
        /// </summary>
        /// <param name="sqlCon"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static IDictionary<string, string> GetColumnNames(SqlConnection sqlCon, string tableName)
        {
            var results = new Dictionary<string, string>();

            using (var sqlCmd = sqlCon.CreateCommand())
            {
                try
                {
                    // No data wanted, only schema
                    sqlCmd.CommandText = "SELECT TOP 0 * FROM " + tableName;
                    sqlCmd.CommandType = CommandType.Text;

                    using (var reader = sqlCmd.ExecuteReader())
                    {
                        var dataTable = reader.GetSchemaTable();

                        string colName = "";
                        string colType = "";
                        foreach (DataRow row in dataTable.Rows)
                        {
                            colName = row.Field<string>("ColumnName");
                            colType = row.Field<string>("DataTypeName");

                            results.Add(colName, colType + "(" + row.Field<int>("ColumnSize") + ")");
                        }
                    }

                }
                catch (Exception)
                {

                }
            }

            return results;
        }

        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits
                //for each byte
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }

        public static Uri getURI(string Extra = "")
        {
            return new Uri(Common.localhost + Extra);
        }


        // perform login with details 
        // Steps on Usage 
        // Set User asking for a nonce with a provided access token, in return we can hash the password with. 
        // 
        // Autho Password Encrypted on temp token and cmkey. 

        //public string RequestSessionToken( "test@bumblebee.com.au", Common.Password, Common.DefaultAccessToken); IsValidUser(string Email, string Password ) 
        public static string RequestSessionToken( string Email, string Password, string DefaultAccessToken ) 
        {
            // Reference 
            //        public string RequestSessionToken(string userName, string nonce, string userCredentials = "")

            // Initial token provided for General Access.   - this can be a provided key as well. 
            // The user can use a cmkey or a provided Key to login initially, typically a general access key.  (systemsetting windows gen'd one can work as well.)

            //string lscmkey = "{9C638998-D441-478C-97BB-049794709CE1}";
            string lsNonce = "";
            string lsResult = "";

            var client = new HttpClient();
            var request = new System.Net.Http.HttpRequestMessage()
            {
                // URL's 
                // "RequestSessionToken?username={userName}&Nonce={nonce}&password={userCredentials}")
                //RequestUri = new Uri("http://localhost:9990/RequestSessionToken?username={userName}&Nonce={nonce}&password={userCredentials}"),   
                //RequestUri = getURI("RequestUserAuthorised?username=hilary.hackman@bumblebee.com.au&"+lscmkey),   
                Method = HttpMethod.Get
            };
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                // Returned Key to hash against     // "hilary.hackman@bumblebee.com.au"
                // RestService.cs - RequestUserAuthorised?username={userName}&cmkey={cmkey}"  

                // First part Login to see if Hilary exists and request an initial access token.                                 
                HttpResponseMessage response = client.GetAsync(Helperfunctions.getURI("RequestUserAuthorised?username=" + Email + "&cmkey=" + DefaultAccessToken)).Result;
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception("Unknown user.");
                }
                else
                {
                    // Returns a nonce.   
                    lsNonce = (string)response.Content.ReadAsAsync<string>().Result;
                    if (lsNonce == "")
                    {
                        throw new Exception("Nonce has failed to return a valid response.");
                    }

                    // Hashed Password 
                    string lsHashed = MD5Hash(lsNonce + Password + DefaultAccessToken);

                    // Set the Default Token in the Header.
                    client.DefaultRequestHeaders.Add("cmkey", DefaultAccessToken);

                    // IsUserAuthorised - Perform Login  
                    //[WebGet(UriTemplate = "RequestSessionToken?username={userName}&Nonce={nonce}&password={userCredentials}")]
                    response = client.GetAsync(getURI("RequestSessionToken?username=" + Email + "&Nonce=" + lsNonce + "&password=" + lsHashed)).Result;
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        // What is the error msg. 
                        lsResult = (string)response.Content.ReadAsAsync<string>().Result;
                        // Log accordingly. 

                        throw new Exception("Login failed to exectue. "+lsResult);
                    }
                    else
                    {
                        //  get the New session token that is available due to logining successfully 
                        string lsToken = ""; // response 
                        lsToken = (string)response.Content.ReadAsAsync<string>().Result;
// Problem here as its coming back as failed when Employee is correct check the email 
                        // Once have a Token set it in the Common. 
                        Common.Instance().SessionToken = lsToken; 

                        return lsToken; // it will also be returned. 
                    }
                }
            }
            catch (System.Exception e)
            {
                throw new Exception(string.Format("Error Message:{0}", e.Message));
            }
        }


        // Create a Refreshtoken func 
   /*
        Set this up for a refresh 
    * 
        public static string RequestSessionToken(string Email, string Password, string DefaultAccessToken)
        {
            // Reference 
            //        public string RequestSessionToken(string userName, string nonce, string userCredentials = "")

            // Initial token provided for General Access.   - this can be a provided key as well. 
            // The user can use a cmkey or a provided Key to login initially, typically a general access key.  (systemsetting windows gen'd one can work as well.)

            //string lscmkey = "{9C638998-D441-478C-97BB-049794709CE1}";
            string lsNonce = "";

            var client = new HttpClient();
            var request = new System.Net.Http.HttpRequestMessage()
            {
                // URL's 
                // "RequestSessionToken?username={userName}&Nonce={nonce}&password={userCredentials}")
                //RequestUri = new Uri("http://localhost:9990/RequestSessionToken?username={userName}&Nonce={nonce}&password={userCredentials}"),   
                //RequestUri = getURI("RequestUserAuthorised?username=hilary.hackman@bumblebee.com.au&"+lscmkey),   
                Method = HttpMethod.Get
            };
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                // Returned Key to hash against     // "hilary.hackman@bumblebee.com.au"
                // RestService.cs - RequestUserAuthorised?username={userName}&cmkey={cmkey}"  

                // First part Login to see if Hilary exists and request an initial access token. 
                HttpResponseMessage response = client.GetAsync(Helperfunctions.getURI("RequestUserAuthorised?username=" + Email + "&" + DefaultAccessToken)).Result;
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Unknown user.");
                }
                else
                {
                    // Returns a nonce.   
                    lsNonce = (string)response.Content.ReadAsAsync<string>().Result;
                    if (lsNonce == "")
                    {
                        throw new Exception("Nonce has failed to return a valid response.");
                    }

                    // Hashed Password 
                    string lsHashed = MD5Hash(lsNonce + Password + DefaultAccessToken);

                    // IsUserAuthorised - Perform Login  
                    //[WebGet(UriTemplate = "RequestSessionToken?username={userName}&Nonce={nonce}&password={userCredentials}")]
                    response = client.GetAsync(getURI("RequestSessionToken?username=" + Email + "&Nonce=" + lsNonce + "&password=" + lsHashed)).Result;
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception("Login failed to exectue. ");
                    }
                    else
                    {
                        //  get the New session token that is available due to logining successfully 
                        string lsToken = ""; // response 

                        lsToken = (string)response.Content.ReadAsAsync<string>().Result;

                        //return lsToken;

                        // Now if the Token doesn't work then perform a IsValidSessionToken() 


                        // Now set the Header so as to  
                        // Header Details - software key 
                        request.Headers.Add("token", lsToken);

                        // IsSessionValid - is verifying that the header token is still valid 
                        response = client.GetAsync(getURI("isvalidsessiontoken?username=" + Email + "&token=" + lsToken)).Result;
                        if (!response.IsSuccessStatusCode)
                        {
                            throw new Exception("IsSessionValid threw an error. ");
                        }
                        else
                        {
                            // Ok then return with the lsToken

                            // if you need to perform a Refersh the following will help 

                            // Call  RefreshSession(lsSessiontoken);
                            // refreshToken?username={userName}&token={sessionToken}")]
                            // make the call and make sure its a refreshed token. 
                            response = client.GetAsync(getURI("refreshToken?username=" + Email + "&token=" + lsToken)).Result;
                            if (!response.IsSuccessStatusCode)
                            {
                                throw new Exception("IsSessionValid threw an error. ");
                            }
                            else
                            {
                                // New token 
                                string lsNewToken = (string)response.Content.ReadAsAsync<string>().Result;
                                if (lsToken == lsNewToken)
                                {
                                    throw new Exception("Expecting a new session token from Refreshtoken.");
                                }

                                // Finally Logout. 
                            }

                        }

                    }
                }
            }
            catch (System.Exception e)
            {
                throw new Exception(string.Format("Error Message:{0}", e.Message));
            }
        }

*/

        //public static string SqlDataType(FormElementType elementType)
        //{
        //    return SqlTypeMap[elementType];
        //}

        //public static Type DataType(FormElementType elementType)
        //{
        //    return DataTypeMap[elementType];

        //}


    }
}
