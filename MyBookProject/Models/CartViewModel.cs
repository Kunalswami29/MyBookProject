using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyBookProject.Models;

namespace MyBookProject.Models
{
    public class CartViewModel
    {
        public User User { get; set; }
        public Book Book { get; set; }
    }
}