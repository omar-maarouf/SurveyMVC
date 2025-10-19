using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SurveyMVC.Models
{
    public class Question
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string QuestionDetails { get; set; }
        public int SurveyId { get; set; }
        public virtual Survey Survey { get; set; }
        public virtual List<Answer> Answers { get; set; }
    }
}