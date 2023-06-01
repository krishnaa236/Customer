
using CustomerManagementSystem.Domain;
using CustomerManagementSystem.Filter;
using CustomerManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace CustomerManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly CustomerManagementContext _customerManagementContext;
        // GET: CustomerCRUD/Customer

        public AccountController()
        {
            _customerManagementContext = new CustomerManagementContext();
        }
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
       

        [HttpPost]
        public ActionResult Login(CustomerDetail customerDetail)
        {
            var user = _customerManagementContext.users.FirstOrDefault(users => users.User_name == customerDetail.UserName && users.password == customerDetail.Password && users.status == true);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid UserName Or Password");

                return View(customerDetail);
            }
            else if (user.Role == "Admin")
            {
                FormsAuthentication.SetAuthCookie(customerDetail.UserName, false);
                return RedirectToAction("CustomerList", "CustomerList", new { area = "" });
            }
            else if (user.Role == "Customer")
            {
                FormsAuthentication.SetAuthCookie(customerDetail.UserName, false);
                return RedirectToAction("CustomerDetail", "User", new { area = "UserCRUD" , id=0 });
            }
            return View(customerDetail);
        }
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("CustomerList", "CustomerList");
        }
       
    }
}