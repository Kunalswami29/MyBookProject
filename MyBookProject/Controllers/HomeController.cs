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
                                       || b.Book_Auth.Contains(searchString) || b.Book_Cat.Contains(searchString));
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

        public ActionResult Order()
        {
            if(Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Order(Order order)
        {
            if (!ModelState.IsValid)
            {
                return HttpNotFound();
            }

            if(order.OrderId == 0)
            {
                order.DueDate = DateTime.Today.AddDays(15);
                context.Orders.Add(order);
                ViewBag.Message = "Proceed to Checkout Please !!";
                context.SaveChanges();
            }



            return View();

        }

        public ActionResult Payment()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            
            return View();
        }

        [HttpPost]
        public ActionResult Payment(Payment payment)
        {
            if(payment.PaymentId == 0)
            {
                context.Payments.Add(payment);
                ViewBag.Message = "Thanks Your Order Has Been Placed Successfully!!";
                context.SaveChanges();
            }

            return View();
        }



    }
}