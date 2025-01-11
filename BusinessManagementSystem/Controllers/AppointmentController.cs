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
using Newtonsoft.Json;
using System.Net;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Diagnostics.Contracts;
using BusinessManagementSystem.Helper;
using BusinessManagementSystem.Enums;
using AspNetCore;

namespace BusinessManagementSystem.Controllers
{
    [Authorize]
    public class AppointmentController : BaseController
    {
        public ResponseDto<Appointment> _responseDto;
        public ResponseDto<AppointmentDto> _responseAppointmentDto;
        private ILogger<AppointmentController> _logger;
        private readonly ModalView _modalView;
        public AppointmentController(IBusinessLayer businessLayer, INotyfService notyf, IEmailSender emailSender, ILogger<AppointmentController> logger, JavaScriptEncoder javaScriptEncoder) : base(businessLayer, notyf, emailSender, javaScriptEncoder)
        {
            _responseDto = new ResponseDto<Appointment>();
            _responseAppointmentDto = new ResponseDto<AppointmentDto>();
            _modalView = new ModalView("Delete Confirmation !", "Delete", "Are you sure to delete the selected Appointment?", "");
            _logger = logger;

        }
        [Authorize(Roles = "superadmin,admin_tattoo")]
        public IActionResult Index()
        {
            RequestDto requestDto = _businessLayer.AppointmentService.GetInitialRequestDtoFilter();
            requestDto.ParameterFilter = "Status";
            ViewBag.ModalInformation = _modalView;
            ViewBag.AppointmentStatus = new SelectList(SD.ApointmentStatus, "Key", "Value");
            return View(requestDto);
        }

        [Authorize(Roles = "superadmin,admin_tattoo,employee_tattoo")]
        public IActionResult MyAppointments()
        {
            RequestDto requestDto = _businessLayer.AppointmentService.GetInitialRequestDtoFilter();
            requestDto.ParameterFilter = "Status";
            ViewBag.ModalInformation = _modalView;
            ViewBag.AppointmentStatus = new SelectList(SD.ApointmentStatus, "Key", "Value");
            return View(requestDto);
        }
        [Authorize(Roles = "superadmin,admin_tattoo,employee_tattoo")]
        public IActionResult Detail(Guid guid)
        {
            if (guid == Guid.Empty) return NotFound();
            var _responseDto = _businessLayer.AppointmentService.GetAppointmentByGuid(guid);
            if (_responseDto == null)
            {
                return NotFound();
            }
            return View(_responseDto.Data);
        }

        [HttpGet]
        [Authorize(Roles = "superadmin,admin_tattoo")]
        public IActionResult Create()
        {
            AppointmentSelectListViewBag();
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "superadmin,admin_tattoo")]
        public IActionResult Create(AppointmentDto appointmentDto)
        {
            var js = JsonConvert.SerializeObject(appointmentDto);
            AppointmentSelectListViewBag();
            if (ModelState.IsValid)
            {
                _responseDto = _businessLayer.AppointmentService.CreateAppointment(appointmentDto);
                if (_responseDto.StatusCode == HttpStatusCode.OK)
                {
                    _notyf.Success(_responseDto.Message);
                    #region email
                    var messageArtist = _businessLayer.BasicConfigurationService.GetBasicConfig().Data.NewAppointmentTemplateArtist;
                    var messageClient = _businessLayer.BasicConfigurationService.GetBasicConfig().Data.NewAppointmentTemplateClient;
                    var userInfo = _businessLayer.UserService.GetUserById(appointmentDto.UserId).Data;
                    string artistEmail = userInfo.Email;
                    appointmentDto.ArtistAssigned = userInfo.FullName;
                    string htmlNewAppointmentArtist = _emailSender.PrepareEmailAppointmentArtist(appointmentDto, messageArtist);
                    string htmlNewAppointmentClient = _emailSender.PrepareEmailAppointmentClient(appointmentDto, messageClient);
                    _emailSender.SendEmailAsync(email: artistEmail, subject: "New Appointment", htmlNewAppointmentArtist);
                    _emailSender.SendEmailAsync(email: appointmentDto.ClientEmail, subject: "New Appointment", htmlNewAppointmentClient);
                    #endregion
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _notyf.Error(_responseDto.Message);
                    return View(appointmentDto);
                }
            }
            else
            {
                IEnumerable<ModelError> errors = ModelState.Values.SelectMany(v => v.Errors).ToList();
                foreach (var error in errors)
                {
                    _notyf.Error(error.ErrorMessage);
                }
                return View(appointmentDto);
            }
        }

