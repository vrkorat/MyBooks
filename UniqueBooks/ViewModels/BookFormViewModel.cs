using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using UniqueBooks.Models;

namespace UniqueBooks.ViewModels
{
    public class BookFormViewModel
    {
        public IEnumerable<Genre> Genres { get; set; }
        public Book Book { get; set; }
        public String Title
        {
            get
            {
                if (this.Book != null && this.Book.Id != 0)
                {
                    return "Edit Book";
                }
                else
                {
                    return "New Book";
                }
            }
            set { this.Title = value; }

        }
    }
}