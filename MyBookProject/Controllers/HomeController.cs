using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public ViewResult Index()
        {
            return View();
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

        public ActionResult Details (int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = context.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        public ActionResult ResourceList(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            var book = from b in context.Books
                       select b;
            if (!String.IsNullOrEmpty(searchString))
            {
                book = book.Where(b => b.BookName.Contains(searchString)
                                       || b.Book_Auth.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    book = book.OrderByDescending(b => b.Book_Auth);
                    break;

                default:
                    book = book.OrderBy(b => b.BookName);
                    break;
            }

            //var book = context.Books.ToList();

            return View(book.ToList());
        }
       
        
    }
}