using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Web.Http;
using UniqueBooks.Dtos;
using UniqueBooks.Models;

namespace UniqueBooks.Controllers.Api
{
    public class NewRentalsController : ApiController
    {
        private ApplicationDbContext _context;

        public NewRentalsController()
        {
            _context = new ApplicationDbContext();
        }


        [HttpPost]
        public IHttpActionResult CreateNewRentals(NewRentalDto newRental)
        {
            //throw new NotImplementedException("Under Construction");
            var customer = _context.Customers.Single(
                c => c.Id == newRental.CustomerId);
            foreach (var bookId in newRental.bookIds)
            {
                var book = _context.Books.Single(bdata => bdata.Id == bookId);

                if (book.NumberAvailable == 0)
                    return BadRequest("Book Is Not Available.");

                book.NumberAvailable--;

                var rental = new Rental()
                {
                    //CustomerId = customer.Id,
                    Customer = customer,
                    //BookId = book.Id,
                    Book = book,
                    DateRented = DateTime.Now
                };

                _context.Rentals.Add(rental);
            }
            
            _context.SaveChanges();

            return Ok();
            //return View();

        }

        [HttpGet]
        public IHttpActionResult SetReturned(int id)
        {
            var renter = _context.Rentals.SingleOrDefault(r => r.Id == id);
            renter.DateReturned = DateTime.Now;

            var book = _context.Books.SingleOrDefault(b => b.Id == renter.BookId);
            book.NumberAvailable++;

            _context.SaveChanges();
            return Ok(renter);
        }
    }
}
