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
using System.Security.Cryptography;
using Newtonsoft.Json;
using System.Text.RegularExpressions;


namespace HR3Weblinks.Examples
{

    ///  2016-03-10 : Employee Onboarding - known issue with serializing of Datetime. 

    // Employe OnBoard structure to needed to Import a fully qualified Employee. 
    public class EmployeeOnBoardInfo
    {
        public EmployeeOnBoardInfo() { }

        public int employee_details_Id { get; set; }
        public string empno { get; set; }
        
        public string archived { get; set; }
        //
        //public DateTime? termdate { get; set; }
        public string surname { get; set; }
        public string firstname { get; set; }

        //for version 1.2.4.1, the custome dont need hireddate,but in the furture it may need to be back.
        //public DateTime? HiredDate { get; set; }
        
        public DateTime? hireddate { get; set; }
        
        public DateTime? update_date { get; set; }

        //public string employee_details_id { get; set; }
        
        public string pay_points { get; set; }
        public int? pay_points_id { get; set; }
        
        public string suspend_reasons { get; set; }
        public int? suspend_reasons_id { get; set; }
        
        public string departments { get; set; }
        public int? departments_id { get; set; }
        
        public string classifications { get; set; }
        public int? classifications_id { get; set; }
        
        public string employment_status { get; set; }
        public int? employment_status_id { get; set; }
        
        public string asco { get; set; }
        public int? asco_id { get; set; }
        
        public string tax_header { get; set; }
        public int? tax_header_id { get; set; }
        
        public string pay_periods { get; set; }
        public int? pay_periods_id { get; set; }
        
        public string template_header { get; set; }
        public int? template_header_id { get; set; }
        
        public string empcond_header { get; set; }
        public int? empcond_header_id { get; set; }
        
        public string nationality { get; set; }
        public int? nationality_id { get; set; }
        
        public string workerscomp_class { get; set; }
        public int? workerscomp_class_id { get; set; }
        
        public string title { get; set; }
        public int? title_id { get; set; }
        
        public string initial { get; set; }
        
        public string printname { get; set; }
        
        public string preferredname { get; set; }
        
        public string prevsurname { get; set; }
        
        public string prevfirstname { get; set; }
        
        public string previnitial { get; set; }
        
        public int employee_level { get; set; }
        
        public string remotelogin { get; set; }
        
        public string employee_password { get; set; }
        
        public string passquestion { get; set; }
        
        public string passanswer { get; set; }
        
        public string employee_suspend { get; set; }
        
        public string apprentice { get; set; }
        
        public string accredtrainee { get; set; }
        
        public string cdep { get; set; }
        
        public string sex { get; set; }
        
        public DateTime? birthdate { get; set; }
        
        public string driverslic { get; set; }
        
        public string licenseno { get; set; }
        
        public string smoker { get; set; }
        
        public string taxavg { get; set; }
        
        public string taxexempt { get; set; }
        
        public decimal zonereb { get; set; }
        
        public string resident { get; set; }
        
        public string basisofpayment { get; set; }
        
        public string taxfreeclaimed { get; set; }
        
        public string hecs { get; set; }
        
        public string sfss { get; set; }
        
        public string annuityrebate { get; set; }
        
        public string signaturepresent { get; set; }
        
        public DateTime? declareddate { get; set; }
        
        public string taxvariation { get; set; }
        
        public DateTime? taxvarstart { get; set; }
        
        public DateTime? taxvarfinish { get; set; }
        
        public decimal taxvarrate { get; set; }
        
        public string spouse { get; set; }
        
        public int numberdep { get; set; }
        
        public string taxfileno { get; set; }
        
        public string taxtype { get; set; }
        
        public string ppsvarno { get; set; }
        
        public string paytype { get; set; }
        
        public decimal stdhours { get; set; }
        
        public decimal stdrate { get; set; }
        
        public string rdo { get; set; }
        
        public string passport { get; set; }
        
        public DateTime? passexpires { get; set; }
        
        public string autopay { get; set; }
        
        public decimal autoamt { get; set; }
        
        public decimal warnif1 { get; set; }
        
        public decimal warnif2 { get; set; }
        
        public decimal sacrifice { get; set; }
        
        public decimal salary { get; set; }
        
        public decimal defaultoncost { get; set; }
        
        public string acrrueleave { get; set; }
        
