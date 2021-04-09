﻿using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBookProject.Models;
using Microsoft.AspNet.Identity;
using System.Net;
using Microsoft.Owin.Security;

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

        //for Registering the User

        [HttpPost]
        public ActionResult Register(User user)
        {
            if (!ModelState.IsValid)
            {
                return HttpNotFound();
            }

            context.Users.Add(user);
            context.SaveChanges();
            
            

            ViewBag.Message = "Thanks For Registering,This Is Your UserId"+ " " +user.UserId+ " " + "Use It for Login";

            return View();
        }

        // for Accessing the Dashboard 
        public ActionResult Dashboard()
        {


            return View("Dashboard");
        }

        //For Accessing the Login Page
        public ActionResult Login()
        {
            
            return View();
        }

        
        // For Authenticating the user
        [HttpPost]
        
        [ValidateAntiForgeryToken()]
        public ActionResult Login(User objUser)
        {
            if (ModelState.IsValid)
            {
                using (OnlineBookStoreDbEntities db = new OnlineBookStoreDbEntities())
                {
                    var obj = db.Users.Where(a => a.UserId.Equals(objUser.UserId) && a.PasswordHash.Equals(objUser.PasswordHash)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["UserID"] = obj.UserId.ToString();
                        Session["FirstName"] = obj.FirstName.ToString();
                        if(obj.UserCategory == "Admin")
                        {
                            return RedirectToAction("Dashboard", "User");
                        }
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                            ViewBag.Message = "Invalid Username/Password !!";  
                    }
                }
            }
            return View();
        }


        // For Ending the Session And LogOut
        public ActionResult LogOut()
        {
            
            string UserId = (string)Session["UserId"];
            Session.Clear();
            return RedirectToAction("Login", "User");

        }

        
       


    }
}