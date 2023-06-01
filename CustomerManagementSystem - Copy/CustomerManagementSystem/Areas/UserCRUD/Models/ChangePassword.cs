using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CustomerManagementSystem.Areas.CustomerCRUD.Models
{
    public class ChangePassword
    {
        [Required]
        [RegularExpression("^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9!@#$%^&*]).{8,20}$", ErrorMessage = "password Should Contain Minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character:")]
        public string OldPassword { get; set; }

        [Required]
        [RegularExpression("^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9!@#$%^&*]).{8,20}$", ErrorMessage = "password Should Contain Minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character:")]
        public string NewPassword { get; set; }

        [Required]
        [RegularExpression("^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9!@#$%^&*]).{8,20}$", ErrorMessage = "password Should Contain Minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character:")]
        public string ConfirmPassword { get; set; }
    }

    
}