using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SurveyMVC.Models
{
    public class Response
    {
        [Key]
        public int Id { get; set; }
        public int SurveyId { get; set; }
        public virtual Survey Survey { get; set; }
        public string EmployeeId { get; set; }
        public virtual ApplicationUser Employee { get; set; }
        public virtual List<Answer> Answers { get; set; }
    }

    public class ResponseViewModel
    {
        public int SurveyId { get; set; }
        public string SurveyTitle { get; set; }
        public List<string> Answers { get; set; }
        public List<Question> Questions { get; set; }
    }
}