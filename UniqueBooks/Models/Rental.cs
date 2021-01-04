using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Web;

namespace UniqueBooks.Models
{
    public class Rental
    {
        public int Id { get; set; }

        public DateTime DateRented { get; set; }

        public DateTime? DateReturned { get; set; }
        //foreign key
        [Required]
        public int BookId { get; set; }
        
        public Book Book { get; set; }
        
        //foreign key
        [Required]
        public int CustomerId { get; set; }
        
        public Customer Customer { get; set; }
    }
}