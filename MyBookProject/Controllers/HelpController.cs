using MyBookProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Web;
using System.Web.Mvc;
namespace MyBookProject.Controllers
{
    public class HelpController : Controller
    {
        private OnlineBookStoreDbEntities context;

        public HelpController()
        {
            context = new OnlineBookStoreDbEntities();
        }




        // GET: Help
        public ActionResult Index()
        {
            
            return View();
        }


        [HttpPost]
        public ActionResult Index(Ticket ticket)
        {
            
            if(ticket.TicketId == 0)
            {
                //ticket.Date = DateTime.Now;

                context.Tickets.Add(ticket);

                context.SaveChanges();
                ViewBag.Message = "Thanks  " + ticket.FirstName +"Your Query Will  Resolved Shortly"+ "Your Ticket ID is" + "  " + ticket.TicketId;
            }
           

          

            return View();
        }
    }
}