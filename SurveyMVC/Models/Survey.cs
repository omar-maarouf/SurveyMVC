using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SurveyMVC.Models
{
    public class Survey
    {
        [Key]
        public int Id { get; set; }
        public string AdminId { get; set; }
        [Required]
        public string Title { get; set; }
    }
}