using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace University1.Models
{
    public class QuestionVM
    {
        public int QuestionID { get; set; }
        public string QuestionText { get; set; }
        public ICollection<ChoiceVM> Choices { get; set; }
    }

    public class ChoiceVM
    {
        public int ChoiceID { get; set; }
        public string ChoiceText { get; set; }
    }

    public class QuizAnswersVM
    {
        public int QuestionID { get; set; }
        public int TestID { get; set; }
        public string AnswerQ { get; set; }
        public bool isCorrect { get; set; }

    }
}