using AspNetCoreHero.ToastNotification.Abstractions;
using BusinessManagementSystem.BusinessLayer.Services;
using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Models;
using BusinessManagementSystem.Services;
using BusinessManagementSystem.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Encodings.Web;

namespace BusinessManagementSystem.Controllers
{
    public class AppointmentController : BaseController
    {
        public ResponseDto<Appointment> _responseDto;
        public ResponseDto<UserDto> _responseAppointmentDto;
        private ILogger<AppointmentController> _logger;
        private readonly ModalView _modalView;
        private readonly dynamic roleList;
        //private ModalView _modalView;
        public AppointmentController(IBusinessLayer businessLayer, INotyfService notyf, IEmailSender emailSender, ILogger<AppointmentController> logger, JavaScriptEncoder javaScriptEncoder) : base(businessLayer, notyf, emailSender, javaScriptEncoder)
        {
            roleList = _businessLayer.UserService.RoleList();

            _responseDto = new ResponseDto<Appointment>();
            _responseAppointmentDto = new ResponseDto<UserDto>();
            _modalView = new ModalView("Delete Confirmation !", "Delete", "Are you sure to delete the selected User?", "");
            _logger = logger;

        }

        [HttpGet]
        [Authorize(Roles = "superadmin,admin_tattoo,employee_tattoo")]
        public IActionResult Create()
        {

            return View();
        }

    }
}
