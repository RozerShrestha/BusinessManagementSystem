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
        public IActionResult Create(AdvancePayment advancePayment)
        {
            AdvancePaymentViewBagList();

            if (ModelState.IsValid)
            {
                _responseDto=_businessLayer.AdvancePaymentService.CreateAdvancePayment(advancePayment);
                if (_responseDto.StatusCode == HttpStatusCode.OK)
                {
                    _notyf.Success("Success");
                    #region email
                    var messageSuperAdmin = _businessLayer.BasicConfigurationService.GetBasicConfig().Data.AdvancePaymentSuperadminTemplate;
                    var userSuperadmins = _businessLayer.UserService.GetSuperadminUser().Datas;
                    foreach(var user in userSuperadmins)
                    {
                        string htmlAdvanceAmountSuperAdmin = _emailSender.PrepareEmailAdvanceSettlement(advancePayment, messageSuperAdmin, "msgsuperadmin");
                        _emailSender.SendEmailAsync(email: user.Email, subject: "Advance Payment Request", htmlAdvanceAmountSuperAdmin);
                    }
                    
                    #endregion
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
                    if (advancePayment.Status == true)
                    {
                        #region email
                        var messageArtist = _businessLayer.BasicConfigurationService.GetBasicConfig().Data.AdvancePaymentArtistTemplate;
                        string htmlAdvanceAmountArtist = _emailSender.PrepareEmailAdvanceSettlement(advancePayment, messageArtist, "msgartist");
                        _emailSender.SendEmailAsync(email: _businessLayer.UserService.GetUserById(advancePayment.UserId).Data.Email, subject: "Advance Payment", htmlAdvanceAmountArtist);
                        #endregion
                    }
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

        [HttpGet]
        [Authorize(Roles = "superadmin")]
        public IActionResult Delete(Guid guid)
        {
            if (guid == Guid.Empty)
            {
                _notyf.Error("Something went wrong");
                return NotFound();
            }
            var item = _businessLayer.AdvancePaymentService.GetAdvancePayment(guid);
            if (item.StatusCode == HttpStatusCode.OK)
            {
                _responseDto = _businessLayer.AdvancePaymentService.DeleteAdvancePayment(item.Data.Id);
                if (_responseDto.StatusCode == HttpStatusCode.OK)
                {
                    return Ok(true);
                }
                else
                {
                    return BadRequest(false);
                }
            }
            else
            {
                return BadRequest(false);
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
