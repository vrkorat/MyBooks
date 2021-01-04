using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using UniqueBooks.Models;
using UniqueBooks.ViewModels;

namespace UniqueBooks.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private ApplicationDbContext _context;

        public BooksController()
        {
            //creating instance of context class for communicating with database
            //this is Disposable object
            _context = new ApplicationDbContext();
        }

        //this is Disposable object we should override the _context
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        //GET: Books
        [AllowAnonymous]
        public ActionResult Index(int? pageIndex, string sortBy)
        {
            if (!pageIndex.HasValue)
            {
                pageIndex = 1;
            }

            if (String.IsNullOrWhiteSpace(sortBy))
            {
                sortBy = "BookName";
            }
            //return Content(String.Format("pageIndex = {0} and sortBy = {1}", pageIndex, sortBy));

            //for static
            //var books = GetBooks();
            var books = _context.Books.Include(bdata => bdata.Genre).ToList();
            if (User.IsInRole(RoleName.CanManageCustomer))
            {
                return View("List",books);
            }

            return View("ReadOnlyList", books);

        }

        //GET: Books/New
        [Authorize(Roles = RoleName.CanManageCustomer)]
        public ActionResult New()
        {
            var genres = _context.Genres.ToList();
            var viewModel = new BookFormViewModel
            {
                //Why instantiate book object and pass it in viewModel
                //because we need Book.Id=0
                //But by doing s other not NullAble property like PublishDate 
                //also initialized to 1 jan 0001 also NumberInStock is initialized to 0
                //Book = new Book(),

                //to reslove this we add hidden file condition in BookFrom abive 
                //submit button limitation of it you not able to change/rename the prop.'Book.Id'
                Genres = genres
            };
            return View("BookForm",viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManageCustomer)]
        public ActionResult Save(Book book)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new BookFormViewModel
                {
                    Book = book,
                    Genres = _context.Genres.ToList()
                };
                return View("BookForm", viewModel);
            }

            //we are using Book.Id as identifier for Add New Book(Id = 0)
            if (book.Id == 0)
            {
                book.DateAdded = DateTime.Now;
                _context.Books.Add(book);
            }
            //for Update Book (Id != 0)
            else
            {
                var bookInDb = _context.Books.Single(bdata => bdata.Id == book.Id);

                bookInDb.BookName = book.BookName;
                bookInDb.AuthorName = book.AuthorName;
                bookInDb.PublisherName = book.PublisherName;
                bookInDb.PublishDate = book.PublishDate;
                bookInDb.NumberInStock = book.NumberInStock;
                bookInDb.GenreId = book.GenreId;
                bookInDb.NumberAvailable = book.NumberInStock;

            }
            _context.SaveChanges();
            return RedirectToAction("Index","Books");
        }

        //GET: Books/Edit/id
        [Authorize(Roles = RoleName.CanManageCustomer)]
        public ActionResult Edit(int id)
        {
            var book = _context.Books.SingleOrDefault(bdata => bdata.Id == id);

            if (book == null)
            {
                return HttpNotFound();
            }
            var viewModel = new BookFormViewModel
            {
                Book = book,
                Genres = _context.Genres.ToList()
            };
            return View("BookForm",viewModel);
        }

        //GET: Books/Details
        public ActionResult Details(int id)
        {
            //var book = GetBooks().SingleOrDefault(bdata => bdata.Id == id);
            var book = _context.Books.Include(bdata => bdata.Genre).SingleOrDefault(bdata => bdata.Id == id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        //GET: Books/Published/year/month
        [Route("Books/Published/{year:regex(^\\d{4}$)}/{month:regex(^\\d{2}$):range(1,12)}")]
        public ActionResult ByPublishDate(int year, int month)
        {
            return Content(year + "/" + month);
        }

        // GET: Books/Random
        public ActionResult Random()
        {
            var book = new Book()
            {
                BookName = "ASP.NET 4.5 in C#",
                AuthorName = "Mosh Hamedani",
                PublisherName = "Udemy Publication",
                PublishDate = new DateTime(2015, 12, 08)
            };
            var customers = new List<Customer>()
            {
                new Customer(){CustomerName = "customer 1"},
                new Customer(){CustomerName = "customer 2"}
            };

            var viewModel = new RandomBookViewModel
            {
                Book = book,
                Customers = customers
            };

            //3.way
            return View(viewModel);

            //1.way ViewData["RandomBook"] = book1;
            //2.way ViewBag.RandomBook = book1;
            //return View();


            //return Content("Hello World!" );
            //return HttpNotFound();
            //return new EmptyResult();
            //return RedirectToAction("index", "Home", new{ page = 1,sortBy = "BookName"});


        }

        //private IEnumerable<Book> GetBooks()
        //{
        //    IList<Book> books = new List<Book>()
        //    {
        //        new Book(){ Id = 1, BookName = "Concepts of physics 1", PublisherName = "Bharati Bhawan", PublishDate = new DateTime(2015,08,12),AuthorName = "H.C.Verma"},
        //        new Book(){ Id = 2, BookName = "Concepts of physics 2", PublisherName = "Bharati Bhawan", PublishDate = new DateTime(2015,08,12),AuthorName = "H.C.Verma"},
        //    };
        //    return books;
        //}

    }

}