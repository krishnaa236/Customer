using CustomerManagementSystem.Domain;
using CustomerManagementSystem.Domain.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace CustomerManagementSystem.Areas.CustomerCRUD.Models
{
    public class UserEditProfile
    {
        public long customer_id { get; set; }
        [Required(ErrorMessage = "Please enter a Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter a SurName")]
        public string LastName { get; set; }
        [Required]
        [RegularExpression("^\\d{10}$", ErrorMessage = "Please Enter a valid phone Number")]
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
        public string Country { get; set; }
        public bool Status { get; set; }
        public string customer_address { get; set; }
        public string ImageUrl { get; set; }
        public UserEditProfile GetCustomerData(UserEditProfile userEditProfile,Customer Customer,user user)
        {
            userEditProfile.customer_id = Customer.customer_id;
            userEditProfile.FirstName = Customer.customer_firstname;
            userEditProfile.LastName = Customer.customer_lastname;
            userEditProfile.PhoneNumber = Customer.customer_phonenumber;
            userEditProfile.Email = Customer.customer_email;
            userEditProfile.City = Customer.customer_city;
            userEditProfile.Country = Customer.customer_country;
            userEditProfile.UserName = user.User_name;
            userEditProfile.Status = user.status;
            userEditProfile.ImageUrl = user.ImageUrl;
            return userEditProfile;
        }
        public UserEditProfile GetAdminData(UserEditProfile userEditProfile, Admin Admin, user user)
        {
            userEditProfile.customer_id = Admin.Admin_id;
            userEditProfile.FirstName = Admin.Admin_FirstName;
            userEditProfile.LastName = Admin.Admin_LastName;
            userEditProfile.PhoneNumber = Admin.Admin_PhoneNumber;
            userEditProfile.Email = Admin.Admin_Email;
            userEditProfile.City = Admin.Admin_city;
            userEditProfile.Country = Admin.Admin_country;
            userEditProfile.UserName = user.User_name;
            userEditProfile.Status = user.status;
            userEditProfile.ImageUrl = user.ImageUrl;
            return userEditProfile;
        }
        public void UpdateUserProfile(UserEditProfile userEditProfile,user user,CustomerManagementContext _customerManagementContext, string fileName, string baseURL)
        {
            if (user.Role != EnumsforRoles.Admin)
            {
                var Customer = _customerManagementContext.Customers.FirstOrDefault(Customers => Customers.customer_id == user.customer_id);
                Customer.customer_firstname = userEditProfile.FirstName;
                Customer.customer_lastname = userEditProfile.LastName;
                Customer.customer_email = userEditProfile.Email;
                Customer.customer_phonenumber = userEditProfile.PhoneNumber;
                Customer.customer_city = userEditProfile.City;
                Customer.customer_country = userEditProfile.Country;
                Customer.updated_at = DateTime.Now;
                _customerManagementContext.SaveChanges();

            }
            else
            {
                var Admin = _customerManagementContext.Admins.FirstOrDefault(Admins => Admins.Admin_id == user.Admin_id);
                Admin.Admin_FirstName = userEditProfile.FirstName;
                Admin.Admin_LastName = userEditProfile.LastName;
                Admin.Admin_Email = userEditProfile.Email;
                Admin.Admin_PhoneNumber = userEditProfile.PhoneNumber;
                Admin.Admin_city = userEditProfile.City;
                Admin.Admin_country = userEditProfile.Country;
                Admin.created_at = DateTime.Now;

                _customerManagementContext.SaveChanges();
            }
            
            user.User_name = userEditProfile.UserName;
            user.status = userEditProfile.Status;
            user.updated_at = DateTime.Now;
            user.Updated_by = user.User_id;
            if(fileName != "")
            {
                user.ImageUrl = baseURL + "/Images/UserProfile/" + fileName;
            }
            
            _customerManagementContext.SaveChanges();
        }
    }
}