        [Authorize(Roles = "superadmin,admin_tattoo")]
        public IActionResult Edit(Guid guid)
        {
            if (guid == Guid.Empty) return NotFound();
            AppointmentSelectListViewBag();
            _responseAppointmentDto = _businessLayer.AppointmentService.GetAppointmentByGuid(guid);

            if (roleName == SD.Role_Superadmin || roleName == SD.Role_TattooAdmin || userId == _responseAppointmentDto.Data.UserId)
            {
                if (_responseAppointmentDto.StatusCode != HttpStatusCode.OK)
                {
                    return NotFound();
                }
                else
                    return View(_responseAppointmentDto.Data);
            }
            else
            {
                _notyf.Warning($"{fullName} is not authroized to perform this task");
                return RedirectToAction("AccessDenied", "Error");
            }




        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "superadmin,admin_tattoo")]
        public IActionResult Edit(AppointmentDto appointmentDto)
        {
            string htmlUpdateAppointmentArtist = "";
            string htmlUpdateAppointmentClient = "";
            string messageArtist = "";
            string messageClient = "";
            appointmentDto.AppointmentCreatedId = userId;
            appointmentDto.DbStatus = _businessLayer.AppointmentService.GetAppointmentByGuid(appointmentDto.guid).Data.Status;
            if (roleName == SD.Role_Superadmin || roleName == SD.Role_TattooAdmin || userId == appointmentDto.UserId)
            {
                AppointmentSelectListViewBag();
                if (ModelState.IsValid)
                {
                    _responseDto = _businessLayer.AppointmentService.UpdateAppointment(appointmentDto);
                    if (_responseDto.StatusCode == HttpStatusCode.OK)
                    {
                        _notyf.Success(_responseDto.Message);
                        #region email
                        
                        var userInfo = _businessLayer.UserService.GetUserById(appointmentDto.UserId).Data;
                        string artistEmail = userInfo.Email;
                        appointmentDto.ArtistAssigned = userInfo.FullName;
                        if (appointmentDto.Status == AppointmentStat.Completed.ToString())
                        {
                            messageArtist = _businessLayer.BasicConfigurationService.GetBasicConfig().Data.AppointmentCompletedArtist;
                            messageClient = _businessLayer.BasicConfigurationService.GetBasicConfig().Data.AppointmentCompletedClient;
                            htmlUpdateAppointmentArtist = _emailSender.PrepareEmailAppointmentArtist(appointmentDto, messageArtist);
                            htmlUpdateAppointmentClient = _emailSender.PrepareEmailAppointmentClient(appointmentDto, messageClient);
                        }
                        else
                        {
                            messageArtist = _businessLayer.BasicConfigurationService.GetBasicConfig().Data.AppointmentUpdateTemplateArtist;
                            messageClient = _businessLayer.BasicConfigurationService.GetBasicConfig().Data.AppointmentUpdateTemplateClient;
                            htmlUpdateAppointmentArtist = _emailSender.PrepareEmailAppointmentArtist(appointmentDto, messageArtist);
                            htmlUpdateAppointmentClient = _emailSender.PrepareEmailAppointmentClient(appointmentDto, messageClient);
                        }
                       
                        _emailSender.SendEmailAsync(email: artistEmail, subject: "Regarding Change In Appointment", htmlUpdateAppointmentArtist);
                        _emailSender.SendEmailAsync(email: appointmentDto.ClientEmail, subject: "Regarding Change In Appointment", htmlUpdateAppointmentClient);
                        #endregion
                        if (roleName == SD.Role_Superadmin || roleName == SD.Role_TattooAdmin)
                            return RedirectToAction(nameof(Index));
                        else
                            return RedirectToAction(nameof(MyAppointments));
                    }
                    else
                    {
                        _notyf.Error(_responseDto.Message);
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
                    return View(appointmentDto);
                }
            }
            else
            {
                _notyf.Warning($"{fullName} is not authroized to perform this task");
                return RedirectToAction("AccessDenied", "Error");
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
            var item = _businessLayer.AppointmentService.GetAppointmentByGuid(guid);
            if (item.StatusCode == HttpStatusCode.OK)
            {
                _responseDto = _businessLayer.AppointmentService.DeleteAppointmentByGuid(item.Data.guid);
                if (_responseDto.StatusCode == HttpStatusCode.OK)
                {
                    return Ok(_responseDto);
                }
                else
                {
                    return BadRequest(_responseDto);
                }
            }
            else
            {
                return BadRequest(_responseDto);
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "superadmin")]
        public IActionResult DeleteConfirmed(int appointmentId)
        {
            _responseDto = _businessLayer.AppointmentService.DeleteAppointmentById(appointmentId);
            if (_responseDto.StatusCode == HttpStatusCode.OK)
                return RedirectToAction(nameof(Index));
            else
            {
                _notyf.Error($"Error deleting User due to : {_responseDto.Message}");
                return View();
            }
        }


        #region API CALLS

        [HttpPost]
        [Authorize(Roles = "superadmin,admin_tattoo")]
        public IActionResult GetAllAppointment([FromBody] RequestDto requestDto)
        {
            _responseAppointmentDto = _businessLayer.AppointmentService.GetAllAppointment(requestDto);
            if (_responseAppointmentDto.StatusCode == HttpStatusCode.OK || _responseAppointmentDto.StatusCode == HttpStatusCode.NotFound) return Ok(_responseAppointmentDto.Datas);
            else return BadRequest();
        }
        [HttpPost]
        [Authorize(Roles = "superadmin,admin_tattoo,employee_tattoo")]
        public IActionResult GetAllAppointmentByArtist([FromBody] RequestDto requestDto)
        {
            _responseAppointmentDto = _businessLayer.AppointmentService.GetAllAppointmentByArtist(userId, requestDto);
            if (_responseAppointmentDto.StatusCode == HttpStatusCode.OK || _responseAppointmentDto.StatusCode == HttpStatusCode.NotFound) return Ok(_responseAppointmentDto.Datas);
            else return BadRequest();
        }
        [HttpGet]
        [Authorize(Roles = "superadmin,admin_tattoo,employee_tattoo")]
        public IActionResult GetPaymentCalculation(bool isForeigner, string category, double totalHours, int deposit, int discount = 0, double discountInHour = 0)
        {
            double totalCost = 0.0;
            if (!string.IsNullOrEmpty(category) && totalHours != 0 && deposit >= 1000)
            {
                string costDescription = _businessLayer.AppointmentService.GetTotalCost(isForeigner, category, totalHours, deposit, discount, discountInHour, out totalCost);
                var result = new
                {
                    TotalCost = totalCost,
                    CostDescription = costDescription
                };
                return Ok(result);
            }
            else
            {
                return BadRequest("Parameters didn't match the required data");
            }

        }

        #endregion

    }
}
