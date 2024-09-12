 using AspNetCoreHero.ToastNotification.Abstractions;
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

        public BaseController(IUnitOfWork unitOfWork, INotyfService notyf, ILogger<T> logger, JavaScriptEncoder javaScriptEncoder)
        {
            _notyf = notyf;
            _unitOfWork = unitOfWork;
            this.userDto = new UserDto();
            _logger = logger;
            _javaScriptEncoder = javaScriptEncoder;
        }
        public override void OnActionExecuting(ActionExecutingContext ctx)
        {
            ViewData["UserDetail"] = UserDetail();
            //ViewData["Menu"] = MenuList();
            ViewData["Title"] = "FreakStreet Empire";

        }
        public UserDto UserDetail()
        {
            try
            {
                var claims = User.Identities.First().Claims;
                _logger.LogInformation(claims.ToList().ToString());
                string claimString = "";
                foreach (var claim in claims)
                {
                    claimString += $"TYPE: {claim.Type}, VALUE {claim.Value} ## ";
                }
                _logger.LogWarning($"Claims: {claimString}");
                var loggedInEmail = claims.FirstOrDefault(x => x.Type.Contains("emailaddress", StringComparison.OrdinalIgnoreCase)).Value;
                var loggedInUserName = loggedInEmail.Split("@")[0].Trim();


                userDto =_unitOfWork.Base.UserDetail(loggedInUserName);
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
      
    }
}
