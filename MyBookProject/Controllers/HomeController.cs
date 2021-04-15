using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBookProject.Models;

namespace MyBookProject.Controllers
{
    public class HomeController : Controller
    {
        private OnlineBookStoreDbEntities context;

        public HomeController()
        {
            context = new OnlineBookStoreDbEntities();
        }

        protected override void Dispose(bool disposing)
        {
            context.Dispose();
        }

        public ActionResult Index()
        {
            var books = context.Books.ToList();

            return View(books);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }

       
       
        
    }
}