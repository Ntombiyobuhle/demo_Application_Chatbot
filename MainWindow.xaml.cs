using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace demo_Application_Chatbot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //cresting an instance for the class get reminder 
        get_reminder reminder = new get_reminder();
        string hold_task = string.Empty;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void chat(object sender, RoutedEventArgs e)
        {
            // hide other page and set chat page visible
            reminder_page.Visibility = Visibility.Hidden;
            quiz_page.Visibility = Visibility.Hidden;
            activity_page.Visibility = Visibility.Hidden;

            // current page is chat page
            Chat_page.Visibility = Visibility.Visible;
        }

        private void remind(object sender, RoutedEventArgs e)
        {
            // hide other page and set chat page visible
            Chat_page.Visibility = Visibility.Hidden;
            quiz_page.Visibility = Visibility.Hidden;
            activity_page.Visibility = Visibility.Hidden;

            // current page is chat page
            reminder_page.Visibility = Visibility.Visible;
        }
        private void Quiz(object sender, RoutedEventArgs e)
        {
            // hide other page and set chat page visible
            Chat_page.Visibility = Visibility.Hidden;
            reminder_page.Visibility = Visibility.Hidden;
            activity_page.Visibility = Visibility.Hidden;

            // current page is chat page
            quiz_page.Visibility = Visibility.Visible;
        }

        private void Activity(object sender, RoutedEventArgs e)
        {
            // hide other page and set chat page visible
            Chat_page.Visibility = Visibility.Hidden;
            reminder_page.Visibility = Visibility.Hidden;
            quiz_page.Visibility = Visibility.Hidden;


            // current page is chat page
            activity_page.Visibility = Visibility.Visible;
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            // close the application 
            System.Environment.Exit(0);
        }
        private void setting_reminder(object sender, RoutedEventArgs e)
        {
            // temp variables to collect users input 
            string temp_userTask = user_task.Text.ToString();

            // check if EMPTY OR NOT 
            if (reminder.vaildate_input(temp_userTask) != "found")
            {
                // show error message 
                MessageBox.Show(reminder.vaildate_input(temp_userTask));
                return;

            }
            // if all vaidated , then check for the task 

            if (temp_userTask.Contains("add task"))

            {
                // Replase the found task on add task 
                string get_description = temp_userTask.Replace("add task", "");

                // add task  message to list view 
                chat_append.Items.Add("task added with description: " + get_description + " \nWould you like to set a reminder?");

                // the asign the task to globle varible 
                hold_task = get_description;



                // MessageBox.Show("Tak add with description: " + get_description);
            }
            else if (temp_userTask.Contains("remind"))
            {
                Console.WriteLine("to add task type 'add task' and add description");
                string hold_day = reminder.get_days(temp_userTask);
                //check task holder
                if (hold_day == "days")
                {
                    // get the day and user's input
                    hold_day = reminder.get_days(temp_userTask);

                    // check if today 
                    if (hold_day != "today")
                    {
                        // get the message 
                        if (reminder.today_date(hold_task, hold_day) != "error")
                        {
                            // add to list 
                            chat_append.Items.Add(reminder.today_date(hold_task, hold_day));
                        }
                    }
                    else
                    {
                        chat_append.Items.Add("sorry, System chatbot failed to set reminder");
                    }

                    // MessageBox.Show("Remind days are  " + hold_day);
                }
                else
                {
                    // get the calculated date
                    if (reminder.get_remindDate(hold_task, hold_day) != "done")
                    {
                        // add to list 
                        chat_append.Items.Add("great, i will remind you " + hold_day +" to task "+ hold_task );
                    }

                   //   System.Console.Beep()
                    //  chat_append.Items.Add("No task was set to remind you");






                    else
                    {
                        // show error message
                        System.Console.Beep();
                        chat_append.Items.Add("No task was set to remind you");
                    }

                }
            }
            else if (temp_userTask.Contains("show remind"))

            {
                chat_append.Items.Add("your remind are");
                chat_append.Items.Add(reminder.get_remind());
            }// end of else if
            else
            {
                // show error message 
                System.Console.Beep();
                chat_append.Items.Add("Sorry, I don't understand your request. Please try again.");
            }
        }


    }
}




