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
    }
}