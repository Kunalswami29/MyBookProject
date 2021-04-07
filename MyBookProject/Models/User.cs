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
    using System.ComponentModel.DataAnnotations.Schema;
    
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            this.Orders = new HashSet<Order>();
            this.Payments = new HashSet<Payment>();
            this.Status = new HashSet<Status>();
        }
    
        public int UserId { get; set; }
        //[Required(ErrorMessage ="Please Enter the First Name")]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }
       // [Required(ErrorMessage = "Please Enter the Last Name")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

       // [Required(ErrorMessage = "Please Enter the DOB")]
        [Display(Name = "Date Of Birth")]
        public Nullable<System.DateTime> DOB { get; set; }
      //  [Required(ErrorMessage = "Please Enter the Gender")]
        [Display(Name = "Gender")]
        public string Gender { get; set; }
       // [Required(ErrorMessage = "Please Enter the Contact")]
        [Display(Name = "Contact No.")]
        public string Contact_Number { get; set; }
       // [Required(ErrorMessage = "Please Enter the First Name")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
       // [Required(ErrorMessage = "Please Enter the Category")]
        [Display(Name = "User Categry")]
        public string UserCategory { get; set; }
       // [Key]
       // [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public Nullable<int> User_NO { get; set; }
       // [Required(ErrorMessage = "Please Enter the Password")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string PasswordHash { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Payment> Payments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Status> Status { get; set; }
    }
}
