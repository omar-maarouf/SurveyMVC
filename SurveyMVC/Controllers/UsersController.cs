using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SurveyMVC.Models;

namespace SurveyMVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : AccountController
    {

        // GET: Users
        public ActionResult Index()
        {
            var users = UserManager.Users.ToList();
            List<UserViewModel> list = new List<UserViewModel>();
            foreach (var user in users)
            {
                if (!(user.Id.Equals(this.User.Identity.GetUserId())) && !(user.UserName.Equals("admin")))
                {
                    list.Add(new UserViewModel { User = user, IsAdmin = UserManager.IsInRole(user.Id, "Admin") });
                }
        }
            //var user = UserManager.Users.FirstOrDefault().Roles.FirstOrDefault();
            return View(list);
        }

        // POST: Users/EditRole/?id=5&admin=false
        [HttpPost]
        public ActionResult EditRole(string id, bool admin)
        {
            var user = UserManager.Users.Where(u => u.Id.Equals(id)).FirstOrDefault();
            if (admin)
            {
                UserManager.AddToRole(user.Id, Role.Admin.ToString());
                UserManager.RemoveFromRole(user.Id, Role.Employee.ToString());
            }
            else
            {
                UserManager.AddToRole(user.Id, Role.Employee.ToString());
                UserManager.RemoveFromRole(user.Id, Role.Admin.ToString());
            }
            return RedirectToAction("Index");
        }
    }
}
