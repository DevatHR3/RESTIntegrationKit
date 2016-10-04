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

    /*
     * Employee Onboard 
     *   is a set of 
     *      to get a qualified Employee inserted into the system. 
     * 
     * NOTE: if you execute these functions over and over you will get duplicate errors. 
     *   Consider restoring the database or change the empno and names to avoid duplicates
     * 
     * 
     *   
     * 
     */
    // 

    // Employee Onboard class structure 
    public class EmpOnBoardInfo
    {
        public EmpOnBoardInfo() { }

        public string company_code { get; set; }    // Company code 
        public int employee_details_Id { get; set; }

        public string empno { get; set; }   // Max 6 chars - must be unique 


        public string archived { get; set; }    // T / F  
        //
        //public DateTime? termdate { get; set; }
        public string surname { get; set; }
        public string firstname { get; set; }

        public DateTime? hireddate { get; set; }

        public DateTime? update_date { get; set; }

        public string pay_points { get; set; }          
        public int? pay_points_id { get; set; }

        public string suspend_reasons { get; set; }
        public int? suspend_reasons_id { get; set; }

        public string departments { get; set; }      // Department code 
        public int? departments_id { get; set; }

        public string classifications { get; set; }
        public int? classifications_id { get; set; }

        public string employment_status { get; set; }
        public int? employment_status_id { get; set; }

        public string asco { get; set; }
        public int? asco_id { get; set; }

        public string tax_header { get; set; }       // Tax header code - 1 / 2 / 3 / 4 / 5 / 6 / 7 
        public int? tax_header_id { get; set; }

        public string pay_periods { get; set; }      // Weekly / Fortnightly / Monthly
        public int? pay_periods_id { get; set; }

        public string template_header { get; set; }
        public int? template_header_id { get; set; }

        public string empcond_header { get; set; }
        public int? empcond_header_id { get; set; }

        public string nationality { get; set; }      // Australia / New Zealand   - the country is used. 
        public int? nationality_id { get; set; }

        public string workerscomp_class { get; set; }
        public int? workerscomp_class_id { get; set; }

        public string title { get; set; }             // Mr / Mrs / Miss / Dr / Jnr
        public int? title_id { get; set; }

        public string initial { get; set; }

        public string printname { get; set; }

        public string preferredname { get; set; }

        public string prevsurname { get; set; }

        public string prevfirstname { get; set; }

        public string previnitial { get; set; }

        public int employee_level { get; set; }       // 0..9

        public string remotelogin { get; set; }        // T / F

        public string employee_password { get; set; }     // not supported

        public string passquestion { get; set; }

        public string passanswer { get; set; }

        public string employee_suspend { get; set; }       // T / F

        public string apprentice { get; set; }             // T / F

        public string accredtrainee { get; set; }         // T / F

        public string cdep { get; set; }               // T / F

        public string sex { get; set; }                // M / F / O

        public DateTime? birthdate { get; set; }

        public string driverslic { get; set; }              // T / F

        public string licenseno { get; set; }

        public string smoker { get; set; }                  // T / F  

        public string taxavg { get; set; }                   // T / F

        public string taxexempt { get; set; }             // T / F

        public decimal zonereb { get; set; }              // 0.0

        public string resident { get; set; }               // T / F

        public string basisofpayment { get; set; }         // BasisofPayment  should be a  one of the following. 
/*    ‘F’  - Full time
    ‘P’  - Part Time 
    ‘C’ – Casual
    ‘L’ – Labour Hire 
    ‘S’ – Pension / Annuity     */

        public string taxfreeclaimed { get; set; }         // T / F

        public string hecs { get; set; }                   // T / F

        public string sfss { get; set; }                  // T / F

        public string annuityrebate { get; set; }           // T / F

        public string signaturepresent { get; set; }        // T / F

        public DateTime? declareddate { get; set; }

        public string taxvariation { get; set; }             // T / F

        public DateTime? taxvarstart { get; set; }

        public DateTime? taxvarfinish { get; set; }

        public decimal taxvarrate { get; set; }

        public string spouse { get; set; }               // T / F

        public int numberdep { get; set; }             // 0.. 

        public string taxfileno { get; set; }          // A valid taxfile no 

        public string taxtype { get; set; }
        /*
         * Taxtype should be either – I , C, L, V

                 TaxType = 'C'   Contractor
                 TaxType = 'I'    Individual Non business
                 TaxType = 'L'   Labour Hire
                 TaxType = 'V'  Voluntary Agreement

         * */

        public string ppsvarno { get; set; }

        public string paytype { get; set; }
        /*
         * Paytype should be either - B , C , Q , E 
             B = Bank Transfer
         *   C = Cash
         *   Q = Cheque
         *   E - Electronic Funds Transfer

         */

        public decimal stdhours { get; set; }    // 0.0   - required for Employee Payment Standard Hours 

        public decimal stdrate { get; set; }     // 0.0   - required for Employee Payment rate.    

        public string rdo { get; set; }          // T / F 

        public string passport { get; set; }    // T / F 

        public DateTime? passexpires { get; set; }

        public string autopay { get; set; }      // T / F

        public decimal autoamt { get; set; }     // 0.0

        public decimal warnif1 { get; set; }     // 0.0

        public decimal warnif2 { get; set; }

        public decimal sacrifice { get; set; }    // 0.0 

        public decimal salary { get; set; }        // 0.0 

        public decimal defaultoncost { get; set; }      // 0.0   

        public string acrrueleave { get; set; }        // T / F

        public string tfnnot { get; set; }             // T / F    - Tax file notification

        public decimal ftbamount { get; set; }          // 0.0

        public string zonerebate { get; set; }          // T / F

        public string emailpayslip { get; set; }        // T / F

        public string empabn { get; set; }              // 99999999   

        public decimal withholdrate { get; set; }       // 0.0

        public string paynote { get; set; }

        public string centrelink_registered { get; set; }    // T / F

        public string centrelink_consent { get; set; }        // T / F

        public string centrelink_crn { get; set; }

        public string active_in_external { get; set; }       // T / F    - warning if this is set to 'F' you will not be able to retrieve the employee's details 

        public int timeclock_identifier { get; set; }

        public string tfnnz { get; set; }                   // T / F    - New Zealand tax file no. (use different validation)

        public string payrolltax { get; set; }              // T / F

        public string payrolltax_state { get; set; }        // State that Payroll  - Victoria / New South Wales / ...   see States 
        public int? payrolltax_state_id { get; set; }

        public string pay_period_cycle_set { get; set; }     // Pay Period Cycle set. 
        public int? pay_period_cycle_set_id { get; set; }

        public string security_group { get; set; }          // Security Group the employee belongs to 
        public int? security_group_id { get; set; }


        public string position { get; set; }                // Employee Position 

        public string position_identifier { get; set; }     

        public decimal position_incumbent_fte { get; set; }     // 0.0

        public string address_line_1 { get; set; }

        public string address_line_2 { get; set; }

        public string suburb { get; set; }

        public string postcode { get; set; }

        public string phone_no { get; set; }                // Business Phone

        public string phone_no_ah { get; set; }           // After hours / Home Phone
         
        public string phone_no_mob { get; set; }          // Mobile 

        public string bsb { get; set; }                    // bankstatebranch

        public string account_no { get; set; }

        public string account_name { get; set; }

        public string lodgeref { get; set; }

        public string pay_item_code { get; set; }         // P01 - Pay item code for Ordinary time.   

        public string super_fund { get; set; }

        public string super_fund_member_no { get; set; }

        public string Email { get; set; }

        public string Kiosk_Login { get; set; }           // T / F 

        //public DateTime? Update_Date { get; set; }
    }


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

    // Employee Onboard utilities class 
    public class Employee_Onboard_class
    {

        /// <summary>
        ///  NOTE: The data is setup specifically against the Sample database, company "Bumble Bee".
        ///        That is all the company related items are related to this company only. 
        ///        If you have any issue when posting just verify the lookup information is available. 
        /// 
        ///         This function is just a population function for a fully qualified Employee to be passed in. 
        /// </summary>
        /// <returns></returns>
        public EmployeeOnBoardInfo Populate_employee_Onboard()
        {
            EmployeeOnBoardInfo loRec = new EmployeeOnBoardInfo();

            // Requirements : company code needs to be set if we are defining a number of company specific items ie: Superfunds. 
            loRec.employee_details_Id = 0;  // PK for new Employee should be 0. 
            loRec.company_code = "BBE";     // Required Field - we must know what company that the Employee is being assigned to. 
            loRec.empno = "ABC001";         // Alphanumeric - upto 6 chars 

            loRec.archived = "F";           // Should be false 
            loRec.surname = "Xexxxx";       // Surname of the Employee

            loRec.firstname = "Testy";      // Firstname of the Employee 

            loRec.hireddate = DateTime.UtcNow;    // Hireddate - Date the Employee commenced Employment. 
            loRec.update_date = null;             // dont set this. Internal datetime the record was last updated.         

            // A category for employees being paid.
            loRec.pay_points = "Head Office";                        // Must match existing Company Pay Points. 

            // By default onboarding process all new employees need to be set to 
            //    "New employee - awaiting final check" 
            loRec.suspend_reasons = "New employee - awaiting final check";  // Reason a employee has been suspended.   

            // Part of Costing structure, every Employee belongs 
            loRec.departments = "VICADM";                            // Must match an existing Company related Department.  Code/Description ?  
            loRec.classifications = "Management";                    // Must match existing value in the Classification lookup table.
            loRec.employment_status = "Casual";                      // Casual / Full Time /  Part time
            loRec.asco = "";                                         // Industry standard Job category
            loRec.tax_header = "Tax Free Threshold Not Claimed";     // Must match an existing Company related Tax scale
            loRec.pay_periods = "Weekly";                            // Weekly / Fortnighly / Monthly / ... 
            loRec.template_header = "";                              // Must match an existing Company related template
            loRec.empcond_header = "Casual (VIC)";                   // Must match an existing Company related Employment condition.  
            loRec.nationality = "Australia";                         // Country match
            loRec.workerscomp_class = "AMP Workers Compensation Division"; // Must match an existing Company related Worker Compensation Insurers 
            loRec.title = "Mr";                                      // Must match existing value in the Titles lookup table.
            loRec.initial = "A";
            loRec.printname = "";                                    // Can be assembled in the data entry window  or made up of firstname Surname Initials
            loRec.preferredname = "";     // alternative name
            loRec.prevsurname = "";
            loRec.prevfirstname = "";
            loRec.previnitial = "";
            loRec.employee_level = 0;                                // Security Level  valid for 0 - 9 
            loRec.remotelogin = "T";                                 // Enabled Employee login (Kiosk Login)
            loRec.employee_password = "password";                    // Employee password are encrypted.
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

/*
  BasisofPayment  should be a  one of the following. 
    ‘F’  - Full time
    ‘P’  - Part Time 
    ‘C’ – Casual
    ‘L’ – Labour Hire 
    ‘S’ – Pension / Annuity 
 */


            loRec.taxfreeclaimed = "T";
            loRec.hecs = "F";
            loRec.sfss = "F";
            loRec.annuityrebate = "F";
            loRec.signaturepresent = "T";
            loRec.declareddate = DateTime.UtcNow;                         // TFN Declaration date. 
            loRec.taxvariation = "F";
            loRec.taxvarstart = null;
            loRec.taxvarfinish = null;
            loRec.taxvarrate = 0.0m;
            loRec.spouse = "T";
            loRec.numberdep = 1;
            loRec.taxfileno = "111111111";                                // Applied
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
            loRec.emailpayslip = "T";                         // Email payslip 
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
            loRec.payrolltax_state = "";                      // Must match an existing Company related States
            loRec.pay_period_cycle_set = "";                  // Must match an existing Company related Pay Period cycle set 
            loRec.security_group = "";                        // Must match an existing Company related Security Groups
            loRec.position = "";                              // HR Postion Information. 
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
            loRec.bsb = "731051";                             // Bank state branch
            loRec.account_no = "12345";
            loRec.account_name = "Testy Acct";
            loRec.lodgeref = "Pay";

            loRec.pay_item_code = "P01";                      // Must match an existing Company related Pay item (Payments)
            loRec.super_fund = "AMP";                         // Must match an existing Company related Superfund
            loRec.super_fund_member_no = "12345";

            loRec.Email = "testya@bumbles.com.au";
            loRec.Kiosk_Login = "T";

            return loRec;
        }


        /// <summary>
        ///  An Example of an absolutely min details to get in a valid Employee into the system. 
        /// </summary>
        /// <returns></returns>
        public string insEOB()
        {
            // Adjusted to SqlSampleHR db. 
            EmployeeOnBoardInfo loType01 = new EmployeeOnBoardInfo();
            loType01.company_code = "BBE";
            loType01.empno = "TEST04";   // WARNING:if you are going to run this multiple times you will need to change the empno  
            loType01.title = "Ms";       //   also the firstname and surname as well. 
            loType01.firstname = "Alice";
            loType01.preferredname = "";
            loType01.surname = "la";
            loType01.Email = "pc1@ttt.com.au";
            //            loType01.emailOther =  "p1c@email.org.au";      // This doesn't exist this is a mistake. 
            loType01.sex = "M";
            loType01.pay_points = "Head Office";
            //loType01.hireddate = "\/Date(1440093600000+1000)\/";
            loType01.hireddate = DateTime.UtcNow; // Hireddate - Date the Employee commenced Employment. 

            loType01.taxfileno = "11111111111";
            loType01.departments = "VICBK";                
            loType01.empcond_header = "Non-Award (VIC)";
            loType01.tax_header = "2";
            loType01.classifications = "Management";
            loType01.employment_status = "Full Time";
            loType01.pay_periods = "Monthly";
            loType01.pay_item_code = "P01";
            loType01.super_fund = "AXA";
            loType01.position = "OHS Manager";

            loType01.update_date = DateTime.UtcNow;

            // todo: For this to work the Hireddate is required 

            string lsResult = "";
            try
            {
                HttpResponseMessage response = Common.RequestaPOSTClient("employee/onboard", loType01);
                if (response != null)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Insert Employee Onboard : Ok");

                        var jsonString = response.Content.ReadAsStringAsync();
                        jsonString.Wait();

                        PKandUpdateDate loResult = JsonConvert.DeserializeObject<PKandUpdateDate>(jsonString.Result);

                        Console.WriteLine("Insert Employee Onboard : " + jsonString.Result);
                        Console.WriteLine("Insert Employee Onboard : id= " + loResult.ID.ToString() + " - Message= " + loResult.ToString());

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
                                return string.Format("Insert Employee Onboard : Returned a Badrequest Message {0} ", loResult.message);
                            default:
                                return string.Format("Insert Employee Onboard : Returned a Status of {0}", response.StatusCode); // Worked fine.                     
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


        // Pending 
        /// <summary>
        ///  An Example of an Pending Employee 
        ///  Alot of Lookup ie Departments are company bound with out a Company the look up cannot be validated. 
        ///  
        ///  For a Pending Employee the Suspend Reason needs to be set to a 'New Employee - Awaiting Final Check'
        ///  Also a Company code cannot be set. 
        ///  
        ///  The Employee can be accessed via the Employee Add New wizard via the Pending Employee's option. 
        /// 
        ///  If you wish to constantly rerun this call you will need to change
        ///    - Empno 
        ///    - Firstname 
        ///    - Surname
        /// </summary>
        /// <returns></returns>
        public string insPendingEOB()
        {
            // Adjusted to SqlSampleHR db. 
            EmployeeOnBoardInfo loType01 = new EmployeeOnBoardInfo();
            //loType01.company_code = "BBE";  // No company 
            loType01.empno = "TEST11";   // WARNING:if you are going to run this multiple times you will need to change the empno  
            loType01.title = "Mr";       //   also the firstname and surname as well. 
            loType01.firstname = "Taylorxz";
            loType01.preferredname = "";
            loType01.surname = "Julesxz";
            loType01.Email = "pc1@ttt.com.au";
            //            loType01.emailOther =  "p1c@email.org.au";      // This doesn't exist this is a mistake. 
            loType01.sex = "F";
            // loType01.pay_points = "Head Office";
            //loType01.hireddate = "\/Date(1440093600000+1000)\/";
            loType01.hireddate = DateTime.UtcNow; // Hireddate - Date the Employee commenced Employment. 

            loType01.taxfileno = "11111111111";
            //loType01.departments = "VICBK";                   // if excluding the Company Code then Company Lookups like Departments can not be filled in. 
            //loType01.empcond_header = "Non-Award (VIC)";
            loType01.tax_header = "2";
            //loType01.classifications = "Management";
            loType01.employment_status = "Full Time";
            //loType01.pay_periods = "Monthly";
            //loType01.pay_item_code = "P01";
            //loType01.super_fund = "AXA";
            //loType01.position = "OHS Manager";

            loType01.suspend_reasons = "New employee - awaiting final check";  // Reason a employee has been suspended.   

            loType01.update_date = DateTime.UtcNow;

            // todo: For this to work the Hireddate is required 

            string lsResult = "";
            try
            {
                HttpResponseMessage response = Common.RequestaPOSTClient("employee/onboard", loType01);
                if (response != null)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Insert Employee Onboard : Ok");

                        var jsonString = response.Content.ReadAsStringAsync();
                        jsonString.Wait();

                        PKandUpdateDate loResult = JsonConvert.DeserializeObject<PKandUpdateDate>(jsonString.Result);

                        Console.WriteLine("Insert Employee Onboard : " + jsonString.Result);
                        Console.WriteLine("Insert Employee Onboard : id= " + loResult.ID.ToString() + " - Message= " + loResult.ToString());

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
                                return string.Format("Insert Employee Onboard : Returned a Badrequest Message {0} ", loResult.message);
                            default:
                                return string.Format("Insert Employee Onboard : Returned a Status of {0}", response.StatusCode); // Worked fine.                     
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


        /// <summary>
        /// This POST shows an invalid post, typically if an error in the data a Bad Request will be posted. 
        /// - failure due to not having a Company_code or a Hired date.   
        ///    
        /// </summary>
        /// <returns></returns>
        public string insInvalidEmployee_Onboard_E06()
        {
            EmployeeOnBoardInfo loType01 = new EmployeeOnBoardInfo();
            // loType01.company_code = "BBE"; // will fail on this as its not provided. - actually if a company is not provide the employee will become pending
            //  and require the Employee Wizard to be completed. 
            loType01.empno = "TEST99";
            loType01.title = "Mr";
            loType01.firstname = "John";
            loType01.preferredname = "";
            loType01.surname = "line";
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
            
            string lsResult = "";
            try
            {
                HttpResponseMessage response = Common.RequestaPOSTClient("employee/onboard", loType01);
                if (response != null)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Insert Employee Onboard : Ok");

                        var jsonString = response.Content.ReadAsStringAsync();
                        jsonString.Wait();

                        PKandUpdateDate loResult = JsonConvert.DeserializeObject<PKandUpdateDate>(jsonString.Result);

                        Console.WriteLine("Insert Employee Onboard : " + jsonString.Result);
                        Console.WriteLine("Insert Employee Onboard : id= " + loResult.ID.ToString() + " - Message= " + loResult.ToString());

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
                                return string.Format("Insert Employee Onboard : Returned a Badrequest Message {0} ", loResult.message);
                            default:
                                return string.Format("Insert Employee Onboard : Returned a Status of {0}", response.StatusCode); // Worked fine.                     
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

        


        /// <summary>
        /// Insert a fully qualified Employee via Onboard. 
        /// 
        /// For this process to work change the Hireddate to no less than 90days from today. 
        /// 
        /// </summary>
        /// <returns></returns>
        public string insEmployee_Onboard_E06()
        {
            // id will be assigned and returned  
            EmployeeOnBoardInfo loType01 = new EmployeeOnBoardInfo();
            loType01.company_code = "BBE";
            loType01.empno = "TEST90";
            loType01.title = "Mr";
            loType01.firstname = "Henry";
            loType01.preferredname = "";
            loType01.surname = "Newton";
            loType01.Email = "pc1@ttt.com.au";
            loType01.sex = "M";
            loType01.pay_points = "Head Office";

            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-AU", true);
            culture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            loType01.hireddate = Convert.ToDateTime("01/07/2016", culture);  // 

            loType01.taxfileno = "11111111111";                  // Applied 
            loType01.departments = "VICBK";
            loType01.empcond_header = "Non-Award (VIC)";
            loType01.tax_header = "2";
            loType01.classifications = "Management";
            loType01.employment_status = "Full Time";
            loType01.pay_periods = "Monthly";
            loType01.pay_item_code = "P01";
            loType01.super_fund = "AXA";
            loType01.position = "OHS Manager";

            loType01.suspend_reasons = "New employee - awaiting final check";
        

            string lsResult = "";
            try
            {
                HttpResponseMessage response = Common.RequestaPOSTClient("employee/onboard", loType01);
                if (response != null)
                {
                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();

                    PKandUpdateDate loResult = JsonConvert.DeserializeObject<PKandUpdateDate>(jsonString.Result);

                    Console.WriteLine("Insert Employee Onboard : " + jsonString.Result);
                    Console.WriteLine("Insert Employee Onboard : id= " + loResult.ID.ToString() + " - Message= " + loResult.ToString());

                    // todo: Set the Employee_details_id of the saved record. 
                    lsResult = loResult.ID.ToString();
                }
            }
            catch (System.Exception e)
            {
                // throw new Exception(string.Format("Error Message:{0}", e.Message));
                return string.Format("Error Message:{0}", e.Message);
            }

            return lsResult;
        }



        /// <summary>
        /// Update Employee Onboard offers the ability 
        /// </summary>
        /// <returns></returns>
        public string updEmployee_Onboard()
        {
            return "";
        }


    }
}
