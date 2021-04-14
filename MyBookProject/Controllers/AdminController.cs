using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using MyBookProject.Models;
using System.Data;
using System.Data.Entity;


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




        // get : HttpGet  to show all books
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


        

        // to update/Add Book to DB
        [HttpPost]
        public ActionResult Save (Book book)
        {
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (book.BookId == 0)
            {
                context.Books.Add(book);
                ViewBag.Message = "Book Added Successfully";
            }
            else
            {
                var bookInDb = context.Books.Single(m => m.BookId == book.BookId);
                if (bookInDb != null)
                {
                    bookInDb.BookName = book.BookName;
                    bookInDb.BookCode = book.BookCode;
                    bookInDb.Book_Auth = book.Book_Auth;
                    bookInDb.Book_Cat = book.Book_Cat;
                    bookInDb.Book_des = book.Book_des;
                    bookInDb.Book_Opt = book.Book_Opt;
                    bookInDb.Book_Type = book.Book_Type;
                    bookInDb.Discount = book.Discount;
                    bookInDb.Rate = book.Rate;
                }

            }
           
            context.SaveChanges();

          

            return RedirectToAction("Index", "Admin");
        }


       // to render details to the Update Page 
        public ActionResult Update(int? id)
        {
            var book = context.Books.SingleOrDefault(c => c.BookId == id);
            if (book == null)
            {
                return HttpNotFound();
            }

            return View(book);

        }

       

        
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