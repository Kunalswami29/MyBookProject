using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBookProject.Models;

namespace MyBookProject.Controllers
{
    public class UserController : Controller
    {
        private OnlineBookStoreDbEntities context;

        public UserController()
        {
            context = new OnlineBookStoreDbEntities();
        }

        protected override void Dispose(bool disposing)
        {
            context.Dispose();
        }

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            if (!ModelState.IsValid)
            {
                return HttpNotFound();
            }

            context.Users.Add(user);
            context.SaveChanges();

            ViewBag.Message = "Thanks For Registering,This Is Your UserId";

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }


    }
}