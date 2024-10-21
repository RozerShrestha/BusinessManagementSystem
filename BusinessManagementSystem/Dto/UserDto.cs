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
        //[ValidateNever]
        public int UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        public string? Password { get; set; }
        [Display(Name = "Confirm Password")]
        public string? ConfirmPassword { get; set; }


        [Required]
        public string FullName { get; set; }
        [Required]
        public string Address { get; set; }
        [Display(Name = "Date Of Birth")]
        [Required]
        public DateOnly DateOfBirth { get; set; }
        [Display(Name = "Mobile Number")]
        [RegularExpression("^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Occupation { get; set; }
        [Required]
        public bool Status { get; set; }
        [Required]
        public int  RoleId { get; set; }
        [ValidateNever]
        public string RoleName { get; set; }

    }
}
