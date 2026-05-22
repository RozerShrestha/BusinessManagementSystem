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
        ResponseDto<LoginResponseDto> _responseDto;
        protected readonly INotyfService _notyf;
        protected readonly IEmailSender _emailSender;
        public LoginController(ILogin<LoginResponseDto> iLogin, IEmailSender emailSender, INotyfService notyf) 
        { 
            _iLogin = iLogin;
            _responseDto= new ResponseDto<LoginResponseDto>();
            _notyf = notyf;
            _emailSender = emailSender;
        }
        public IActionResult Index()
        {
            return View();
        }

        //[HttpGet("Login")]
        //public IActionResult Login([FromQuery] string returnUrl)
        //{
        //    var redirectUri = returnUrl is null ? Url.Content("~/") : "/" + returnUrl;

        //    if (User.Identity.IsAuthenticated)
        //    {
        //        return LocalRedirect(redirectUri);
        //    }

        //    return Challenge();
        //}

        


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LoginUser(LoginRequestDto loginRequest)
         {
            ModelState.Remove(nameof(loginRequest.ConfirmPassword)); //just to ignore ConfirmPassword to validate
            ModelState.Remove(nameof(loginRequest.OTP));
            if (ModelState.IsValid)
            {
                _responseDto = _iLogin.Login(loginRequest);
                if (_responseDto.StatusCode == HttpStatusCode.OK)
                {
                    HttpContext.Session.SetString("Token", _responseDto.Data.Token);
                    HttpContext.Response.Cookies.Append("AuthToken", _responseDto.Data.Token, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Strict,
                        Expires = DateTimeOffset.Now.AddDays(90)
                    });
                    //ViewBag.Message = _responseDto.Message;
                    _notyf.Success(_responseDto.Message);
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    ViewBag.LoginResponse = _responseDto;
                    return View("Index", loginRequest);
                }
            }
            else
            {
                var errors = ModelState.Values
                               .SelectMany(v => v.Errors)
                               .Select(e => e.ErrorMessage)
                               .ToList();
                _responseDto.StatusCode = HttpStatusCode.BadRequest;
                _responseDto.Message = string.Join(", ", errors);
                ViewBag.LoginResponse = _responseDto;
                return View("Index", loginRequest);
            }

            
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
               _responseDto = _iLogin.Register_User(userDto);
                if(_responseDto.StatusCode!= HttpStatusCode.OK) 
                {
                    _notyf.Error(_responseDto.Message);
                    ViewBag.RegisterResponse = _responseDto;
                }
                else
                {
                    _notyf.Success(_responseDto.Message);
                    ViewBag.LoginResponse = _responseDto;
                    return View("Index");
                }
                
            }
            return View("Register");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ForgotPassword(LoginRequestDto loginRequestDto)
        {
            if (ModelState.IsValid)
            {
                _responseDto = _iLogin.ForgotPassword(loginRequestDto);
                if (_responseDto.StatusCode != HttpStatusCode.OK)
                {
                    _notyf.Error(_responseDto.Message);
                    
                }
                else
                {
                    _notyf.Success(_responseDto.Message);
                }
            }
            else
            {
                var errors = ModelState.Values
                                               .SelectMany(v => v.Errors)
                                               .Select(e => e.ErrorMessage)
                                               .ToList();
                _responseDto.StatusCode = HttpStatusCode.BadRequest;
                _responseDto.Message = string.Join(", ", errors);
            }
            ViewBag.LoginResponse = _responseDto;
            return View("Index");
        }
        public IActionResult Logout([FromQuery] string returnUrl)
        {
            HttpContext.Session.Remove("Token");
            HttpContext.Response.Cookies.Delete("AuthToken");
            return RedirectToAction("Index");
        }


        #region API
        [HttpPost]
        public IActionResult LoginUserAPI(LoginRequestDto loginRequest)
        {
            ModelState.Remove(nameof(loginRequest.ConfirmPassword)); //just to ignore ConfirmPassword to validate
            ModelState.Remove(nameof(loginRequest.OTP));
            if (ModelState.IsValid)
            {
                _responseDto = _iLogin.Login(loginRequest);
                if (_responseDto.StatusCode == HttpStatusCode.OK)
                {
                    HttpContext.Session.SetString("Token", _responseDto.Data.Token);
                    _notyf.Success(_responseDto.Message);
                    return Ok(_responseDto);
                }
                else
                {
                    ModelState.AddModelError("", _responseDto.Message);
                    return BadRequest(_responseDto);
                }
            }
            else
            {
                return BadRequest(_responseDto);
            }

        }

        [HttpPost]
        public IActionResult GenerateOtp([FromBody] LoginRequestDto loginRequest)
        {
            int otp = 0;
            if (!string.IsNullOrEmpty(loginRequest.Username))
            {
                otp = _iLogin.GenerateOtp(loginRequest).Result; // Await the Task<int> result
                if (otp != 0)
                {
                    //send otp through email here
                    string emailOtp = _emailSender.PrepareEmailForOtp(loginRequest.Username, $"Dear {{fullname}}, your OTP to change password is: {otp}");
                    _emailSender.SendEmailAsync(email: loginRequest.Username, subject: "OTP to Change Password", emailOtp);
                    _responseDto.StatusCode = HttpStatusCode.OK;
                    _responseDto.Message = "OTP sent to email, please check";
                    return Ok(_responseDto);
                }
                else
                {
                    _responseDto.StatusCode = HttpStatusCode.NotFound;
                    _responseDto.Message = "Could not Generate OTP";
                    return BadRequest(_responseDto);
                }
            }
            else
            {
                _responseDto.StatusCode = HttpStatusCode.BadRequest;
                _responseDto.Message = "UserName or Email is Empty";
                return BadRequest(_responseDto);
            }
        }

        [HttpPost]
        public IActionResult GetToken([FromBody] LoginRequestDto loginRequest)
        {
            ModelState.Remove(nameof(loginRequest.ConfirmPassword)); //just to ignore ConfirmPassword to validate
            ModelState.Remove(nameof(loginRequest.OTP));
            if (ModelState.IsValid)
            {
                _responseDto = _iLogin.Login(loginRequest);
                return Ok(_responseDto);
            }
            else
            {
                _responseDto.Message = "Invalid Username or password";
                return BadRequest(_responseDto);
            }
        }

        #endregion
    }
}
