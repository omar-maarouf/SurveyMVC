using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SurveyMVC.Models
{
    public class Answer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string AnswerDetails { get; set; }
        public int ResponseId { get; set; }
        public int QuestionId { get; set; }
        public virtual Response Response { get; set; }
        public virtual Question Question { get; set; }
    }
}