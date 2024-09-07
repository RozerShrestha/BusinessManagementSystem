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
                    _unitOfWork.CreateTransaction();
                    await _unitOfWork.Users.InsertAsync(user);
                    await _unitOfWork.Save();
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
                    //Begin The Tranaction
                    _unitOfWork.CreateTransaction();
                    //Use Generic Reposiory to Insert a new employee
                    await _unitOfWork.Users.UpdateAsync(user);
                    //Save Changes to database
                    await _unitOfWork.Save();
                    //Commit the Changes to database
                    _unitOfWork.Commit();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    //Rollback Transaction
                    _unitOfWork.Rollback();
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
            //Use Employee Repository to Fetch Employees along with the Department Data by Employee Id
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
            //Begin The Tranaction
            _unitOfWork.CreateTransaction();
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user.Data != null)
            {
                try
                {
                    await _unitOfWork.Users.DeleteAsync(user.Data);
                    //Save Changes to database
                    await _unitOfWork.Save();
                    //Commit the Changes to database
                    _unitOfWork.Commit();
                }
                catch (Exception)
                {
                    //Rollback Transaction
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
