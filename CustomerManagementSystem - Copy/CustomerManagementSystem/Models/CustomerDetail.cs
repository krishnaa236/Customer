using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CustomerManagementSystem.Models
{
    public class CustomerDetail
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [RegularExpression("^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9!@#$%^&*]).{8,20}$", ErrorMessage = "password Should Contain Minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character:")]
        public string Password { get; set; }
    }
}