using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomerController : Controller
    {
        CustomerViewModel customerViewModel;
        private ApplicationDbContext _context;

        public CustomerController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            //customerViewModel = new CustomerViewModel();
            //var Customers = _context.Customers.Include(c => c.MembershipType).ToList();

            return View();
        }

        public ActionResult Details(int id)
        {
            Customers customer = _context.Customers.Include(c => c.MembershipType).FirstOrDefault(x => x.ID == id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();

            var viewModel = new CustomerFormViewModel
            {
                Customer = new Customers(),
                MembershipTypes = membershipTypes
            };

            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customers customer)
        {
            if(!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes
                };

                return View("CustomerForm", viewModel);
            }

            if(customer.ID == 0)
            {
                _context.Customers.Add(customer);
            }
            else
            {
                Customers customertoUpdate = _context.Customers.Include(c => c.MembershipType).FirstOrDefault(x => x.ID == customer.ID);
                customertoUpdate.Name = customer.Name;
                customertoUpdate.BirthDate = customer.BirthDate;
                customertoUpdate.MembershipTypeId = customer.MembershipTypeId;
                customertoUpdate.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
            }
            
            _context.SaveChanges();

            return RedirectToAction("Index", "Customer");
        }

        public ActionResult Edit(int id)
        {
            Customers customer = _context.Customers.Include(c => c.MembershipType).FirstOrDefault(x => x.ID == id);

            if (customer == null)
                return HttpNotFound();

            var viewModel = new CustomerFormViewModel
            {
                MembershipTypes = _context.MembershipTypes.ToList(),
                Customer = customer
            };

            return View("CustomerForm", viewModel);
        }
    }
}