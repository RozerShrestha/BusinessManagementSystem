using AspNetCoreHero.ToastNotification.Abstractions;
using BusinessManagementSystem.BusinessLayer.Services;
using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Models;
using BusinessManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;
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
            ViewBag.ModalInformation = _modalView;
            return View();
        }
        public IActionResult MyPayments()
        {
            ViewBag.ModalInformation = _modalView;
            return View();
        }

        #region API
        public IActionResult GetAllPayments(RequestDto requestDto)
        {
            _responsePaymentDto = _businessLayer.PaymentService.GetAllPayments();
            if (_responsePaymentDto.StatusCode == HttpStatusCode.OK || _responsePaymentDto.StatusCode == HttpStatusCode.NotFound) return Ok(_responsePaymentDto.Datas);
            else
                return BadRequest();
        }


        public IActionResult GetMyPayments(RequestDto requestDto)
        {
            _responsePaymentDto = _businessLayer.PaymentService.GetMyPayments(userId);
            if (_responsePaymentDto.StatusCode == HttpStatusCode.OK || _responsePaymentDto.StatusCode == HttpStatusCode.NotFound) return Ok(_responsePaymentDto.Datas);
            else
                return BadRequest();
        }
        #endregion
    }
}
