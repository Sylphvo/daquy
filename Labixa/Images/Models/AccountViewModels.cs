using Microsoft.AspNet.Identity;
using Microsoft.VisualBasic;
using Outsourcing.Data.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace Labixa.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }
    }

   

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
    public class LoginUserViewModel
    {
        [Required(ErrorMessage = "Account name compulsory!")]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password compulsory!")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        //[Display(Name = "Remember me?")]
        //public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        //[Required(ErrorMessage = "Họ tên là bắt buộc !")]
        //[Display(Name = "Name")]
        //public string Email { get; set; }

        [Required(ErrorMessage = "Address mail compulsory !")]
        [EmailAddress(ErrorMessage ="Address email incorrect format !")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Affilate ID compulsory !")]
        [Display(Name = "Affilate_ID")]
        public string Affilate_ID { get; set; }
        public int ProductId { get; set; }
        public IEnumerable<Product> Products { get; set; }

        //[Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        //[Display(Name = "Phone number")]
        //[DataType(DataType.PhoneNumber)]
        //[RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Điện thoại không đúng định dạng ")]
        //public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Account username compulsory !")]
        [Display(Name = "User name")]
        [RegularExpression(@"^[0-9a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Special characters are not allowed.")]
        public string UserName { get; set; } 

        //[Required(ErrorMessage = "Nick Name là bắt buộc !")]
        //[Display(Name = "Nick Name")]
        //public string NickName { get; set; }

        [Required(ErrorMessage = "Password compulsory !")]
        [StringLength(100, ErrorMessage = "Password weak !", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Confirm password don't match !")]
        public string ConfirmPassword { get; set; }
        //[DataType(DataType.Text)]
        //[Display(Name = "First Name")]
        //[Compare("FirstName", ErrorMessage = "")]
        //public string FirstName { get; set; }

        //[DataType(DataType.Text)]
        //[Display(Name = "Last Name")]
        //[Compare("LastName", ErrorMessage = "")]
        //public string LastName { get; set; }
        [Required(ErrorMessage = "Vui lòng chấp nhận chính sách của chúng tôi !")]
        [DataType(DataType.CreditCard)]

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
        //[Required(ErrorMessage = "Vui lòng nhập ETH Address*")]
        //[DataType(DataType.Text)]
        //[Display(Name = "ETH Address")]
        //[Compare("ETH_Address", ErrorMessage = "Vui lòng nhập ETH Address*")]

        public string ETH_Address { get; set; }    }
    public class UserEditViewModel
    {
        [Key]
        public string Id { get; set; }
        [Required(ErrorMessage = "Họ tên là bắt buộc !")]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email là bắt buộc !")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không đúng định dạng !")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Vui lòng lựa chọn khóa học!")]
        public int ProductId { get; set; }
        public IEnumerable<Product> Products { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [Display(Name = "Phone number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Điện thoại không đúng định dạng ")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Tên tài khoản là bắt buộc !")]
        [Display(Name = "User name")]
        public string UserName { get; set; }

    }
    public class InputEmailViewModel
    {
        [Required(ErrorMessage = "Email required")]
        [EmailAddress(ErrorMessage = "Email incorrect format")]
        [Display(Name = "EmailReset")]
        public string EmailReset { get; set; }

    }
    public class ContactView
    {
        [Required(ErrorMessage = "Address mail compulsory !")]
        [EmailAddress(ErrorMessage = "Address email incorrect format !")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
        [Required(ErrorMessage = "Please select file.")]
        [Display(Name = "Browse File")]
        public string files { get; set; }

    }
    
}
