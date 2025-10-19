using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SurveyMVC.Models;

namespace SurveyMVC.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            // Admin dashboard
            if (User.IsInRole("Admin"))
            {
                var totalSurveys = db.Surveys.Count();
                var totalResponses = db.Responses.Count();
                var totalQuestions = db.Questions.Count();
                var totalEmployees = db.Users.Count(u => u.Roles.Any(r => r.RoleId == db.Roles.FirstOrDefault(role => role.Name == "Employee").Id));

                ViewBag.TotalSurveys = totalSurveys;
                ViewBag.TotalResponses = totalResponses;
                ViewBag.TotalQuestions = totalQuestions;
                ViewBag.TotalEmployees = totalEmployees;

                return View("AdminHome");
            }
            // Employee dashboard
            else if (User.IsInRole("Employee"))
            {
                var myResponses = db.Responses.Count(r => r.EmployeeId == userId);
                var availableSurveys = db.Surveys.Count();

                ViewBag.MyResponses = myResponses;
                ViewBag.AvailableSurveys = availableSurveys;

                return View("EmployeeHome");
            }

            // Default fallback (unauthorized)
            return View();
        }
    }
}
