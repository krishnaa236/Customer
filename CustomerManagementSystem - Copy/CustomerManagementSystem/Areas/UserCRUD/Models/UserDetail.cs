using CustomerManagementSystem.Domain;
using CustomerManagementSystem.Domain.Domain;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomerManagementSystem.Areas.CustomerCRUD.Models
{
    public class UserDetail
    {
        public long customer_id { get; set; }
        [Required(ErrorMessage = "Please enter a Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter a SurName")]
        public string LastName { get; set; }
        [Required]
        [RegularExpression("^\\d{10}$",ErrorMessage ="Please Enter a valid phone Number")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Email Id is Required")]
        [RegularExpression(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$",
                            ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [RegularExpression("^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9!@#$%^&*]).{8,20}$", ErrorMessage = "password Should Contain Minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character:")]
        public string password { get; set; }
        [Required]
        public string Country { get; set; }
        public bool Status { get; set; }
        public string customer_address { get; set; }
        public string Role { get; set; }
        public string ImageUrl { get; set; }


        public long AddCustomer(UserDetail userDetail,CustomerManagementContext _customerManagementContext,Customer customer)
        {
            customer.customer_firstname = userDetail.FirstName;
            customer.customer_lastname = userDetail.LastName;
            customer.customer_email = userDetail.Email;
            customer.customer_phonenumber = userDetail.PhoneNumber;
            customer.customer_city = userDetail.City;
            customer.customer_country = userDetail.Country;
            customer.created_at = DateTime.Now;
            _customerManagementContext.Customers.Add(customer);
            _customerManagementContext.SaveChanges();
            return customer.customer_id;
        }
        public long AddAdmin(UserDetail userDetail, CustomerManagementContext _customerManagementContext, Admin admin)
        {
            admin.Admin_FirstName = userDetail.FirstName;
            admin.Admin_LastName = userDetail.LastName;
            admin.Admin_Email = userDetail.Email;
            admin.Admin_PhoneNumber = userDetail.PhoneNumber;
            admin.Admin_city = userDetail.City;
            admin.Admin_country = userDetail.Country;
            admin.created_at = DateTime.Now;
            _customerManagementContext.Admins.Add(admin);
            _customerManagementContext.SaveChanges();
            return admin.Admin_id;
        }
        public void AddUser(UserDetail userDetail, CustomerManagementContext _customerManagementContext, user user, long customer_id, long Admin_id)
        {       
                if (userDetail.Role != EnumsforRoles.Admin)
                {
                    user.customer_id = customer_id;
                }
                else
                {
                    user.Admin_id = Admin_id;
                }         
           
            user.User_name = userDetail.UserName;
            user.password = userDetail.password;
            user.status = userDetail.Status;
            user.created_at = DateTime.Now;
            user.Role = userDetail.Role;
            _customerManagementContext.users.Add(user);
            _customerManagementContext.SaveChanges();
        }



        public UserDetail GetCustomerData(UserDetail userDetail, long id, CustomerManagementContext _customerManagementContext)
        {
            var Customer = _customerManagementContext.Customers.FirstOrDefault(Customers => Customers.customer_id == id);
            var user = _customerManagementContext.users.FirstOrDefault(Users => Users.customer_id == id);
            var customerData = GetUserDetail(userDetail,Customer,user);
            return customerData;
        }
        public UserDetail GetUserDetail(UserDetail userDetail,Customer Customer, user user)
        {
            userDetail.customer_id = Customer.customer_id;
            userDetail.FirstName = Customer.customer_firstname;
            userDetail.LastName = Customer.customer_lastname;
            userDetail.PhoneNumber = Customer.customer_phonenumber;
            userDetail.Email = Customer.customer_email;
            userDetail.City = Customer.customer_city;
            userDetail.Country = Customer.customer_country;
            userDetail.UserName = user.User_name;
            userDetail.Status = user.status;
            userDetail.ImageUrl = user.ImageUrl;
            return userDetail;
        }
        public void UpdateCustomer(UserDetail userDetail, CustomerManagementContext _customerManagementContext, Customer customer)
        {
            customer.customer_firstname = userDetail.FirstName;
            customer.customer_lastname = userDetail.LastName;
            customer.customer_email = userDetail.Email;
            customer.customer_phonenumber = userDetail.PhoneNumber;
            customer.customer_city = userDetail.City;
            customer.customer_country = userDetail.Country;
            customer.updated_at = DateTime.Now;
            _customerManagementContext.SaveChanges();
        }

        public void  UpdateUser(UserDetail userDetail, CustomerManagementContext _customerManagementContext, user user,long CurrentUserId)
        {
            user.User_name = userDetail.UserName;
            user.password = userDetail.password;
            user.status = userDetail.Status;
            user.updated_at = DateTime.Now;
            user.Updated_by = CurrentUserId;
            _customerManagementContext.SaveChanges();
        }
    }
}