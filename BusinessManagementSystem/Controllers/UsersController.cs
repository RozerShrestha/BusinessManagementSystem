using AspNetCore;
using AspNetCoreHero.ToastNotification.Abstractions;
using BusinessManagementSystem.BusinessLayer.Services;
using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Models;
using BusinessManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Text.Encodings.Web;

namespace BusinessManagementSystem.Controllers
{
    [Authorize]
    public class UsersController : BaseController
    {
        public ResponseDto<User> _responseDto;
        private ILogger<UsersController> _logger;
        private readonly ModalView _modalView;
        public UsersController(IBusinessLayer businessLayer, INotyfService notyf, IEmailSender emailSender, ILogger<UsersController> logger, JavaScriptEncoder javaScriptEncoder) : base(businessLayer, notyf, emailSender, javaScriptEncoder)
        {
            _responseDto = new ResponseDto<User>();
            _logger = logger;
            _modalView = new ModalView();
        }
        [HttpGet]
        [Authorize(Roles = "superadmin,admin_tattoo,admin_kaffe,admin_apartment")]
        public IActionResult Index()
        {
            _responseDto = _businessLayer.UserService.GetAllUser();
            
            return View(_responseDto);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            if(id == 0)
            {
                return NotFound();
            }
            var _responseDto = _businessLayer.UserService.GetUserById(id);
            if (_responseDto == null) 
            {
                return NotFound();
            }
            return View(_responseDto);
        }
        [HttpPost]
        public IActionResult Create(UserDto user)
        {
            if (ModelState.IsValid)
            {
                _responseDto = _businessLayer.UserService.Create(user);
                return RedirectToAction(nameof(Index));
            }
            return View(_responseDto);
        }

        public IActionResult EditUser(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var _responseDto = _businessLayer.UserService.GetUserById(id);
            if (_responseDto == null)
            {
                return NotFound();
            }
            return View(_responseDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditUser(int id, [Bind("Id,Guid,UserName,Email,FullName,DateOfBirth,Gender,Address,PhoneNumber,RoleId")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _responseDto=_businessLayer.UserService.Update(user);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    //_unitOfWork.Rollback();
                }
            }
            return View(_responseDto);
        }

        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            _responseDto = _businessLayer.UserService.GetUserById(id);
            if (_responseDto == null)
            {
                return NotFound();
            }
            return View(_responseDto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            
            var _responseDto = _businessLayer.UserService.GetUserById(id);
            if (_responseDto.Data != null)
            {
                try
                {
                    _responseDto=_businessLayer.UserService.Delete(id);
                }
                catch (Exception)
                {
                    //_unitOfWork.Rollback();
                    
                }
            }
            return RedirectToAction(nameof(Index));
        }



        #region API CALLS
        //[HttpGet]
        //public IActionResult GetAllUsers()
        //{
            
        //}

        #endregion
    }
}
