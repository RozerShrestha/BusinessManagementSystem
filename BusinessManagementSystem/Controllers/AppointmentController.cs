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

namespace BusinessManagementSystem.Controllers
{
    [Authorize(Roles = "superadmin,admin_tattoo,employee_tattoo")]
    public class AppointmentController : BaseController
    {
        public ResponseDto<Appointment> _responseDto;
        public ResponseDto<AppointmentDto> _responseAppointmentDto;
        private ILogger<AppointmentController> _logger;
        private readonly ModalView _modalView;
        private readonly dynamic artistList;
        private readonly dynamic referalList;
        public AppointmentController(IBusinessLayer businessLayer, INotyfService notyf, IEmailSender emailSender, ILogger<AppointmentController> logger, JavaScriptEncoder javaScriptEncoder) : base(businessLayer, notyf, emailSender, javaScriptEncoder)
        {

            _responseDto = new ResponseDto<Appointment>();
            _responseAppointmentDto = new ResponseDto<AppointmentDto>();
            _modalView = new ModalView("Delete Confirmation !", "Delete", "Are you sure to delete the selected Appointment?", "");
            artistList = _businessLayer.UserService.GetAllActiveTattooArtist();
            referalList = _businessLayer.ReferalService.GetAllActiveReferalList();
            _logger = logger;

        }
        public IActionResult Index()
        {
            ViewBag.ModalInformation = _modalView;
            return View(_responseDto);
        }

        [HttpGet]
        
        public IActionResult Create()
        {
            //var jso = JsonConvert.SerializeObject(artistList,new JsonSerializerSettings { ReferenceLoopHandling=ReferenceLoopHandling.Ignore});
            ViewBag.ArtistList = new SelectList(artistList, "Id", "Name");
            ViewBag.ReferalList = new SelectList(referalList, "Id", "Name");
            ViewBag.TattooCategories = new SelectList(SD.TattooCategories, "Key", "Value");
            ViewBag.AppointmentStatus=new SelectList(SD.ApointmentStatus, "Key", "Value");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Appointment appointment)
        {
            ViewBag.ArtistList = new SelectList(artistList, "Id", "Name");
            ViewBag.ReferalList = new SelectList(referalList, "Id", "Name");
            ViewBag.TattooCategories = new SelectList(SD.TattooCategories, "Key", "Value");
            ViewBag.AppointmentStatus = new SelectList(SD.ApointmentStatus, "Key", "Value");

            if (ModelState.IsValid)
            {
                _responseDto=_businessLayer.AppointmentService.CreateAppointment(appointment);
                if (_responseDto.StatusCode == HttpStatusCode.OK)
                {
                    _notyf.Success(_responseDto.Message);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _notyf.Error(_responseDto.Message);
                    return View(appointment);
                }
            }
            else
            {
                IEnumerable<ModelError> errors = ModelState.Values.SelectMany(v => v.Errors).ToList();
                foreach (var error in errors)
                {
                    _notyf.Error(error.ErrorMessage);
                }
                return View(appointment);
            }
        }

        public IActionResult Edit(Guid guid)
        {
            if (guid == Guid.Empty)return NotFound();
            ViewBag.ArtistList = new SelectList(artistList, "Id", "Name");
            ViewBag.ReferalList = new SelectList(referalList, "Id", "Name");
            ViewBag.TattooCategories = new SelectList(SD.TattooCategories, "Key", "Value");
            ViewBag.AppointmentStatus = new SelectList(SD.ApointmentStatus, "Key", "Value");
            var _responseDto = _businessLayer.AppointmentService.GetAppointmentByGuid(guid);
            if (_responseDto == null)
            {
                return NotFound();
            }
            return View(_responseDto.Data);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Appointment appointment)
        {
            //ModelState.Remove(nameof(userDto.Password)); //just to ignore ConfirmPassword to validate
            //ModelState.Remove(nameof(userDto.ConfirmPassword)); //just to ignore ConfirmPassword to validate
            if (roleName == SD.Role_Superadmin || userId == userDto.UserId)
            {
                if (ModelState.IsValid)
                {
                    ViewBag.ArtistList = new SelectList(artistList, "Id", "Name");
                    ViewBag.ReferalList = new SelectList(referalList, "Id", "Name");
                    ViewBag.TattooCategories = new SelectList(SD.TattooCategories, "Key", "Value");
                    ViewBag.AppointmentStatus = new SelectList(SD.ApointmentStatus, "Key", "Value");
                    
                    _responseDto = _businessLayer.AppointmentService.UpdateAppointment(appointment);
                    if (_responseDto.StatusCode == HttpStatusCode.OK)
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
            else
            {
                _notyf.Warning($"{fullName} is not authroized to perform this task");
                return RedirectToAction(nameof(Index));
            }


        }

        [HttpGet]
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
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int appointmentId)
        {
            _responseDto = _businessLayer.AppointmentService.GetAppointmentById(appointmentId);
            if (_responseDto.Data != null)
            {
                try
                {
                    _responseDto = _businessLayer.AppointmentService.DeleteAppointmentById(appointmentId);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _notyf.Error($"Error deleting User due to : {ex.Message}");
                    return View(_responseDto.Data.guid);
                }
            }
            else
            {
                _notyf.Error("Error: User not Found");
                return NotFound();
            }

        }


        #region API CALLS

        [HttpGet]
        public IActionResult GetAllAppointment()
        {
           
            _responseAppointmentDto = _businessLayer.AppointmentService.GetAllAppointment();
            

            if (_responseAppointmentDto.StatusCode == HttpStatusCode.OK)
            {
                return Ok(_responseAppointmentDto.Datas);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public IActionResult GetPaymentCalculation(string category, int totalHours, int deposit, int discount=0, int discountInHour=0)
        {
            int totalCost = 0;
            if (!string.IsNullOrEmpty(category) && totalHours != 0 && deposit >= 1000)
            {
                string costDescription = _businessLayer.AppointmentService.GetTotalCost(category, totalHours, deposit, discount, discountInHour,out totalCost);
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
