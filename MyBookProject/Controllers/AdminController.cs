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




        // get : HttpGet  to show all books in sorted or searched form
        public ActionResult Index(string sortOrder, string searchString)
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



                return View(book.ToList());
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
                ViewBag.Message = "Book Updated Successfully !!";

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

       

        // For Deleting the User
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
                
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public ActionResult Ticket()
        {
            

            var ticket = context.Tickets.ToList();

            return View(ticket);
        }

        public ActionResult Remove(int? id)
        {
            try
            {
                Ticket ticket = context.Tickets.Find(id);
                context.Tickets.Remove(ticket);
                context.SaveChanges();
            }
            catch (DataException)
            {

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {

            var ticket= context.Tickets.SingleOrDefault(c => c.TicketId == id);
            if (ticket == null)
            {
                return HttpNotFound();
            }

            return View(ticket);
        }

        public ActionResult Resolve(Ticket ticket)
        {
            if (!ModelState.IsValid)
            {
                return View("Error");
            }

            if(ticket.TicketId != 0)
            {
                var ticketInDb = context.Tickets.Single(t => t.TicketId == ticket.TicketId);

                ticketInDb.UserId = ticket.UserId;
                ticketInDb.FirstName = ticket.FirstName;
                ticketInDb.Description = ticket.Description;
                ticketInDb.Date = ticket.Date;
                ticketInDb.status = ticket.status;

             
            }


            context.SaveChanges();

            return RedirectToAction("Index");

        }
    }
}