        public string tfnnot { get; set; }
        
        public decimal ftbamount { get; set; }
        
        public string zonerebate { get; set; }
        
        public string emailpayslip { get; set; }
        
        public string empabn { get; set; }
        
        public decimal withholdrate { get; set; }
        
        public string paynote { get; set; }
        
        public string centrelink_registered { get; set; }
        
        public string centrelink_consent { get; set; }
        
        public string centrelink_crn { get; set; }
        
        public string active_in_external { get; set; }
        
        public int timeclock_identifier { get; set; }
        
        public string tfnnz { get; set; }
        
        public string payrolltax { get; set; }
        
        public string payrolltax_state { get; set; }
        public int? payrolltax_state_id { get; set; }
        
        public string pay_period_cycle_set { get; set; }
        public int? pay_period_cycle_set_id { get; set; }
        
        public string security_group { get; set; }
        public int? security_group_id { get; set; }
        
        public string company_code { get; set; }
        
        public string position { get; set; }
        
        public string position_identifier { get; set; }
        
        public decimal position_incumbent_fte { get; set; }
        
        public string address_line_1 { get; set; }
        
        public string address_line_2 { get; set; }
        
        public string suburb { get; set; }
        
        public string postcode { get; set; }
        
        public string phone_no { get; set; }
        
        public string phone_no_ah { get; set; }
        
        public string phone_no_mob { get; set; }
        
        public string bsb { get; set; }
        
        public string account_no { get; set; }
        
        public string account_name { get; set; }
        
        public string lodgeref { get; set; }
        
        public string pay_item_code { get; set; }
        
        public string super_fund { get; set; }
        
        public string super_fund_member_no { get; set; }
        
        public string Email { get; set; }
        
        public string Kiosk_Login { get; set; }

        //public DateTime? Update_Date { get; set; }
    }

    public class Employee_Onboard_class
    {

