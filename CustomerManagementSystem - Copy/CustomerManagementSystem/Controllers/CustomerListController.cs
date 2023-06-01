


using CustomerManagementSystem.Domain;
using CustomerManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomerManagementSystem.Controllers
{
    public class CustomerListController : Controller
    {

        [Authorize(Roles="Admin")]
        public ActionResult CustomerList()
        {
            using (CustomerManagementContext dbContext = new CustomerManagementContext())
            {
                CustomerListModel customerListModel = new CustomerListModel();
                customerListModel.customers = dbContext.Customers.Where(Customer=>Customer.deleted_at==null).ToList();
                return View(customerListModel);
            }
           
        }
        [Authorize(Roles = "Admin")]
        public void DeleteCustomer(long customer_Id)
        {
            using (CustomerManagementContext dbContext = new CustomerManagementContext())
            {
                var Customer = dbContext.Customers.FirstOrDefault(Customers => Customers.customer_id == customer_Id);
                if (Customer != null)
                {
                     Customer.deleted_at = DateTime.Now;
                    dbContext.SaveChanges();
                }
                var user = dbContext.users.FirstOrDefault(Users => Users.customer_id == customer_Id);
                if(user != null)
                {
                    user.status = false;
                    user.deleted_at=DateTime.Now;
                    dbContext.SaveChanges();
                }
            }

        }
    }
}