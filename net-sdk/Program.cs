using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR3Weblinks.Examples
{
    /// <summary>
    /// The Calls are just using HttpClient 
    /// 
    ///  2016-03-10 : Employee Onboarding - known issue with serializing of Datetime. 
    ///  
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            string lsError = "";

            // WARNING make sure common.cs - localhost is pointing to the correct address. 

            Console.WriteLine("HR3Weblinks Examples Started.");

            Common.Instance();  // Storage for Sessiontoken. 

            // Uncomment what you wish to look at 

            // contact_types
            // A simple example on  ...
            Contact_types_class loContact_types = new Contact_types_class();

            // Several basic functions that don't need Authorisation. 
            loContact_types.wsPingURL();
            Console.WriteLine("-");
            loContact_types.wsBuildNoURL();
            Console.WriteLine("-");

            // Authoristation Handshake to perform a GET
            Console.WriteLine("getAuthoContact_types - Attempting. ");
            lsError = loContact_types.getAuthoContact_types();
            if (lsError != "")
            {
                Console.WriteLine("Error getAuthoContact_types() : " + lsError);
            }

            Console.WriteLine("-");
            Console.WriteLine("getContacttypes - Attempting. ");

            // Using an existing Sessiontoken perform.         
            loContact_types.getContact_types();
            if (lsError != "")
            {
                Console.WriteLine("Error getContacttypes() : " + lsError);
            }

            Console.WriteLine("-");
            Console.WriteLine("getContact_typesAsync - Attempting. ");
            // Returns a set of Contact types
            loContact_types.getContact_typesAsync();
            if (lsError != "")
            {
                Console.WriteLine("Error getContact_typesAsync() : " + lsError);
            }

            Console.WriteLine("-");
            Console.WriteLine("getaContact_typeAsync - Attempting. ");
            // Returns a set of Contact types
            loContact_types.getaContact_typeAsync();
            if (lsError != "")
            {
                Console.WriteLine("Error getContact_typeAsync() : " + lsError);
            }

            Console.WriteLine("-");


            Console.WriteLine("insContact_type - Attempting. ");

            lsError = loContact_types.insContact_type();
            if (lsError != "")
            {
                Console.WriteLine("Error insContact_type()() : " + lsError);
            }

            Console.WriteLine("-");

            Console.WriteLine("insContact_typesAsync - Attempting. ");

            lsError = loContact_types.insContact_typesAsync().Result;
            if (lsError != "")
            {
                Console.WriteLine("Error insContact_typesAsync() : " + lsError);
            }

            Console.WriteLine("-");

            Console.WriteLine("insContact_typesAsync02 - Attempting. ");
            lsError = loContact_types.insContact_typesAsync02();
            if (lsError != "")
            {
                Console.WriteLine("Error insContact_typesAsync02() : " + lsError);
            }

            Console.WriteLine("-");
            Console.WriteLine("updContact_typesAsync - Attempting. ");

            lsError = loContact_types.updContact_typesAsync();
            if (lsError != "")
            {
                Console.WriteLine("Error updContact_typesAsync() : " + lsError);
            }

            Console.WriteLine("-");

            Console.WriteLine("delContact_typesAsync - Attempting. ");
            lsError = loContact_types.delContact_typesAsync();
            if (lsError != "")
            {
                Console.WriteLine("Error delContact_typesAsync() : " + lsError);
            }

/*
            // Employee Onboard process.
            Employee_Onboard_class loEmployee_Onboard = new Employee_Onboard_class();


            //lsError = loEmployee_Onboard.insEOB();
            lsError = loEmployee_Onboard.insEmployee_Onboard_E06().Result;
            if (lsError != "")
            {
                Console.WriteLine("Error insEmployee_Onboard_E06() : " + lsError);
            }
*/


            Console.WriteLine("HR3Weblinks Examples Completed.");
            Console.ReadKey();

        }
    }
}