        /// <summary>
        ///  NOTE: The data is setup specificly against the SqlSample database, company "Bumble Bee".
        ///        That is all the company related items are related to this company only. 
        /// 
        /// </summary>
        /// <returns></returns>
        public EmployeeOnBoardInfo Populate_employee_Onboard()
        {
            EmployeeOnBoardInfo loRec = new EmployeeOnBoardInfo();

            //    - company code needs to be set. 

            loRec.employee_details_Id = 0;  // PK for new Employee should be 0. 
            loRec.company_code = "BBE";   // Required Field - we must know what company that the Employee is being assigned to. 
            loRec.empno = "ABC001";    // Alphanumeric - upto 6 chars 

            loRec.archived = "F";      // Should be false 
            loRec.surname = "Xexxxx";  // Surname of the Employee

            loRec.firstname = "Testy"; // Fistname of the Employee 

            loRec.hireddate = DateTime.UtcNow; // Hireddate - Date the Employee commenced Employment. 
            loRec.update_date = null;   // dont set this. Internal datetime the record was last updated.         

            // A category for employees being paid.
            loRec.pay_points = "Head Office";   // Must match existing Company Pay Points. 

            // By default onboarding process all new employees need to be set to 
            //    "New employee - awaiting final check" 
            loRec.suspend_reasons = "New employee - awaiting final check";  // Reason a employee has been suspended.   
            // Part of Costing structure, every Employee belongs 
            loRec.departments = "VICADM"; // Must match an existing Company related Department.  Code/Description ?  
            loRec.classifications = "Management";   // Must match existing value in the Classification lookup table.
            loRec.employment_status = "Casual";     // Casual / Full Time /  Part time
            loRec.asco = "";   // 
            loRec.tax_header = "Tax Free Threshold Not Claimed";    // Must match an existing Company related Tax scale
            loRec.pay_periods = "Weekly";     // Weekly / Fortnighly / Monthly / ... 
            loRec.template_header = "";       // Must match an existing Company related template
            loRec.empcond_header = "Casual (VIC)";    // Must match an existing Company related Employment condition.  
            loRec.nationality = "Australia";               // Country match
            loRec.workerscomp_class = "AMP Workers Compensation Division"; // Must match an existing Company related Worker Compensation Insurers 
            loRec.title = "Mr";          // Must match existing value in the Titles lookup table.
            loRec.initial = "A";
            loRec.printname = "";       // Can be assembled in the date entry window  or made up of firstname Surname Initials
            loRec.preferredname = "";     // alternative name
            loRec.prevsurname = "";
            loRec.prevfirstname = "";
            loRec.previnitial = "";
            loRec.employee_level = 0;   // Security Level 
            loRec.remotelogin = "T";       // Enabled Employee login. 
            loRec.employee_password = "password";   
            loRec.passquestion = "Default Password";
            loRec.passanswer = "Default";
            loRec.employee_suspend = "F";
            loRec.apprentice = "F";
            loRec.accredtrainee = "F";
            loRec.cdep = "F";
            loRec.sex = "M";

            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-AU", true);
            culture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            loRec.birthdate = Convert.ToDateTime("25/11/1975", culture);

            loRec.driverslic = "T";
            loRec.licenseno = "";
            loRec.smoker = "F";
            loRec.taxavg = "F";
            loRec.taxexempt = "F";
            loRec.zonereb = 0.0m;
            loRec.resident = "T";
            loRec.basisofpayment = "F";
            loRec.taxfreeclaimed = "T";
            loRec.hecs = "F";
            loRec.sfss = "F";
            loRec.annuityrebate = "F";
            loRec.signaturepresent = "T";
            loRec.declareddate = DateTime.UtcNow;
            loRec.taxvariation = "F";
            loRec.taxvarstart = null;
            loRec.taxvarfinish = null;
            loRec.taxvarrate = 0.0m;
            loRec.spouse = "T";
            loRec.numberdep = 1;
            loRec.taxfileno = "111111111";  // Applied
            loRec.taxtype = "I";
            loRec.ppsvarno = "";
            loRec.paytype = "E";
            loRec.stdhours = 38.0m;
            loRec.stdrate = 25.35m;
            loRec.rdo = "T";
            loRec.passport = "T";
            loRec.passexpires = null;
            loRec.autopay = "F";
            loRec.autoamt = 0.0m;
            loRec.warnif1 = 0.0m;
            loRec.warnif2 = 0.0m;
            loRec.sacrifice = 0.0m;
            loRec.salary = 0.0m;
            loRec.defaultoncost = 0.0m;
            loRec.acrrueleave = "T";
            loRec.tfnnot = "T";
            loRec.ftbamount = 0.0m;
            loRec.zonerebate = "F";
            loRec.emailpayslip = "T";
            loRec.empabn = "";
            loRec.withholdrate = 0.0m;
            loRec.paynote = "";
            loRec.centrelink_registered = "F";
            loRec.centrelink_consent = "F";
            loRec.centrelink_crn = "";
            loRec.active_in_external = "T";
            loRec.timeclock_identifier = 0;
            loRec.tfnnz = "F";
            loRec.payrolltax = "F";
            loRec.payrolltax_state = "";   // Must match an existing Company related States
            loRec.pay_period_cycle_set = "";   // Must match an existing Company related Pay Period cycle set 
            loRec.security_group = "";   // Must match an existing Company related Security Groups
            loRec.position = "";     // HR Postion Information. 
            loRec.position_identifier = "";
            loRec.position_incumbent_fte = 0.0m;

            // Address Details
            loRec.address_line_1 = "1 Test Pl";
            loRec.address_line_2 = "";
            loRec.suburb = "Melbourne";
            loRec.postcode = "3000";
            loRec.phone_no = "5551234";
            loRec.phone_no_ah = "";
            loRec.phone_no_mob = "";

            // Banking Details
            loRec.bsb = "731051";   // Bank state branch
            loRec.account_no = "12345";
            loRec.account_name = "Testy Acct";
            loRec.lodgeref = "Pay";

            loRec.pay_item_code = "P01";    // Must match an existing Company related Pay item (Payments)
            loRec.super_fund = "AMP";            // Must match an existing Company related Superfund
            loRec.super_fund_member_no = "12345";

            loRec.Email = "testya@bumbles.com.au";
            loRec.Kiosk_Login = "T";

            return loRec;
        }


        // Issue we are getting a Bad Request (Bad Json) serializing EmployeeOnBoardInfo 
        // so we will create a Json to post. 

