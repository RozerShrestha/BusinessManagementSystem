using AspNetCoreHero.ToastNotification.Abstractions;
using BusinessManagementSystem.BusinessLayer.Services;
using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Models;
using BusinessManagementSystem.Services;
using BusinessManagementSystem.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using System.Text.Encodings.Web;

namespace BusinessManagementSystem.Controllers
{
    public class PaymentController : BaseController
    {
        public ResponseDto<PaymentDto> _responsePaymentDto;
        private ILogger<PaymentController> _logger;
        private readonly ModalView _modalView;
        private readonly dynamic artistList;
        private readonly dynamic referalList;
        public PaymentController(IBusinessLayer businessLayer, INotyfService notyf, IEmailSender emailSender, ILogger<PaymentController> logger, JavaScriptEncoder javaScriptEncoder) : base(businessLayer, notyf, emailSender, javaScriptEncoder)
        {
            _responsePaymentDto = new ResponseDto<PaymentDto>();
            _modalView = new ModalView("Delete Confirmation !", "Delete", "Are you sure to delete the selected Payment?", "");
            _logger = logger;

        }

        public IActionResult AllPayments()
        {
            RequestDto requestDto = _businessLayer.AppointmentService.GetInitialRequestDtoFilter();
            ViewBag.ModalInformation = _modalView;
            ViewBag.AppointmentStatus = new SelectList(SD.ApointmentStatus, "Key", "Value");
            return View(requestDto);
        }
        public IActionResult MyPayments()
        {
            RequestDto requestDto = _businessLayer.AppointmentService.GetInitialRequestDtoFilter();
            ViewBag.ModalInformation = _modalView;
            ViewBag.AppointmentStatus = new SelectList(SD.ApointmentStatus, "Key", "Value");
            return View(requestDto);
        }
        //public IActionResult PaymentSettlement()
        //{
             
        //}

        #region API
        [HttpPost]
        public IActionResult GetAllPayments([FromBody] RequestDto requestDto)
        {
            _responsePaymentDto = _businessLayer.PaymentService.GetAllPayments(requestDto);
            if (_responsePaymentDto.StatusCode == HttpStatusCode.OK || _responsePaymentDto.StatusCode == HttpStatusCode.NotFound) return Ok(_responsePaymentDto.Datas);
            else
                return BadRequest();
        }
        [HttpPost]
        public IActionResult GetMyPayments([FromBody] RequestDto requestDto)
        {
            _responsePaymentDto = _businessLayer.PaymentService.GetMyPayments(userId,requestDto);
            if (_responsePaymentDto.StatusCode == HttpStatusCode.OK || _responsePaymentDto.StatusCode == HttpStatusCode.NotFound) return Ok(_responsePaymentDto.Datas);
            else
                return BadRequest();
        }
        #endregion
    }
}
