using AspNetCore;
using AspNetCoreHero.ToastNotification.Abstractions;
using BusinessManagementSystem.BusinessLayer.Services;
using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Models;
using BusinessManagementSystem.Services;
using BusinessManagementSystem.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Net;
using System.Text.Encodings.Web;

namespace BusinessManagementSystem.Controllers
{
    [Authorize]
    public class UsersController : BaseController
    {
        public ResponseDto<User> _responseDto;
        public ResponseDto<UserDto> _responseUserDto;
        public ResponseDto<UserRoleDto> _responseUserRoleDto;
        private ILogger<UsersController> _logger;
        private readonly ModalView _modalView;
        private readonly dynamic roleList;
        public UsersController(IBusinessLayer businessLayer, INotyfService notyf, IEmailSender emailSender, ILogger<UsersController> logger, JavaScriptEncoder javaScriptEncoder) : base(businessLayer, notyf, emailSender, javaScriptEncoder)
        {
            _responseDto = new ResponseDto<User>();
            _responseUserDto = new ResponseDto<UserDto>();

            _responseUserRoleDto = new ResponseDto<UserRoleDto>();
            _logger = logger;
            roleList = _businessLayer.UserService.RoleList();
            _modalView = new ModalView();
        }
        [HttpGet]
        [Authorize(Roles = "superadmin,admin_tattoo,admin_kaffe,admin_apartment")]
        public IActionResult Index()
        {
            return View(_responseDto);
        }

        [HttpGet]
        public IActionResult Detail(Guid guid)
        {
            if(guid == Guid.Empty)
            {
                return NotFound();
            }
            _responseUserDto = _businessLayer.UserService.GetUserByGuid(guid);
            if (_responseUserDto == null) 
            {
                return NotFound();
            }
            //return View(_responseUserDto.Data);
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["RoleList"] = new SelectList(roleList, "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(UserDto userDto)
        {
            ViewData["RoleList"] = new SelectList(roleList, "Id", "Name");
            if (ModelState.IsValid)
            {
                _responseDto = _businessLayer.UserService.CreateUser(userDto);
                if (_responseDto.StatusCode == HttpStatusCode.OK)
                {
                    return RedirectToAction(nameof(Index));
                } 
                else
                {
                    _notyf.Error(_responseDto.Message);
                    return View(userDto);
                }
            }
            else
            {
                IEnumerable<ModelError> errors = ModelState.Values.SelectMany(v => v.Errors).ToList();
                foreach (var error in errors)
                {
                    _notyf.Error(error.ErrorMessage);
                }
                return View(userDto);
            }
        }

        public IActionResult Edit(Guid guid)
        {
            if (guid==Guid.Empty)
            {
                return NotFound();
            }
            ViewData["RoleList"] = new SelectList(roleList, "Id", "Name");
            var _responseDto = _businessLayer.UserService.GetUserByGuid(guid);
            if (_responseDto == null)
             {
                return NotFound();
            }
            return View(_responseDto.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UserDto userDto)
      {
            ViewData["RoleList"] = new SelectList(roleList, "Id", "Name");
            if (ModelState.IsValid)
            {
                _responseDto=_businessLayer.UserService.UpdateUser(userDto);
                if(_responseDto.StatusCode == HttpStatusCode.OK)
                {
                    _notyf.Success(_responseDto.Message);
                }
                else
                {
                    _notyf.Error(_responseDto.Message);
                    return View(userDto);
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                IEnumerable<ModelError> errors = ModelState.Values.SelectMany(v => v.Errors).ToList();
                foreach (var error in errors)
                {
                    _notyf.Error(error.ErrorMessage);
                }
                return View(_responseDto.Data);
            }
           
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
                    _responseDto=_businessLayer.UserService.DeleteUser(id);
                }
                catch (Exception)
                {
                    //_unitOfWork.Rollback();
                    
                }
            }
            return RedirectToAction(nameof(Index));
        }



        #region API CALLS

        [HttpGet]
        [Authorize(Roles = "superadmin,admin_tattoo,admin_kaffe,admin_apartment")]
        public IActionResult GetAllUser()
        {
            string who = roleName;
            if(who==SD.Role_Superadmin)
                _responseUserRoleDto = _businessLayer.UserService.GetAllUser(SD.Role_Superadmin);
            else
            {
                _responseUserRoleDto = _businessLayer.UserService.GetAllUser(SD.Role_ApartmentAdmin);
            }

            if (_responseUserRoleDto.StatusCode == HttpStatusCode.OK)
            {
                return Ok(_responseUserRoleDto);
            }
            else
            {
                return BadRequest(_responseUserRoleDto);
            }
            
        }

        #endregion
    }
}
