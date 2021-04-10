using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using MyBookProject.Models;
using System.Data;

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
        public ActionResult Index()
        {
            var books = context.Books.ToList();

            return View(books);
        }

       

        // to render the addBook PAge
        public ActionResult AddBook()
        {
            return View();
        }


        

        // to add Book to DB
        [HttpPost]
        public ActionResult Save (Book book)
        {
            if (book.BookId == 0)
            {
                context.Books.Add(book);
            }
            else
            {
                var bookInDb = context.Books.SingleOrDefault(b => b.BookId == book.BookId);

                bookInDb.BookName = book.BookName;
                bookInDb.Book_Auth = book.Book_Auth;
                bookInDb.Book_des = book.Book_des;
                bookInDb.Book_Type = book.Book_Type;
                bookInDb.Book_Cat = book.Book_Cat;
                bookInDb.Book_Opt = book.Book_Opt;
                bookInDb.Discount = book.Discount;
                bookInDb.Rate = book.Rate;
            }

            context.SaveChanges();

            ViewBag.Message = "Book Added Successfully !!";

            return View("AddBook");
        }


       // to render details to the Update Page 
        public ActionResult Update(int? id)
        {
            var book = context.Books.Where(c => c.BookId == id).FirstOrDefault();
            if (book == null)
            {
                return HttpNotFound();
            }

            return View("Update");

        }

        // to  update the details in Db

       /* [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateBook(Book book)
        {
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            


            return View("AddBook");
        }*/


        
        public ActionResult Delete(int? id)
        {
            try
            {
                Book book = context.Books.Find(id);
                context.Books.Remove(book);
                context.SaveChanges();
            }
            catch (DataException)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }
    }
}