using System;
using System.Collections.Generic;
using System.Text;

namespace TypeTrivia.Models
{
    public class Question
    {
        public enum QuestionType
        {
            Attacking,
            Defending,
            DualType
        }
        public QuestionType Type { get; set; }
    }
}
