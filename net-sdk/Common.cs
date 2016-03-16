using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR3Weblinks.Examples
{
    public enum DeviceOS
    {
        Android,
        IOS,
        WinPhone,
        Win8,
        Web
    }
    

    /*
     * Troubleshooting 
     *  - Issue : Invalid User Name when attempting to login - then verify the details below that they match in HR3Pay. 
     *    ie: Hilary Hackman email and password matches. 
     *    
     * 
     * */

    public class Common
    {    
        // Employee Access 
        // The Defaultaccesstoken is generated from inside HR3Pay \System Preferences \ Webservices 
        public const string DefaultAccessToken = "{6AB07A2F-E677-4DEC-BD3A-513DBF176F3F}";  // Access token for Hilary Hackman in Sample db. 
        public const string Email = "hilary@bumbles.com.au";   // Sample db - Employee Hilary Hackman  -     
        public const string Password = "hilary";   // Sample db - Employee Hilary Hackman - current password. 
        public const string Username = "hilary";     // Sample db - used to identify who is accessing. 

        // Default Administrator Access
        /*
                // Employee Access 
                public const string DefaultAccessToken = "{238E66C3-643F-452A-BD42-5D6CFFB22872}";   // Access token for Administrator 
                public const string Email = "Administrator";   // Sample db - 
                public const string Password = "";   // Sample db - 
                public const string Username = "Administrator";     // Sample db - used to identify who is accessing. 
        */

        // Default Guest Access
        /*
                // Guest Access 
                public const string DefaultAccessToken = "{07233FD7-2A3C-47DF-AD3F-9099D2A96E74}";   // Access token for guest
                public const string Email = "Guest";   // Sample db - 
                public const string Password = "password";   // Sample db - 
                public const string Username = "Guest";     // Sample db - used to identify who is accessing. 
        */

        public const string localhost = "http://localhost:59595/api/v1.0/";
        // public const string localhost = "http://<Server>/Hr3Restservice/api/v1.0/contact_types/1";

        // singleton var 
        public static Common oCommon = null;

        public static Common Instance()
        {
            Common result;
            if (!(Common.oCommon != null))
            {
                Common.oCommon = new Common();
            }
            result = Common.oCommon;
            return result;
        }


        public string SessionToken  { get; set; }

    }

    


}
