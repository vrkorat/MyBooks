 using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using UniqueBooks.Models;
using UniqueBooks.ViewModels;

namespace UniqueBooks.Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;
        
        public CustomersController()
        {
            //creating instance of context class for communicating with database
            //this is Disposable object
            _context = new ApplicationDbContext();
        }

        //it is also use in dependencies injection framework
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Customers
        [AllowAnonymous]
        public ActionResult Index()
        {
            //forStatic
            //var customers = GetCustomers();
            //when we iterate over a 'customers' at that time calling of database is made  
            var customers = _context.Customers.Include(cdata => cdata.MembershipType).ToList();
            if(User.IsInRole(RoleName.CanManageCustomer))
            {
                return View(customers);
            }
            return View("ReadOnlyCustomer", customers);

        }

        //GET: Customers/Details/id
        
        public ActionResult Details(int id)
        {
            //var customer = GetCustomers().SingleOrDefault(cdata => cdata.Id == id);
            //do the Eager fetching for navigation property of Customer, MembershipType
            var customer = _context.Customers.Include(cdata => cdata.MembershipType).SingleOrDefault(cdata => cdata.Id == id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            
            return View(customer);
        }

        //GET: customer/New

        [Authorize(Roles = RoleName.CanManageCustomer)]
        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel
            {
                //when we create a customer object it's property is 
                //initialized with default values like Customer.Id=0
                Customer = new Customer(),
                MembershipsTypes = membershipTypes
            };
            return View("CustomerForm",viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManageCustomer)]
        public ActionResult Save (Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipsTypes = _context.MembershipTypes.ToList()
                };
                return View("CustomerForm", viewModel);
            }

            if (customer.Id == 0)
            {
                _context.Customers.Add(customer);
            }
            else
            {
                //We should generally apply try catch over below 
                //because below statement return exception for not founding customer with 'customer.Id'
                //But here Does not need to try catch block
                //because this action method only called as result of posting our CustomerForm 
                var customerInDb = _context.Customers.Single(cdata => cdata.Id == customer.Id);

                customerInDb.CustomerName = customer.CustomerName;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            }


            //To see what is Coming in form through the Network in inspector
            //return new EmptyResult();
            _context.SaveChanges();
            return RedirectToAction("Index","Customers");
        }

        //GET: customers/edit/id
        [Authorize(Roles = RoleName.CanManageCustomer)]
        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(cdata => cdata.Id == id);
            if (customer == null)
            {
                return HttpNotFound();

            }

            var viewModel = new CustomerFormViewModel 
            {
                Customer = customer,
                MembershipsTypes =  _context.MembershipTypes.ToList()
            };

            return View("CustomerForm",viewModel);
        }


        //private IEnumerable<Customer> GetCustomers()
        //{
        //    IList<Customer> customers = new List<Customer>()
        //    {
        //        new Customer(){Id = 1,CustomerName = "John Williaums"},
        //        new Customer(){Id = 2,CustomerName = "Mary Williaums"}
        //    };
        //    return customers;
        //}
    }
}