        public string insEOB() 
        {
            EmployeeOnBoardInfo loType01 = new EmployeeOnBoardInfo() ;
            loType01.empno = "TEST04";
            loType01.title =       "Ms";   
            loType01.firstname =    "Pig A";  
            loType01.preferredname =   "";   
            loType01.surname = "la";  
            loType01.Email =      "pc1@ttt.com.au"; 
//            loType01.emailOther =  "p1c@email.org.au";      // This doesn't exist this is a mistake. 
            loType01.sex = "M";   
            loType01.company_code = "BBE";   
            loType01.pay_points = "Head Office";   
//            loType01.hireddate = "\/Date(1440093600000+1000)\/";
            loType01.taxfileno = "11111111111";   
            loType01.departments = "VICAC";   
            loType01.empcond_header = "Non-Award (VIC)";  
            loType01.tax_header = "2";   
            loType01.classifications = "Management";   
            loType01.employment_status = "Full Time";
            loType01.pay_periods = "Monthly";   
            loType01.pay_item_code = "P01";    
            loType01.super_fund = "AXA";   
            loType01.position = "OHS Manager";


            string str = JsonConvert.SerializeObject(loType01);

            string targetUri = Helperfunctions.getURI("EmployeeDetails/onboard").ToString(); // "http://localhost:64398/Hr3RestService/EmployeeDetails/onboard/datasource?format=json";
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

        
        // TODO: Show what happens if something is wrong. 
        // This POST shows an invalid post, typically if an error in the data a Bad Request will be posted. 
        public async Task<string> insInvalidEmployee_Onboard_E06()
        {
            PKandUpdateDate loResult = null;
            string lsResult = "";

            try
            {
                Helperfunctions.RequestSessionToken(Common.Email, Common.Password, Common.DefaultAccessToken);
            }
            catch (System.Exception e)
            {
                throw new Exception(string.Format("Error Message:{0}", e.Message));
                // if token is invalid  - revoked access apply for a new token
            }

            // id will be assigned and returned  
            EmployeeOnBoardInfo loRec = null;
            EmployeeOnBoardInfo loType01 = new EmployeeOnBoardInfo();
            loType01.empno = "TEST04";
            loType01.title = "Ms";
            loType01.firstname = "Pig A";
            loType01.preferredname = "";
            loType01.surname = "la";
            loType01.Email = "pc1@ttt.com.au";
            loType01.sex = "M";
            loType01.company_code = "BBE";
            loType01.pay_points = "Head Office";
            //            loType01.hireddate = "\/Date(1440093600000+1000)\/";  // will fail on this 
            loType01.taxfileno = "11111111111";
            loType01.departments = "VICAC";
            loType01.empcond_header = "Non-Award (VIC)";
            loType01.tax_header = "2";
            loType01.classifications = "Management";
            loType01.employment_status = "Full Time";
            loType01.pay_periods = "Monthly";
            loType01.pay_item_code = "P01";
            loType01.super_fund = "AXA";
            loType01.position = "OHS Manager";

            // loType01.suspend_reasons     // Will fail on this as this is mandatory

            loRec = loType01;
            
//            loRec = Populate_employee_Onboard();

            var client = new HttpClient();

            var request = new System.Net.Http.HttpRequestMessage()
            {
                RequestUri = Helperfunctions.getURI("EmployeeDetails/onboard"),

                Method = HttpMethod.Post
            };

            // Setup Headers
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            // Header Details - software key 
            client.DefaultRequestHeaders.Add("cmkey", Common.Instance().SessionToken);
            // Who is the user of that is making the call. 
            client.DefaultRequestHeaders.Add("username", Common.Username);

            Console.WriteLine("Employee Onboard : Sending Asysnc");
            //var System.Diagnostic

            var response = await client.PostAsJsonAsync(request.RequestUri, loRec);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                // We now need to read the response back as we may want the PK that was assigned to it. 
                // Now transpose the Results. 
                var jsonString = response.Content.ReadAsStringAsync();
                jsonString.Wait();

                // if we provide an incorrect URL lets make sure that the code handles itself correctly. 
                try
                {
                    loResult = JsonConvert.DeserializeObject<PKandUpdateDate>(jsonString.Result);
                    Console.WriteLine("Employee Onboard : " + jsonString.Result);
                    Console.WriteLine("Employee Onboard : id= " + loResult.ID.ToString() + " - Message= " + loResult.Update_Date.ToString());

                    // NOTE: If this validates ok and is created accordingly then we should be able to retrieve and ammend the Employee.
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
                        lsResult = lsResult + "Error : /EmployeeDetails/onboard path failed with : 404 Not Found ";
                        break;
                    case HttpStatusCode.BadRequest:
                        var jsonString = response.Content.ReadAsStringAsync();
                        jsonString.Wait();

                        lsResult = lsResult + string.Format("Error Message:{0}", response.StatusCode + " Possible bad json format. " + jsonString.Result);

                        // Is there a msg to show. 
                        // "Fix following errors before import: Suspend Reason - 'New employee - awaiting final check' does not exist.\r\nHired date must be supplied\r\n"

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


        public async Task<string> insEmployee_Onboard_E06()
        {
            PKandUpdateDate loResult = null;
            string lsResult = "";

            try
            {
                Helperfunctions.RequestSessionToken(Common.Email, Common.Password, Common.DefaultAccessToken);
            }
            catch (System.Exception e)
            {
                throw new Exception(string.Format("Error Message:{0}", e.Message));
                // if token is invalid  - revoked access apply for a new token
            }

            // id will be assigned and returned  
            EmployeeOnBoardInfo loRec = null;
            EmployeeOnBoardInfo loType01 = new EmployeeOnBoardInfo();
            loType01.empno = "TEST05";
            loType01.title = "Ms";
            loType01.firstname = "Pig A";
            loType01.preferredname = "";
            loType01.surname = "la";
            loType01.Email = "pc1@ttt.com.au";
            //            loType01.emailOther =  "p1c@email.org.au";      // This doesn't exist this is a mistake. 
            loType01.sex = "M";
            loType01.company_code = "BBE";
            loType01.pay_points = "Head Office";

// This is erroring. 
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-AU", true);
            culture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            loType01.hireddate = Convert.ToDateTime("25/11/2012", culture);   

            //DateTime.UtcNow;
            loType01.taxfileno = "11111111111";
            loType01.departments = "VICAC";
            loType01.empcond_header = "Non-Award (VIC)";
            loType01.tax_header = "2";
            loType01.classifications = "Management";
            loType01.employment_status = "Full Time";
            loType01.pay_periods = "Monthly";
            loType01.pay_item_code = "P01";
            loType01.super_fund = "AXA";
            loType01.position = "OHS Manager";

            loType01.suspend_reasons = "New employee - awaiting final check";

            loRec = loType01;

            //            loRec = Populate_employee_Onboard();

            var client = new HttpClient();

            var request = new System.Net.Http.HttpRequestMessage()
            {
                RequestUri = Helperfunctions.getURI("EmployeeDetails/onboard"),

                Method = HttpMethod.Post,
                
                //RequestFormat = DataFormat.Json,
                //Resource = "SaveBooking"
            };

            
            // Setup Headers
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            // Header Details - software key 
            client.DefaultRequestHeaders.Add("cmkey", Common.Instance().SessionToken);
            // Who is the user of that is making the call. 
            client.DefaultRequestHeaders.Add("username", Common.Username);

            Console.WriteLine("Employee Onboard : Sending Asysnc");
            //var System.Diagnostic

            var response = await client.PostAsJsonAsync(request.RequestUri, loRec);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                // We now need to read the response back as we may want the PK that was assigned to it. 
                // Now transpose the Results. 
                var jsonString = response.Content.ReadAsStringAsync();
                jsonString.Wait();

                // if we provide an incorrect URL lets make sure that the code handles itself correctly. 
                try
                {
                    loResult = JsonConvert.DeserializeObject<PKandUpdateDate>(jsonString.Result);
                    Console.WriteLine("Employee Onboard : " + jsonString.Result);
                    Console.WriteLine("Employee Onboard : id= " + loResult.ID.ToString() + " - Message= " + loResult.Update_Date.ToString());

                    // NOTE: If this validates ok and is created accordingly then we should be able to retrieve and ammend the Employee.
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
                        lsResult = lsResult + "Error : /EmployeeDetails/onboard path failed with : 404 Not Found ";
                        break;
                    case HttpStatusCode.BadRequest:
                        var jsonString = response.Content.ReadAsStringAsync();
                        jsonString.Wait();

                        lsResult = lsResult + string.Format("Error Message:{0}", response.StatusCode + " Possible bad json format. " + jsonString.Result);

                        // Is there a msg to show. 
                        // "Fix following errors before import: Suspend Reason - 'New employee - awaiting final check' does not exist.\r\nHired date must be supplied\r\n"

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



    }
}
