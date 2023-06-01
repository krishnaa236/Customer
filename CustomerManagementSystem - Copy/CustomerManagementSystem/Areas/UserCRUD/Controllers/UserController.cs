

using CustomerManagementSystem.Areas.CustomerCRUD.Models;
using CustomerManagementSystem.Domain;
using CustomerManagementSystem.Domain.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CustomerManagementSystem.Areas.CustomerCRUD.Controllers
{
    public class UserController : Controller
    {
        private readonly CustomerManagementContext _customerManagementContext;
       
        // GET: CustomerCRUD/Customer

        public UserController()
        {
            _customerManagementContext = new CustomerManagementContext();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult AddUser()
        {

            return View();
        }


        [HttpPost]
        public ActionResult AddUser(UserDetail userDetail)
        {
            Customer customer = new Customer();
            Admin admin = new Admin();
            if (ModelState.IsValid)
            {
                if (userDetail.Role != EnumsforRoles.Admin)
                {
                    customer.customer_id=userDetail.AddCustomer(userDetail, _customerManagementContext,customer);                  
                }
                else
                {
                    admin.Admin_id = userDetail.AddAdmin(userDetail, _customerManagementContext, admin);                  
                }
                user user = new user();              
                userDetail.AddUser(userDetail, _customerManagementContext,user, customer.customer_id,admin.Admin_id);
                TempData["AddUser"] = "Data Added Successfully";
                return RedirectToAction("CustomerList", "CustomerList", new { area = "" });
            }
            else
            {
                return View();
            }

        }

        [Authorize(Roles = "Admin")]
        public ActionResult UpdateCustomer(long id)
        {
            var Customer = _customerManagementContext.Customers.FirstOrDefault(Customers => Customers.customer_id == id);
            var user = _customerManagementContext.users.FirstOrDefault(Users => Users.customer_id == id);
            if (Customer == null)
            {
                return RedirectToAction("CustomerList", "CustomerList", new { area = "" });
            }
            else
            {

                UserDetail userDetail = new UserDetail();
                var UserDetails=userDetail.GetCustomerData(userDetail,id, _customerManagementContext);             
                return View(UserDetails);
            }

        }
        [HttpPost]
        public ActionResult UpdateCustomer(UserDetail userDetail)
        {

            if (ModelState.IsValid)
            {
                var CurrentUser = User.Identity.Name;
                var CurrenUserRole = _customerManagementContext.users.FirstOrDefault(Users => Users.User_name == CurrentUser);
                long CurrenUserID = CurrenUserRole.User_id;
                var Customer = _customerManagementContext.Customers.FirstOrDefault(Customers => Customers.customer_id == userDetail.customer_id);
                var user = _customerManagementContext.users.FirstOrDefault(Users => Users.customer_id == userDetail.customer_id);
                userDetail.UpdateCustomer(userDetail, _customerManagementContext, Customer);
                userDetail.UpdateUser(userDetail, _customerManagementContext, user, CurrenUserID);
               
                return RedirectToAction("CustomerList", "CustomerList", new { area = "" });
            }
            else
            {
                return View(userDetail);
            }

        }
        [Authorize(Roles = "Customer,Admin")]

        public ActionResult ChangePassword()
        {
            return View();
        }


        [HttpPost]
        public ActionResult ChangePassword(ChangePassword changePassword)
        {
            string userName = User.Identity.Name;
            if (changePassword.OldPassword == changePassword.NewPassword && changePassword.OldPassword != null && changePassword.NewPassword != null)
            {
                ModelState.AddModelError("", "Old Password and New Password are same");
                return View();
            }
            if (changePassword.ConfirmPassword != changePassword.NewPassword && changePassword.ConfirmPassword != null && changePassword.NewPassword != null)
            {
                ModelState.AddModelError("", "New Password and Confirm Password didn't match");
                return View();
            }
            if (ModelState.IsValid)
            {
                var user = _customerManagementContext.users.FirstOrDefault(User => User.User_name == userName);
                if (user.password == changePassword.OldPassword)
                {
                    user.password = changePassword.NewPassword;
                    user.updated_at = DateTime.Now;
                    user.Updated_by = user.User_id;
                    _customerManagementContext.SaveChanges();
                    TempData["PasswordChanged"] = "Password Changed";
                    if (user.Role != EnumsforRoles.Admin)
                    {
                        return RedirectToAction("CustomerDetail", "User", new { area = "UserCRUD"  ,id = 0 });
                    }
                    else
                    {
                        return RedirectToAction("CustomerList", "CustomerList", new { area = "" });
                    }
                    
                }
                else
                {
                    ModelState.AddModelError("OldPassword", "Please Enter the Right Password");
                    return View();
                }

            }

            return View();
        }
        [Authorize(Roles = "Customer,Admin")]
        public ActionResult UserEditProfile()
        {
            string userName = User.Identity.Name;

            var user = _customerManagementContext.users.FirstOrDefault(Users => Users.User_name == userName);
            if (user.Role != EnumsforRoles.Admin)
            {
                var Customer = _customerManagementContext.Customers.FirstOrDefault(Customers => Customers.customer_id == user.customer_id);
                if (Customer == null)
                {
                    return RedirectToAction("Login", "Account", new { area = "" });
                }
                else
                {
                    UserEditProfile userEditProfile = new UserEditProfile();
                    var CustomerData=userEditProfile.GetCustomerData(userEditProfile,Customer,user);             
                    return View(CustomerData);
                }
            }
            else
            {
                var Admin = _customerManagementContext.Admins.FirstOrDefault(Admins => Admins.Admin_id == user.Admin_id);
                if (Admin == null)
                {
                    return RedirectToAction("Login", "Account", new { area = "" });
                }
                else
                {
                    UserEditProfile userEditProfile = new UserEditProfile();
                    var AdminData=userEditProfile.GetAdminData(userEditProfile, Admin, user);
                    
                    return View(AdminData);
                }
            }
            
        }
        [HttpPost]
        public ActionResult UserEditProfile(UserEditProfile userEditProfile)
        {
            if (ModelState.IsValid)
            {
                string userName = User.Identity.Name;
                var user = _customerManagementContext.users.FirstOrDefault(Users => Users.User_name == User.Identity.Name);
                var file = Request.Files[0];
                if (file.ContentLength == 0)
                {
                    var fileName = "";
                    var baseUrl = "";
                    userEditProfile.UpdateUserProfile(userEditProfile, user, _customerManagementContext, Convert.ToString(fileName), Convert.ToString(baseUrl));
                }
                else
                {
                    string fileName = Convert.ToString(user.User_id) + ".jpg";
                    string path = Path.Combine(Server.MapPath("~/Images/UserProfile"), fileName);
                   
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                        Console.WriteLine("Image deleted successfully.");
                    }
                    file.SaveAs(path);

                    var baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority;
                    userEditProfile.UpdateUserProfile(userEditProfile, user, _customerManagementContext, Convert.ToString(fileName), Convert.ToString(baseUrl));
                }
                
               
                
               
                TempData["ChangeProfile"] = "Profile Updated";
                if (user.Role != EnumsforRoles.Admin)
                {
                    return RedirectToAction("CustomerDetail", "User", new { area = "UserCRUD", id = 0 });
                }
                else
                {
                    return RedirectToAction("CustomerList", "CustomerList", new { area = "" });
                }
            }
            else
            {
                return View(userEditProfile);
            }
        }
       
        [Route("User/CustomerDetail")]
        public ActionResult CustomerDetail(long id)
        {
            if (id != 0)
            {
                var Customer = _customerManagementContext.Customers.FirstOrDefault(Customers => Customers.customer_id == id);
                var user = _customerManagementContext.users.FirstOrDefault(Users => Users.customer_id == Customer.customer_id);
                
                if (Customer == null)
                {
                    return RedirectToAction("CustomerList", "CustomerList", new { area = "" });
                }
                else
                {

                    UserDetail userDetail = new UserDetail();
                    var userData = userDetail.GetUserDetail(userDetail, Customer, user);
                    return View(userDetail);
                }
            }
            else
            {
                string userName = User.Identity.Name;
                var user = _customerManagementContext.users.FirstOrDefault(Users => Users.User_name == userName);
                var Customer = _customerManagementContext.Customers.FirstOrDefault(Customers => Customers.customer_id == user.customer_id);
                if (Customer == null)
                {
                    return RedirectToAction("Login", "Account", new { area = "" });
                }
                else
                {

                    UserDetail userDetail = new UserDetail();
                    var userData = userDetail.GetUserDetail(userDetail, Customer, user);
                    return View(userDetail);
                }
            }
            
            
            
        }
        [Authorize(Roles ="Admin")]
        [Route("User/CustomerDetail/{id:long}")]
        public ActionResult CustomerDetails(long id)
        {
           
                return RedirectToAction("CustomerDetail",new {id=id});
           
            
        }
        [HttpPost]
        public void changeUserProfile(int id)
        {
            var file = Request.Files[0];
            string userName = User.Identity.Name;
            var user = _customerManagementContext.users.FirstOrDefault(Users => Users.User_name == userName);
            if (file != null)
            {
                string fileName = Convert.ToString(user.User_id) + ".jpg";
                string path = Path.Combine(Server.MapPath("~/Images/UserProfile"), fileName);

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                    
                }
                
                file.SaveAs(path);
                var customer = _customerManagementContext.Customers.FirstOrDefault(Customers => Customers.customer_id == user.customer_id);
                var baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority;             
                user.ImageUrl = baseUrl + "/Images/UserProfile/" + fileName;
                _customerManagementContext.SaveChanges();
                
            }

            
        }
    }
}