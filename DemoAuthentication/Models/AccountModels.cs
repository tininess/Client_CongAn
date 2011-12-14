using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace DemoAuthentication.Models
{

    public class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LogOnModel
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

    public class RegisterModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Display(Name = "Địa chỉ Email")]
        [Required(ErrorMessage = "{0} cần có.")]
        [DataType(DataType.EmailAddress)]
        [Email(ErrorMessage = "Địa chỉ email không hợp lệ")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Xác Nhận Password")]
        [Compare("Password", ErrorMessage = "Password không chính xác.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Câu Hỏi Phục Hồi Mật Khẩu")]
        public string question { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Câu Trả Lời")]
        public string answear { get; set; }

        [Required]
        [Display(Name = "Tỉnh / Thành Phố")]
        // public IEnumerable<SelectListItem> Province { get; set; }
        public int SelectedProvinceValue { get; set; }

        [Required]
        [Display(Name = "Quận / Huyện")]
        // public IEnumerable<SelectListItem> Districts { get; set; }
        public int SelectedDistrictValue { get; set; }

        [Required]
        [Display(Name = "Xã / Phường")]
        //public IEnumerable<SelectListItem> DetailProvinces { get; set; }
        public int SelectedDetailProvincesValue { get; set; }

        [Display(Name = "Tên Công Ty / Doanh Nghiệp")]
        public string Companyname { get; set; }

        [Display(Name = "Giấy Phép Kinh Doanh")]
        public string BusinessCode { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 7)]
        [Display(Name = "Điện Thoại Bàn")]
        public string Phone { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required]
        [Display(Name = "Di Động")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 7)]
        public string Mobile { get; set; }

        [Required]
        [Display(Name = "Chứng Minh Nhân Dân")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        public string CMND { get; set; }


        [Required]
        [Display(Name = "Địa chỉ")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        public string Address { get; set; }


        [Required]
        [Display(Name = "Nghề Nghiệp")]
        public string subAddress { get; set; }

        [Required]
        [Display(Name = "Tên Đầy Đủ")]
        public string Name { get; set; }


        [Required]
        [Display(Name = "Tuổi")]
        public int Age { get; set; }
       
    }
    public class UserModel
    {

        [Display(Name = "User name")]
        public string UserName { get; set; }


        [DataType(DataType.EmailAddress)]
        [Display(Name = "Địa chỉ Email")]
        public string Email { get; set; }


        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Xác Nhận Password")]
        [Compare("Password", ErrorMessage = "Password không chính xác.")]
        public string ConfirmPassword { get; set; }


        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Câu Hỏi Phục Hồi Mật Khẩu")]
        public string question { get; set; }


        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Câu Trả Lời")]
        public string answear { get; set; }


        [Display(Name = "Tỉnh / Thành Phố")]
        public int SelectedProvinceValue { get; set; }


        [Display(Name = "Quận / Huyện")]
        public int SelectedDistrictValue { get; set; }


        [Display(Name = "Xã / Phường")]
        public int SelectedDetailProvincesValue { get; set; }

        [Display(Name = "Tên Công Ty / Doanh Nghiệp")]
        public string Companyname { get; set; }

        [Display(Name = "Giấy Phép Kinh Doanh")]
        public string BusinessCode { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 7)]
        [Display(Name = "Điện Thoại Bàn")]
        public string Phone { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Di Động")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 7)]
        public string Mobile { get; set; }


        [Display(Name = "Chứng Minh Nhân Dân")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        public string CMND { get; set; }



        [Display(Name = "Địa chỉ")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        public string Address { get; set; }



        [Display(Name = "Nghề Nghiệp")]
        public string subAddress { get; set; }


        [Display(Name = "Tên Đầy Đủ")]
        public string Name { get; set; }

        [Display(Name = "Tỉnh/Thành Phố")]
        public string Tinh { get; set; }
        [Display(Name = "Quận/Huyện")]
        public string Quan { get; set; }
        [Display(Name = "Phường/Xã")]
        public string Phuong { get; set; }

        [Display(Name = "Tuổi")]
        public int? Age { get; set; }

    }
    public class UserLoginModel
    {
        [Display(Name = "Username")]
        public string UserName { get; set; }


        [DataType(DataType.EmailAddress)]
        [Display(Name = "Địa chỉ Email")]
        public string Email { get; set; }

        [Display(Name = "RoleID")]
        public int? roleID { get; set; }

    }
    public class EmailValidationRule : ModelClientValidationRule
    {
        public EmailValidationRule(string errorMessage)
        {
            ErrorMessage = errorMessage;
            ValidationType = "email";
        }
    }
    public class EmailAttribute : RegularExpressionAttribute, IClientValidatable
    {
        public EmailAttribute()
            : base("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$")
        {
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new EmailValidationRule(ErrorMessage);
        }
    }
}
