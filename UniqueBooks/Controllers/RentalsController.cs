using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;
using UniqueBooks.Models;

namespace UniqueBooks.Controllers
{
    public class RentalsController : Controller
    {
        private ApplicationDbContext _context;

        public RentalsController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Rentals
        public ActionResult New()
        {
            return View();
        }

        //GET: Rentals/ShowRentals
        //[Authorize(Roles = RoleName.CanManageCustomer)]
        public ActionResult ShowRentals(int id)
        {
            var renter = _context.Rentals
                .Include(c => c.Customer)
                .Include(b => b.Book)
                .Where(r => r.CustomerId == id)
                .ToList();
            
            return View(renter);
        }
    }
}