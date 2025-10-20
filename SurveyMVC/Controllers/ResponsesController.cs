using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SurveyMVC.Models;

namespace SurveyMVC.Controllers
{
    public class ResponsesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Responses
        public ActionResult Index()
        {
            var responses = db.Responses.Include(r => r.Survey);
            if (User.IsInRole("Admin")) {
                return View(responses.ToList());
            }
            else
            {
                string emplyeeId = User.Identity.GetUserId();
                responses = responses.Where(r => r.EmployeeId == emplyeeId);
                return View(responses.ToList());
            }
            
        }

        // GET: Responses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Response response = db.Responses.Find(id);
            if (response == null)
            {
                return HttpNotFound();
            }
            return View(response);
        }

        // GET: Responses/Create
        [Authorize(Roles = "Employee")]
        public ActionResult Create(int? surveyId)
        {
            if (surveyId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var survey = db.Surveys.Find(surveyId);
            if (survey == null)
            {
                return HttpNotFound();
            }
            List<Question> questions = db.Questions.Where(q => q.SurveyId.Equals(surveyId.Value)).ToList();
            if (questions.Count == 0)
            {
                return HttpNotFound();
            }
            return View(new ResponseViewModel { Questions = questions, SurveyTitle = survey.Title, SurveyId = surveyId.Value });
        }

        // POST: Responses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employee")]
        public ActionResult Create(ResponseViewModel response)
        {
            //response.Survey = db.Surveys.Find(response.SurveyId);
            response.Questions = db.Questions.Where(q => q.SurveyId.Equals(response.SurveyId)).ToList();
            if (response.Answers == null || !response.Answers.Any())
            {
                ModelState.AddModelError("", "A response must contain answer for each question.");
                return View(response);
            }
            if (response.Answers.Any(a => string.IsNullOrWhiteSpace(a)))
            {
                ModelState.AddModelError("", "All questions must have text.");
                return View(response);
            }
            if (ModelState.IsValid)
            {
                var savedResponse = db.Responses.Add(new Response { EmployeeId = this.User.Identity.GetUserId(), SurveyId = response.SurveyId });
                for (int i = 0; i < response.Answers.Count; i++)
                {
                    Answer temp = new Answer
                    {
                        AnswerDetails = response.Answers[i],
                        QuestionId = response.Questions[i].Id,
                        ResponseId = savedResponse.Id
                    };
                    db.Answers.Add(temp);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(response);
        }

        // GET: Responses/Edit/5
        [Authorize(Roles = "Employee")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Response response = db.Responses.Find(id);
            if (response == null)
            {
                return HttpNotFound();
            }
            ViewBag.SurveyId = new SelectList(db.Surveys, "Id", "AdminId", response.SurveyId);
            return View(response);
        }

        // POST: Responses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employee")]
        public ActionResult Edit([Bind(Include = "Id,SurveyId,EmployeeId")] Response response)
        {
            if (ModelState.IsValid)
            {
                db.Entry(response).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SurveyId = new SelectList(db.Surveys, "Id", "AdminId", response.SurveyId);
            return View(response);
        }

        // GET: Responses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Response response = db.Responses.Find(id);
            if (response == null)
            {
                return HttpNotFound();
            }
            return View(response);
        }

        // POST: Responses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Response response = db.Responses.Find(id);
            db.Responses.Remove(response);
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
