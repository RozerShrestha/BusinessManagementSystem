 using AspNetCoreHero.ToastNotification.Abstractions;
using BusinessManagementSystem.BusinessLayer.Services;
using BusinessManagementSystem.Data;
using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Models;
using BusinessManagementSystem.Services;
using BusinessManagementSystem.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using System.Numerics;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace BusinessManagementSystem.Controllers
{
    [Authorize]
    public class BaseController:Controller
    {
        protected readonly IBusinessLayer _businessLayer;
        protected readonly INotyfService _notyf;
        protected readonly IEmailSender _emailSender;
        protected int roleId;
        protected int userId;
        protected Guid userGuid;
        protected string roleName = string.Empty;
        protected string username = string.Empty;
        protected string email = string.Empty;
        protected string fullName = string.Empty;
        protected string PhoneNumber = string.Empty;
        protected UserDto userDto;
        JavaScriptEncoder _javaScriptEncoder;

        
        public BaseController(IBusinessLayer businessLayer, INotyfService notyf, IEmailSender emailSender, JavaScriptEncoder javaScriptEncoder)
        {
            _notyf = notyf;
            _emailSender= emailSender;
            _businessLayer = businessLayer;
            this.userDto = new UserDto();
            _javaScriptEncoder = javaScriptEncoder;
        }
        public override void OnActionExecuting(ActionExecutingContext ctx)
        {
            ViewData["UserDetail"] = UserDetail();
            ViewData["Menu"] = MenuList();
            ViewData["Title"] = _businessLayer.BasicConfigurationService.GetBasicConfig().Result.Data.ApplicationTitle;
        }
         protected IActionResult HandleError(Exception ex)
        {
        return StatusCode(500, new {message= ex.Message });
        }
        private UserDto UserDetail()
        {
            try
            {
                var claims = User.Identities.First().Claims;
                var loggedInEmail = claims.FirstOrDefault(x => x.Type.Contains("emailaddress", StringComparison.OrdinalIgnoreCase)).Value;
                var loggedInUserName = loggedInEmail.Split("@")[0].Trim();

                userDto = _businessLayer.BaseService.UserDetail(loggedInEmail);
                userId = userDto.UserId;
                username = userDto.UserName;
                email = userDto.Email;
                PhoneNumber = userDto.PhoneNumber;
                roleId = userDto.RoleId;
                roleName = userDto.RoleName;
                fullName = userDto.FullName;
                userGuid = userDto.userGuid;
                
            }
            catch (Exception ex)
            {
               
            }
            return userDto;
        }
        private List<MenuDto> MenuList()
        {
            var menuFilter = _businessLayer.BaseService.MenuList(roleName);
            return menuFilter;
        }
        protected bool IsAdmin => roleName == SD.Role_Superadmin || roleName == SD.Role_TattooAdmin;
        protected bool IsSuperAdmin => roleName == SD.Role_Superadmin;
        protected bool isAuthorized(int _userId)
        {
            if ((roleName == "admin" || roleName == "hradmin") || userId == _userId)
                return true;
            else
                return false;
        }
        protected string EncodedString(string text)
        {
            return _javaScriptEncoder.Encode(text);
        }
    }
}
