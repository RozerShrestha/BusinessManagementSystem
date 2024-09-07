using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessManagementSystem.Dto
{
    public class UserDto
    {
        [ValidateNever]
        public int UserId { get; set; }
        [ValidateNever]
        public string UserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        [Display(Name = "Date Of Birth")]
        public string DateOfBirth { get; set; }
        [Display(Name = "Mobile Number")]
        [RegularExpression("^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string MobileNumber { get; set; }
        public string Gender { get; set; }
        public string Occupation { get; set; }
        public int  RoleId { get; set; }
        [ValidateNever]
        public string RoleName { get; set; }

    }
}
