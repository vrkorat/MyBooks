using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;
using Microsoft.Ajax.Utilities;
using UniqueBooks.Models;

namespace UniqueBooks.ViewModels
{
    public class CustomerFormViewModel
    {
        public IEnumerable<MembershipType> MembershipsTypes { get; set; }
        public Customer Customer { get; set; }
        public string Title {
            get
            {
                if (this.Customer != null && this.Customer.Id != 0)
                {
                    return "Edit Customer";
                }
                else
                {
                    return "New Customer";
                }
            }
            set { this.Title = value; }
        }
    }
}