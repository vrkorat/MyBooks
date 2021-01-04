using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;

namespace UniqueBooks.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter Customer's Name.")]
        [StringLength(255)]
        public string CustomerName { get; set; }

        [Display(Name = "Date Of Birth")]
        [Min18YearsIfAMember]
        [DisplayFormat(DataFormatString = @"{0:dd MMMM,yyyy}")]
        public DateTime? Birthdate { get; set; }

        [Display(Name = "Subscribed To Newsletter")]
        public bool IsSubscribedToNewsletter { get; set; }
        
        //foreign key
        [Required]
        //it is implicitly required because type is byte
        [Display(Name = "Membership Type")]
        public byte MembershipTypeId { get; set; }
        
        //NavigationProperty property
        public MembershipType MembershipType { get; set; }

    }
}