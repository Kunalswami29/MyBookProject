//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyBookProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            this.Orders = new HashSet<Order>();
            this.Payments = new HashSet<Payment>();
            this.Status = new HashSet<Status>();
            this.Tickets = new HashSet<Ticket>();
        }
    
        public int UserId { get; set; }
       
        [Display(Name ="First Name")]
   
        public string FirstName { get; set; }
        
        [Display(Name = "Last Name")]
        
        public string LastName { get; set; }
        
        [Display(Name = "Date Of Birth")]
        
        public Nullable<System.DateTime> DOB { get; set; }
        
        
        [Display(Name = "Gender")]
        public string Gender { get; set; }
        
        [Display(Name = "Contact Number")]
        public string Contact_Number { get; set; }
        
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }
       
        [Display(Name = "Category")]
        public string UserCategory { get; set; }
        
        [Display(Name = "What is Your Favourite Colour?")]
        public string Secret { get; set; }
        [Required]
        [Display(Name = "Password")]
        
        
        public string PasswordHash { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Payment> Payments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Status> Status { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
