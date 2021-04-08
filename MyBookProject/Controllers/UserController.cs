using System.Linq;
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

        [HttpPost]
        public ActionResult Register(User user)
        {
            if (!ModelState.IsValid)
            {
                return HttpNotFound();
            }

            context.Users.Add(user);
            context.SaveChanges();
            
            

            ViewBag.Message = "Thanks For Registering,This Is Your UserId ,Use It for Login"+" "+user.UserId;

            return View();
        }

        public ActionResult Dashboard()
        {


            return View("Dashboard");
        }
        public ActionResult Login()
        {
            
            return View();
        }

        
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
                        return RedirectToAction("Dashboard", "User");
                    }
                    else
                    {
                            ViewBag.Message = "Invalid Username/Password !!";  
                    }
                }
            }
            return View();
        }


        public ActionResult LogOut()
        {
            
            string UserId = (string)Session["UserId"];
            Session.Clear();
            return RedirectToAction("Login", "User");

        }

        
       


    }
}