using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBookProject.Models;
using Microsoft.AspNet.Identity;
using System.Configuration;
using System.Data.SqlClient;


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
            if (Session["UserId"] == null)
            {
                return View();
            }

            return RedirectToAction("Index", "Home");
           
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
            
            

            ViewBag.Message = "Thanks For Registering,This Is Your UserId"+ " " + user.UserId + " " + "Use It for Login";

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
            if (Session["UserId"] == null)
            {
                return View();
            }

            return RedirectToAction("Index", "Home");

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
                        Session["UserCategory"] = obj.UserCategory.ToString();
                        if(obj.UserCategory == "Admin")
                        {
                            return RedirectToAction("Index", "Admin");
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

       
        public ActionResult ResetPassword()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult ResetPassword(User use)
        {

            var user = context.Users.Single(f => f.Email == use.Email && f.Contact_Number == use.Contact_Number && f.Secret == use.Secret);

            if(user == null)
            {
                 ViewBag.Message = "Wrong Credentials Please register or try again";
                return View();
            }

            string v = user.UserId.ToString();

            ViewBag.Message = "This is your UserId"+ " " + v; 
          

            return View();
        }

        public ActionResult Forgot()
        {
            return View();
        }
       


    }
}