 using AspNetCoreHero.ToastNotification.Abstractions;
using BusinessManagementSystem.BusinessLayer.Services;
using BusinessManagementSystem.Data;
using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Models;
using BusinessManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Numerics;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace BusinessManagementSystem.Controllers
{
    public class BaseController<T> : Controller where T : BaseController<T>
    {
        protected readonly IUnitOfWork _unitOfWork;
        //protected readonly IBaseService _baseService;
        protected readonly IBusinessLayer _businessLayer;
        protected readonly ILogger<T> _logger;
        protected readonly INotyfService _notyf;
        protected int roleId;
        protected int userId;
        protected string roleName = string.Empty;
        protected string username = string.Empty;
        protected string email = string.Empty;
        protected string fullName = string.Empty;
        protected string mobileNumber = string.Empty;
        protected UserDto userDto;
        JavaScriptEncoder _javaScriptEncoder;

        public BaseController(IUnitOfWork unitOfWork, IBusinessLayer businessLayer, INotyfService notyf, ILogger<T> logger, JavaScriptEncoder javaScriptEncoder)
        {
            _notyf = notyf;
            _unitOfWork = unitOfWork;
            //_baseService = baseService;
            _businessLayer = businessLayer;
            this.userDto = new UserDto();
            _logger = logger;
            _javaScriptEncoder = javaScriptEncoder;
        }
        public override void OnActionExecuting(ActionExecutingContext ctx)
        {
            ViewData["UserDetail"] = UserDetail();
            ViewData["Menu"] = MenuList();
            ViewData["Title"] = "FreakStreet Empire";

        }
        private UserDto UserDetail()
        {
            try
            {
                var claims = User.Identities.First().Claims;
                _logger.LogInformation(claims.ToList().ToString());
                var loggedInEmail = claims.FirstOrDefault(x => x.Type.Contains("emailaddress", StringComparison.OrdinalIgnoreCase)).Value;
                var loggedInUserName = loggedInEmail.Split("@")[0].Trim();


                //userDto =_unitOfWork.Base.UserDetail(loggedInEmail);
                //userDto=_baseService.UserDetail(loggedInEmail);
                userDto = _businessLayer.BaseService.UserDetail(loggedInEmail);
                userId = userDto.UserId;
                username = userDto.UserName;
                email = userDto.Email;
                mobileNumber = userDto.MobileNumber;
                roleId = userDto.RoleId;
                roleName = userDto.RoleName;
                this.fullName = userDto.FullName;
                _logger.LogInformation($"LoggedIn User Information: {username}, {email}, {mobileNumber}, {roleName}");
                
            }
            catch (Exception ex)
            {

            }
            return userDto;
        }
        private List<MenuDto> MenuList()
        {
            //var menuFilter = _unitOfWork.Base.MenuList(roleName);
            var menuFilter = _businessLayer.BaseService.MenuList(roleName);
            return menuFilter;
        }
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
