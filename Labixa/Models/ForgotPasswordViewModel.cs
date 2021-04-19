using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Labixa.Models
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Mật khẩu là bắt buộc !")]
        [StringLength(100, ErrorMessage = "Mật khẩu chưa đủ bảo mật !", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [CompareAttribute("Password", ErrorMessage = "Nhập lại mật khẩu không trùng khớp !")]
        public string ConfirmPassword { get; set; }
    }
}