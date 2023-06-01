using CustomerManagementAPIs.Models;
using CustomerManagementSystem.Domain;
using CustomerManagementSystem.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using HttpPutAttribute = System.Web.Http.HttpPutAttribute;

namespace CustomerManagementAPI.Controllers
{
    public class CustomerListController : ApiController
    {
       
            private readonly CustomerManagementContext _customerManagementContext;
            // GET: CustomerCRUD/Customer

            public CustomerListController()
            {
                _customerManagementContext = new CustomerManagementContext();
            }


        
        [HttpGet]
        public CustomerListModel CustomerList()
        {
            CustomerListModel customerList = new CustomerListModel();
            customerList.customers = _customerManagementContext.Customers.ToList();
            return customerList;
        }

        [HttpPut]
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
                if (user != null)
                {
                    user.status = false;
                    user.deleted_at = DateTime.Now;
                    dbContext.SaveChanges();
                }
            }

        }
    }
}