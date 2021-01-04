using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UniqueBooks.Models;

namespace UniqueBooks.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter Customer's Name.")]
        [StringLength(255)]
        public string CustomerName { get; set; }

        //[Display(Name = "Date Of Birth")]
        //[Min18YearsIfAMember]
        public DateTime? Birthdate { get; set; }

        //[Display(Name = "Subscribed To Newsletter")]
        public bool IsSubscribedToNewsletter { get; set; }

        //foreign key
        [Required]
        //it is implicitly required because type is byte
        //[Display(Name = "Membership Type")]
        public byte MembershipTypeId { get; set; }

        //NavigationProperty property
        public MembershipTypeDto MembershipType { get; set; }

    }
}