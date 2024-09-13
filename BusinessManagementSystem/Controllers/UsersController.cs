using AspNetCoreHero.ToastNotification.Abstractions;
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
    public class UsersController : BaseController<UsersController>
    {
        public ResponseDto<User> _responseDto;
        ILogin<LoginResponseDto> _iLogin;
        private dynamic insurancePlans;
        private readonly dynamic roleList;
        private readonly IEmailSender _emailSender;
        private readonly ModalView _modalView;
        public UsersController(ILogin<LoginResponseDto> iLogin, IUnitOfWork unitOfWork, INotyfService notyf, IEmailSender emailSender, ILogger<UsersController> logger, JavaScriptEncoder javaScriptEncoder) : base(unitOfWork, notyf, logger, javaScriptEncoder)
        {
            _iLogin = iLogin;
            _responseDto = new ResponseDto<User>();
            roleList = _unitOfWork.Role.GetRoles();
            _emailSender = emailSender;
            _modalView = new ModalView();
        }
        public IActionResult Index()
        {
            var users= _unitOfWork.Users.GetAll();
            return View(users);
        }

        public IActionResult Details(int id)
        {
            if(id == 0)
            {
                return NotFound();
            }
            var user= _unitOfWork.Users.GetById(Convert.ToInt32(id));
            if (user == null) 
            {
                return NotFound();
            }
            return View(user);
        }

        public IActionResult Create([Bind("Id,Guid,UserName,Email,FullName,DateOfBirth,Gender,Address,PhoneNumber,RoleId")] User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.BeginTransaction();
                     _unitOfWork.Users.Insert(user);
                     _unitOfWork.SaveChanges();
                    _unitOfWork.Commit();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {

                    _unitOfWork.Rollback();
                }
            }
            return View(user);
        }

        public IActionResult EditUser(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var user =  _unitOfWork.Users.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
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
                    _unitOfWork.BeginTransaction();
                     _unitOfWork.Users.Update(user);
                     _unitOfWork.SaveChanges();
                    _unitOfWork.Commit();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    _unitOfWork.Rollback();
                }
            }
            return View(user);
        }

        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var user =  _unitOfWork.Users.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            
            var user =  _unitOfWork.Users.GetById(id);
            if (user.Data != null)
            {
                try
                {
                    _unitOfWork.BeginTransaction();
                     _unitOfWork.Users.Delete(user.Data);
                     _unitOfWork.SaveChanges();
                    _unitOfWork.Commit();
                }
                catch (Exception)
                {
                    _unitOfWork.Rollback();
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
