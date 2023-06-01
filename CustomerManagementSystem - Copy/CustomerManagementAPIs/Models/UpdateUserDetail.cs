using CustomerManagementSystem.Domain;
using CustomerManagementSystem.Domain.Domain;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomerManagementAPIs.Models
{
    public class UpdateUserDetail
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
       
        public string ImageUrl { get; set; }


        



        public UpdateUserDetail GetCustomerData(UpdateUserDetail userDetail, long id, CustomerManagementContext _customerManagementContext)
        {
            var Customer = _customerManagementContext.Customers.FirstOrDefault(Customers => Customers.customer_id == id);
            var user = _customerManagementContext.users.FirstOrDefault(Users => Users.customer_id == id);
            var customerData = GetUserDetail(userDetail,Customer,user);
            return customerData;
        }
        public UpdateUserDetail GetUserDetail(UpdateUserDetail userDetail,Customer Customer, user user)
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
        public void UpdateCustomer(UpdateUserDetail userDetail, CustomerManagementContext _customerManagementContext, Customer customer)
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

        public void  UpdateUser(UpdateUserDetail userDetail, CustomerManagementContext _customerManagementContext, user user,long CurrentUserId)
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