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
        public async Task<IActionResult> Index()
        {
            var users=await _unitOfWork.Users.GetAllAsync();
            return View(users);
        }

        public async Task<IActionResult> Details(int id)
        {
            if(id == 0)
            {
                return NotFound();
            }
            var user=await _unitOfWork.Users.GetByIdAsync(Convert.ToInt32(id));
            if (user == null) 
            {
                return NotFound();
            }
            return View(user);
        }

        public async Task<IActionResult> Create([Bind("Id,Guid,UserName,Email,FullName,DateOfBirth,Gender,Address,PhoneNumber,RoleId")] User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.BeginTransactionAsync();
                    await _unitOfWork.Users.InsertAsync(user);
                    await _unitOfWork.SaveChangesAsync();
                    _unitOfWork.CommitAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {

                    _unitOfWork.RollbackAsync();
                }
            }
            return View(user);
        }

        public async Task<IActionResult> EditUser(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(int id, [Bind("Id,Guid,UserName,Email,FullName,DateOfBirth,Gender,Address,PhoneNumber,RoleId")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.BeginTransactionAsync();
                    await _unitOfWork.Users.UpdateAsync(user);
                    await _unitOfWork.SaveChangesAsync();
                    _unitOfWork.CommitAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    _unitOfWork.RollbackAsync();
                }
            }
            return View(user);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user.Data != null)
            {
                try
                {
                    _unitOfWork.BeginTransactionAsync();
                    await _unitOfWork.Users.DeleteAsync(user.Data);
                    await _unitOfWork.SaveChangesAsync();
                    _unitOfWork.CommitAsync();
                }
                catch (Exception)
                {
                    _unitOfWork.RollbackAsync();
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
