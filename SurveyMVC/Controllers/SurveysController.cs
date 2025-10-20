using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SurveyMVC.Models;
using Microsoft.AspNet.Identity;

namespace SurveyMVC.Controllers
{
    public class SurveysController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Surveys
        public ActionResult Index()
        {
            return View(db.Surveys.ToList());
        }

        // GET: Surveys/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Survey survey = db.Surveys.Find(id);
            if (survey == null)
            {
                return HttpNotFound();
            }
            return View(survey);
        }

        // GET: Surveys/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // GET: Surveys/Respond/5
        [Authorize(Roles = "Employee")]
        public ActionResult Respond(int? surveyId)
        {
            if (surveyId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int id = surveyId.Value;
            List<Question> questions = db.Questions.Where(q => q.SurveyId.Equals(id)).ToList();
            if (questions.Count == 0)
            {
                return HttpNotFound();
            }
            // Redirect with surveyId as a route parameter
            return RedirectToAction("Create", "Responses", new { surveyId = id });
        }

        // POST: Surveys/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(Survey survey)
        {

            if (survey.Questions == null || !survey.Questions.Any())
            {
                ModelState.AddModelError("", "A survey must contain at least one question.");
                return View(survey);
            }

            if (survey.Questions.Any(q => string.IsNullOrWhiteSpace(q.QuestionDetails)))
            {
                ModelState.AddModelError("", "All questions must have text.");
                return View(survey);
            }

            if (ModelState.IsValid)
            {
                survey.AdminId = this.User.Identity.GetUserId();
                db.Surveys.Add(survey);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            return View(survey);
        }

        // GET: Surveys/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Survey survey = db.Surveys.Find(id);
            if (survey == null)
            {
                return HttpNotFound();
            }
            return View(survey);
        }

        // POST: Surveys/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Survey survey)
        {
            if (!ModelState.IsValid)
                return View(survey);

            // Load the existing survey and its questions from the database
            var existingSurvey = db.Surveys
                .Include(s => s.Questions)
                .FirstOrDefault(s => s.Id == survey.Id);

            if (existingSurvey == null)
                return HttpNotFound();

            existingSurvey.Title = survey.Title;

            if (survey.Questions != null)
            {
                foreach (var updatedQuestion in survey.Questions)
                {
                    var questionInDb = existingSurvey.Questions
                        .FirstOrDefault(q => q.Id == updatedQuestion.Id);

                    if (questionInDb != null)
                    {
                        questionInDb.QuestionDetails = updatedQuestion.QuestionDetails;
                    }
                }
            }

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Surveys/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Survey survey = db.Surveys.Find(id);
            if (survey == null)
            {
                return HttpNotFound();
            }
            return View(survey);
        }

        // POST: Surveys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Survey survey = db.Surveys.Find(id);
            db.Surveys.Remove(survey);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
