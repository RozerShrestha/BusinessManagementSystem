using AspNetCoreHero.ToastNotification.Abstractions;
using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Models;
using BusinessManagementSystem.Services;
using BusinessManagementSystem.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BusinessManagementSystem.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        ILogin<LoginResponseDto> _iLogin;
        ResponseDto<LoginResponseDto> _ResponseDto;
        protected readonly INotyfService _notyf;
        public LoginController(ILogin<LoginResponseDto> iLogin, INotyfService notyf) 
        { 
            _iLogin = iLogin;
            _ResponseDto= new ResponseDto<LoginResponseDto>();
            _notyf = notyf;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LoginUser(LoginRequestDto loginRequest)
        {
            if (ModelState.IsValid)
            {
                _ResponseDto = _iLogin.Login(loginRequest);
                if (_ResponseDto.StatusCode == HttpStatusCode.OK)
                {
                    HttpContext.Session.SetString("Token", _ResponseDto.Data.Token);
                    ViewBag.Message = _ResponseDto.Message;
                    _notyf.Success(_ResponseDto.Message);
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    ModelState.AddModelError("", _ResponseDto.Message);
                }
                ViewBag.LoginResponse = _ResponseDto;
            }
            
            return View("Index",loginRequest); ;
        }

        [HttpGet("Login")]
		public IActionResult Login([FromQuery] string returnUrl)
		{
			var redirectUri = returnUrl is null ? Url.Content("~/") : "/" + returnUrl;

			if (User.Identity.IsAuthenticated)
			{
				return LocalRedirect(redirectUri);
			}

			return Challenge();
		}

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegisterUser(UserDto userDto)
        {
            if (ModelState.IsValid)
            {
                bool passwordMatch = userDto.Password == userDto.ConfirmPassword ? true : false;
               _ResponseDto = _iLogin.Register_User(userDto);
                if(_ResponseDto.StatusCode!= HttpStatusCode.OK) 
                {
                    _notyf.Error(_ResponseDto.Message);
                    ViewBag.RegisterResponse = _ResponseDto;
                }
                else
                {
                    _notyf.Success(_ResponseDto.Message);
                    ViewBag.LoginResponse = _ResponseDto;
                    return View("Index");
                }
                
            }
            return View("Register");
        }

        public IActionResult Logout([FromQuery] string returnUrl)
        {
            //HttpContext.Session.Remove("Token");
            //return RedirectToAction("Index");
            var redirectUri = returnUrl is null ? Url.Content("~/") : "/" + returnUrl;
           
            if (!User.Identity.IsAuthenticated)
            {
                return LocalRedirect(redirectUri);
            }

            HttpContext.SignOutAsync();

            //return LocalRedirect(redirectUri);
            return View();
        }
    }
}
