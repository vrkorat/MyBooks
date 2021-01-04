using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniqueBooks.Models
{
    public class Book
    {

        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Book Name")]
        public string BookName { get; set; }

        [Required]
        [Display(Name = "Author Name")]
        public string AuthorName { get; set; }

        [Required]
        [Display(Name = "Publisher Name")]
        public string PublisherName { get; set; }

        //Default Format for DateTime is mm/dd/yyyy hh/mm/ss;
        [Display(Name = "Publish Date")]
        public DateTime PublishDate { get; set; }

        public DateTime  DateAdded { get; set; }

        [Required]
        [Range(1, 20)]
        [Display(Name = "Number in Stock")]
        public byte NumberInStock { get; set; }

        public byte NumberAvailable { get; set; }

        //public byte[] bookImage/pdf { get; set; }

        //foreign key
        [Required]
        [Display(Name = "Genre")]
        public byte GenreId { get; set; }
       
        //navigation property of Book,'Genre'
        public Genre Genre { get; set; }

    }
}    