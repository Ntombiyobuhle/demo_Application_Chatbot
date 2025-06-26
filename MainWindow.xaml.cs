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
        //call the load quiz method 
        private List<QuizQuestion> quizData;
       

        //variables
        private int questionIndex = 0;
        private int currentScore = 0;

        //buttons
        private Button selectedChoice = null;
        private Button correctChoiceButton = null;

        public MainWindow()
        {
            InitializeComponent();
            //call the load quiz method
            LoadQuizData();
            showQuiz();
           
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


        private List<string> activityLog = new List<string>();

        private void LogActivity(string activity)
        {
            activityLog.Add(activity);
            activity_log.Items.Add(activity);
            activity_log.ScrollIntoView(activity_log.Items[activity_log.Items.Count - 1]);
        }

        private void Activity(object sender, RoutedEventArgs e)
        {
            // hide other page and set chat page visible
            Chat_page.Visibility = Visibility.Hidden;
            reminder_page.Visibility = Visibility.Hidden;
            quiz_page.Visibility = Visibility.Hidden;

            chat_append.ScrollIntoView(chat_append.Items[chat_append.Items.Count - 1]);
            // current page is chat page
            activity_page.Visibility = Visibility.Visible;

            activity_log.Items.Clear();
            foreach (var activity in activityLog)
            {
                activity_log.Items.Add(activity);
            }
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
            if (!string.IsNullOrEmpty(temp_userTask))
            {
                // if all validated, then check for the task
                if (temp_userTask.Contains("add task"))
                {
                    // Replace the found task on add task
                    string get_description = temp_userTask.Replace("add task", "");

                    // add task message to list view
                    LogActivity("you added a task: " + get_description);
                    chat_append.Items.Add("task added with description: " + get_description + " \nWould you like to set a reminder?");

                    // add the task to the list view but get date and time
                    DateTime date = DateTime.Now.Date;
                    DateTime time = DateTime.Now.ToLocalTime();
                    string format_date = date.ToString("yyyy-MM-dd");
                   
                    activityLog.Add("you added a task: " + get_description + " on " + format_date + " at " + time.ToString("HH:mm:ss"));
                    chat_append.Items.Add("User : " + get_description + "\n" + format_date + " Time " + time.ToString("HH:mm:ss"));

                    // set list view to auto scroll
                    chat_append.ScrollIntoView(chat_append.Items[chat_append.Items.Count - 1]);

                    // assign the task to global variable
                    hold_task = get_description;
                }
                else if (temp_userTask.Contains("remind"))
                {
                    string hold_day = reminder.get_days(temp_userTask);

                    // check task holder
                    if (!string.IsNullOrEmpty(hold_task))
                    {
                        // get the calculated date
                        if (hold_day == "today")
                        {
                            chat_append.Items.Add(reminder.today_date(hold_task, hold_day));
                        }
                        else
                        {
                            LogActivity("you set a reminder for " + hold_day + " days");
                            chat_append.Items.Add("great, i will remind you " + hold_day + " days " + " task " + hold_task);
                            reminder.get_remindDate(hold_task, hold_day);
                        }
                    }
                    else
                    {
                        // show error message
                        System.Console.Beep();
                        activityLog.Add("you tried to set a reminder without a task");
                        chat_append.Items.Add("No task was set to remind you");
                    }
                }
                else if (temp_userTask.Contains("show remind"))
                {
                    activityLog.Add("you requested to show reminders");
                    chat_append.Items.Add("your reminders are");
                    chat_append.Items.Add(reminder.get_remind());
                }
                else
                {
                    // show error message
                    System.Console.Beep();
                    activityLog.Add("you entered an invalid command");
                    chat_append.Items.Add("Sorry, I don't understand your request. Please try again.");
                }
            }
            else
            {
                chat_append.Items.Add("Please enter a task or reminder");
                activityLog.Add("you tried to set a reminder without entering a task or reminder");
            }
        }
        //method to show the quiz on the buttons
        private void showQuiz()
        {

            //check if the user is not done playing
            if (questionIndex >= quizData.Count)
            {
                //show complete message
                MessageBox.Show("Quiz Complete! You already completed the game with " + currentScore + " score");
                activityLog.Add("you completed the quiz with a score of " + currentScore);
                //then reset the game
                currentScore = 0;
                questionIndex = 0;
                DisplayScore.Text = "";
                showQuiz();

                //stop the execute
                return;
            }

            //get the current index quiz
            correctChoiceButton = null;
            selectedChoice = null;

            //then get all the questions values
            var currentQuiz = quizData[questionIndex];

            //displays the question to the user
            DisplayedQuestion.Text = currentQuiz.Question;

            //add the choices to the buttons
            var shuffled = currentQuiz.Choices.OrderBy(_ => Guid.NewGuid()).ToList();

            //then add by index
            FirstChoiceButton.Content = shuffled[0];
            SecondChoiceButton.Content = shuffled[1];
            ThirdChoiceButton.Content = shuffled[2];
            FourthChoiceButton.Content = shuffled[3];

            clearStyle();
        }

        //method to rest the buttons
        private void clearStyle()
        {
            //use for each to reset
            foreach (Button choice in new[] { FirstChoiceButton, SecondChoiceButton, ThirdChoiceButton, FourthChoiceButton })
            {
                choice.Background = Brushes.LightGray;
            }
        }//end of the clear style method

        //method to load the quiz data
        private void LoadQuizData()
        {
            //store info
            quizData = new List<QuizQuestion>
        {
            new QuizQuestion
            {
                Question = "What should you do if you receive an email asking for your password?",
                CorrectChoice = "Report the email as phishing",
                Choices = new List<string>
                {
                    "Reply with your password",
                    "Delete the email",
                    "Report the email as phishing",
                    "Ignore it"
                }
            },
            new QuizQuestion
            {
                Question = "What is phishing?",
                CorrectChoice = "A type of social engineering attack",
                Choices = new List<string>
                {
                    "A type of malware",
                    "A type of virus",
                    "A type of social engineering attack",
                    "A type of firewall"
                }
            },
            new QuizQuestion
            {
                Question = "What is a strong password?",
                CorrectChoice = "A long password with a mix of letters, numbers, and special characters",
                Choices = new List<string>
                {
                    "A short password with only letters",
                    "A long password with only numbers",
                    "A long password with a mix of letters, numbers, and special characters",
                    "A password that is the same for all accounts"
                }
            },
            new QuizQuestion
            {
                Question = "Why is two-factor authentication important?",
                CorrectChoice = "It adds an extra layer of security to your account",
                Choices = new List<string>
                {
                    "It makes your password stronger",
                    "It adds an extra layer of security to your account",
                    "It makes it easier to login",
                    "It is not important"
                }
            },
            new QuizQuestion
            {
                Question = "What should you do if you suspect your account has been hacked?",
                CorrectChoice = "Change your password immediately and report it",
                Choices = new List<string>
                {
                    "Change your password immediately",
                    "Ignore it and hope it goes away",
                    "Report it to the authorities",
                    "Change your password immediately and report it"
                }
            },
            new QuizQuestion
            {
                Question = "What is social engineering?",
                CorrectChoice = "A type of attack that manipulates people into revealing sensitive information",
                Choices = new List<string>
                {
                    "A type of malware",
                    "A type of virus",
                    "A type of attack that manipulates people into revealing sensitive information",
                    "A type of firewall"
                }
            },
            new QuizQuestion
{
    Question = "What is safe browsing?",
    CorrectChoice = "Avoiding suspicious websites and being cautious when clicking on links",
    Choices = new List<string>
    {
        "Visiting any website without worrying about security",
        "Avoiding suspicious websites and being cautious when clicking on links",
        "Downloading software from any website",
        "Sharing personal information on any website"
    }
}




    };
               }//end of load quiz data method
  private void HandleAnswerSelection(object sender, RoutedEventArgs e)
        {

            //use sender object name to get the selected button
            selectedChoice = sender as Button;

            string chosen = selectedChoice.Content.ToString();

            //then check with correct on the current quiz
            string correct = quizData[questionIndex].CorrectChoice;

            //then check if correct or not by if statement
            if (chosen == correct)
            {
                //then set the button background color
                selectedChoice.Background = Brushes.Green;
                //assing to hold
                correctChoiceButton = selectedChoice;
            }
            else
            {
                //if incorrect
                selectedChoice.Background = Brushes.DarkRed;
                correctChoiceButton = selectedChoice;
            }
        }
        private void HandleNextQuestion(object sender, RoutedEventArgs e)
        {
            //check if the user selected one of the choices
            if (selectedChoice == null)
            {
                //then show error message
                MessageBox.Show("Choose one of the 4 choices");
            }
            else
            {
                //then add points , and only if correct
                string chosen = selectedChoice.Content.ToString();
                string correct = quizData[questionIndex].CorrectChoice;

                //check if correct 
                if (chosen == correct)
                {
                    //then add point
                    currentScore++;
                    //then show the score
                    DisplayScore.Text = "Score : " + currentScore;

                    //then move to the next index question
                    questionIndex++;
                    //show the question again for the next one
                    showQuiz();
                }
                else
                {
                    //move to the next question 
                    questionIndex++;
                    showQuiz();
                }

            }
        }
        private void ExecuteActivityCommand(object sender, RoutedEventArgs e)
        {
            string command = activity_command.Text.ToLower();
            if (command == "show activity log")
            {
                activity_log.Items.Clear();
                foreach (var activity in activityLog)
                {
                    activity_log.Items.Add(activity);
                }
            }
            else
            {
                activity_log.Items.Add("Invalid command. Type 'show activity log' to view history.");
            }
            activity_command.Clear();
        }

        private void DeleteHistory(object sender, RoutedEventArgs e)
        {
            activityLog.Clear();
            activity_log.Items.Clear();
        }



    }//end of the handle next question event handler



}//end of class MainWindow




