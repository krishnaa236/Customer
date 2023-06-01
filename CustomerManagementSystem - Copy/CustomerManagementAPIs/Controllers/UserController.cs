
using CustomerManagementAPIs.Models;
using CustomerManagementSystem.Domain;
using CustomerManagementSystem.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using HttpPutAttribute = System.Web.Http.HttpPutAttribute;

namespace CustomerManagementAPIs.Areas.UserCRUD.Controllers
{
    public class UserController : System.Web.Http.ApiController
    {

        private readonly CustomerManagementContext _customerManagementContext;

        // GET: CustomerCRUD/Customer

        public UserController()
        {
            _customerManagementContext = new CustomerManagementContext();
        }
        [HttpPost]
        public string AddUser(AddUserDetail userDetail)
        {
            Customer customer = new Customer();
            Admin admin = new Admin();
            if (!ModelState.IsValid)
            {
                return error(ModelState);
            }
            else
            {
                if (userDetail.Role != EnumsforRoles.Admin)
                {
                    customer.customer_id = userDetail.AddCustomer(userDetail, _customerManagementContext, customer);
                }
                else
                {
                    admin.Admin_id = userDetail.AddAdmin(userDetail, _customerManagementContext, admin);
                }
                user user = new user();
                userDetail.AddUser(userDetail, _customerManagementContext, user, customer.customer_id, admin.Admin_id);
                return HttpResponse("User created successfully");
            }

        }
        [HttpGet]
        public UpdateUserDetail GetCustomerDetail(long id)
        {
            UpdateUserDetail userDetail = new UpdateUserDetail();
            var Customer = _customerManagementContext.Customers.FirstOrDefault(Customers => Customers.customer_id == id);
            var user = _customerManagementContext.users.FirstOrDefault(Users => Users.customer_id == id);
            if (Customer == null)
            {
                return userDetail;
            }
            else
            {

                
                var UserDetails = userDetail.GetCustomerData(userDetail, id, _customerManagementContext);
                return userDetail;
            }

        }
        [HttpPost]
        public UpdateUserDetail UpdateCustomer(long id)
        {
            UpdateUserDetail userDetail = new UpdateUserDetail();
            var Customer = _customerManagementContext.Customers.FirstOrDefault(Customers => Customers.customer_id == id);
            var user = _customerManagementContext.users.FirstOrDefault(Users => Users.customer_id == id);
            if (Customer == null)
            {
                return userDetail;
            }
            else
            {


                var UserDetails = userDetail.GetCustomerData(userDetail, id, _customerManagementContext);
                return userDetail;
            }
            if (ModelState.IsValid)
            {
                var CurrentUser = User.Identity.Name;
                var CurrenUserRole = _customerManagementContext.users.FirstOrDefault(Users => Users.User_name == CurrentUser);
                long CurrenUserID = CurrenUserRole.User_id;
                
                userDetail.UpdateCustomer(userDetail, _customerManagementContext, Customer);
                userDetail.UpdateUser(userDetail, _customerManagementContext, user, CurrenUserID);
                return userDetail;
              
            }
            
        

        }
        private string HttpResponse(string v)
        {
            return "User Created Successfully";
        }

        private string error(System.Web.Http.ModelBinding.ModelStateDictionary modelState)
        {
            return "Model State not Validated";
        }
    }
}