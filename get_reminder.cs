using System.Text.RegularExpressions;

namespace demo_Application_Chatbot
{
    internal class get_reminder
    {
        // globle array or generic 
        private List<string> description = new List<String>();
        private List<string> dates = new List<string>();
        // validate the user input 
        public string vaildate_input(string user_input)
        {
            // check if the user input is empty or not 
            if (user_input != "")

            {
                //  return found 
                return "found";
            }
            else
            {
                // then return error message 
                return "please add task";

            }

        } //end of method 

        public string get_days(string day)
        {
            // get the number from the users input regex is RegularExpressions
            string get_day_in = Regex.Replace(day, @"[^\d]", "");

            // check if day is 0 
            if (get_day_in != "0")
            {
                return get_day_in;
            }
            else
            {
                return "today";
            }
        }// end of method 

        // wethod to store today 
        public string today_date(String desprcrition, string date)
        {
            // validate 
            if (date == "today")
            {
                // get the date 
                DateTime today_date = DateTime.Now.Date;
                string formet_date = today_date.ToString("yyyy-MM-dd");

                // add all 
                description.Add(desprcrition);
                dates.Add(formet_date);

                return "Nice , will remind you " + date;
            }
            else
            {
                return "error";
            }


        } //end of method 

        // get remind date correct 
        public string get_remindDate(string descriptions, string date)
        {
            //get the current date
            DateTime current_date = DateTime.Now.Date;

            // then format date 
            string format_date = current_date.ToString("yyyy-MM-dd");

            // get the day in format
            string find_day = format_date.Substring(8, 2);

            // get date from2 to 8 
            string final_dates = format_date.Substring(0, 8);

            int total_days = int.Parse(find_day) + int.Parse(date);
            String store_date = final_dates + total_days;

            description.Add(descriptions);
            dates.Add(store_date);

            return store_date;

        }//end of method 

        // method to check reminders
        public string get_remind()
        {
            // then search for today 
            DateTime today = DateTime.Now.Date;
            string now_date = today.ToString("yyyy-MM-dd");

            string found_remind = "";
            for (int count = 0; count < dates.Count; count++)
            {
                // check for the date 
                if (dates[count] == now_date)
                {
                    // then append message
                    found_remind += "\nDue Today :" + description[count] + "\n" + dates[count];


                }
                else
                {
                    found_remind += "\n" + description[count] + "\n" + dates[count];
                }
            }
            return found_remind;
        }
    }
}