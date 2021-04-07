using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBookProject.Models;

namespace MyBookProject.Controllers
{
    public class AdminController : Controller
    {
        private OnlineBookStoreDbEntities context;

        public AdminController()
        {
            context = new OnlineBookStoreDbEntities();
        }

        protected override void Dispose(bool disposing)
        {
            context.Dispose();
        }


        // get : HttpGet 
        public ActionResult AddBook()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddBook(Book book)
        {
            if (!ModelState.IsValid)
            {
                return HttpNotFound();
            }

            context.Books.Add(book);
            context.SaveChanges();

            ViewBag.Message = "Book Added Successfully !!";

            return View();
        }
    }
}