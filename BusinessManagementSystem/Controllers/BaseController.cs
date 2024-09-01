using AspNetCoreHero.ToastNotification.Abstractions;
using BusinessManagementSystem.Data;
using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Encodings.Web;

namespace BusinessManagementSystem.Controllers
{
    public class BaseController<T> : Controller where T : BaseController<T>
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly ILogger<T> _logger;
        protected readonly ApplicationDBContext _dbContext;
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

        public BaseController(IUnitOfWork unitOfWork, UserDto userDto, INotyfService notyf, ILogger<T> logger, JavaScriptEncoder javaScriptEncoder)
        {
            _notyf = notyf;
            _unitOfWork = unitOfWork;
            this.userDto = userDto;
            _logger = logger;
            _javaScriptEncoder = javaScriptEncoder;
        }

        public override void OnActionExecuting(ActionExecutingContext ctx)
        {
            //ViewData["UserDetail"] = UserDetail();
            //ViewData["Menu"] = MenuList();
            //ViewData["Title"] = _unitOfWork.BasicConfiguration.GetSingleOrDefault().Data.ApplicationTitle;

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
