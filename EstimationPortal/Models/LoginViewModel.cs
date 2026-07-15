using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EstimationPortal.Models
{
    public class LoginViewModel
    {

        [Required]
        [Display(Name = "Username")]
        public string UserID { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        public string FinancialYear { get; set; }

    }
    public class LoginViewModelWithOTP
    {
        [Required]
        public string UserEmailId { get; set; }
        public string Uip { get; set; }
    }
    public class VSignupViewModel
    {
        public string Name { get; set; }
        public string EmailId1 { get; set; }
        public string NameContact { get; set; }
        public string MobileNo { get; set; }
    }
    public class VerifyOTP
    {
        public int first { get; set; }
        public int second { get; set; }
        public int third { get; set; }
        public int fourth { get; set; }
        public int fifth { get; set; }
        public int sixth { get; set; }
        public int vrfyOTPNumbr { get; set; }
    }
    public class ResetPasswordViewModel
    {

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character:")]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character:")]
        [Display(Name = "ReType New password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

    }
    public class ResetEmailPasswordViewModel
    {
        public string UserEmailId { get; set; }
    }
     

    public class ChangePasswordViewModel
    {

        [Required(ErrorMessage = "EmailId Required")]
        [Display(Name = "EmailId")]
        public string UserEID { get; set; }
        
        [Required]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character:")]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "ReType New password")]
        [Compare("Password", ErrorMessage = "Password and Confirmation password not match.")]
        public string ConfirmPassword { get; set; }

    }
}