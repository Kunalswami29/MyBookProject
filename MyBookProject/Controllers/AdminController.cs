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


       
        public ActionResult Update()
        {
            return View();
        }

        [HttpPost, ActionName("Update")]
        [ValidateAntiForgeryToken]
        public ActionResult UpdatePost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var BookToUpdate = context.Books.Find(id);
            if (TryUpdateModel(BookToUpdate))
            {
                try
                {
                    context.SaveChanges();
                    ViewBag.Message = "Book Updated Successfully !!";
                    return ViewBag;
                }
                catch (DataException)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }

            return View("AddBook");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
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