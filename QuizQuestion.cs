﻿namespace demo_Application_Chatbot
{
    internal class QuizQuestion
    {
        //getters and setters
        public string Question { get; set; }
        public string CorrectChoice { get; set; }

        //get and set on the List 
        public List<string> Choices { get; set; }

    }
}