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
using System.Net;
using System.Text.Encodings.Web;

namespace BusinessManagementSystem.Controllers
{
    [Authorize]
    public class AdvancePaymentController : BaseController
    {
        public ResponseDto<AdvancePayment> _responseDto;
        private ILogger<AdvancePaymentController> _logger;
        private readonly ModalView _modalView;
        public AdvancePaymentController(IBusinessLayer businessLayer, INotyfService notyf, IEmailSender emailSender, ILogger<AdvancePaymentController> logger, JavaScriptEncoder javaScriptEncoder) : base(businessLayer, notyf, emailSender, javaScriptEncoder)
        {
            _responseDto = new ResponseDto<AdvancePayment>();
            _modalView = new ModalView("Delete Confirmation !", "Delete", "Are you sure to delete the selected Advance Appointment?", "");
            _logger = logger;

        }
        public IActionResult AllAdvancePayment()
        {
            RequestDto requestDto = _businessLayer.BaseService.GetInitialRequestDtoFilter();
            requestDto.ParameterFilter = "Statement";
            ViewBag.ModalInformation = _modalView;
            ViewBag.AppointmentStatus = new SelectList(SD.PaidStatus, "Key", "Value");
            return View(requestDto);
        }
        public IActionResult MyAdvancePayment()
        {
            RequestDto requestDto = _businessLayer.BaseService.GetInitialRequestDtoFilter();
            requestDto.ParameterFilter = "Statement";
            ViewBag.ModalInformation = _modalView;
            ViewBag.AppointmentStatus = new SelectList(SD.PaidStatus, "Key", "Value");
            return View(requestDto);
        }

        [HttpGet]
        public IActionResult Create()
        {
            AdvancePaymentViewBagList();
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "employee_tattoo,admin_tattoo")]
        public IActionResult Create(AdvancePayment advancePayment)
        {
            AdvancePaymentViewBagList();
            ModelState.Remove(nameof(advancePayment.PaymentMethod));

            if (ModelState.IsValid)
            {
                _responseDto=_businessLayer.AdvancePaymentService.CreateAdvancePayment(advancePayment);
                if (_responseDto.StatusCode == HttpStatusCode.OK)
                {
                    _notyf.Success("Success");
                    return RedirectToAction(nameof(MyAdvancePayment));
                }
                else
                {
                    _notyf.Error(_responseDto.Message);
                    return View(advancePayment);
                }
            }
            else
            {
                IEnumerable<ModelError> errors = ModelState.Values.SelectMany(v => v.Errors).ToList();
                foreach (var error in errors)
                {
                    _notyf.Error(error.ErrorMessage);
                }
                return View(advancePayment);
            }
        }

        [HttpGet]
        public IActionResult Edit(Guid guid)
        {
            if (guid == Guid.Empty) return NotFound();
            AdvancePaymentViewBagList();
            _responseDto = _businessLayer.AdvancePaymentService.GetAdvancePayment(guid);
            if (_responseDto.StatusCode != HttpStatusCode.OK)
            {
                return NotFound();
            }
            else
                return View(_responseDto.Data);
        }

        [HttpPost]
        public IActionResult Edit(AdvancePayment advancePayment)
        {
            if (advancePayment == null) return NotFound();
            AdvancePaymentViewBagList();
            if (ModelState.IsValid)
            {
                _responseDto = _businessLayer.AdvancePaymentService.UpdateAdvancePayment(advancePayment);
                if (_responseDto.StatusCode == HttpStatusCode.OK)
                {
                    _notyf.Success(_responseDto.Message);
                    if (roleName == SD.Role_Superadmin)
                        return RedirectToAction(nameof(AllAdvancePayment));
                    else
                        return RedirectToAction(nameof(MyAdvancePayment));

                }
                else
                {
                    _notyf.Warning(_responseDto.Message);
                    return RedirectToAction("Edit", new { guid = _responseDto.Data.guid });
                }
            }
            else
            {
                IEnumerable<ModelError> errors = ModelState.Values.SelectMany(v => v.Errors).ToList();
                foreach (var error in errors)
                {
                    _notyf.Error(error.ErrorMessage);
                }
                //return RedirectToAction("Edit", new { guid = appointmentDto.guid });
                return View(advancePayment);
            }    
        }

        #region API

        [HttpPost]
        public IActionResult GetAllAdvancePayment([FromBody] RequestDto requestDto)
        {
            _responseDto = _businessLayer.AdvancePaymentService.GetAllAdvancePayment(requestDto);
            if (_responseDto.StatusCode == HttpStatusCode.OK || _responseDto.StatusCode == HttpStatusCode.NotFound) return Ok(_responseDto.Datas);
            else return BadRequest(_responseDto);
        }

        [HttpPost]
        public IActionResult GetMyAdvancePayment([FromBody] RequestDto requestDto)
        {
            _responseDto = _businessLayer.AdvancePaymentService.GetMyAdvancePayment(requestDto, userId);
            if (_responseDto.StatusCode == HttpStatusCode.OK || _responseDto.StatusCode == HttpStatusCode.NotFound) return Ok(_responseDto.Datas);
            else return BadRequest(_responseDto);
        }
        #endregion
    }
}
