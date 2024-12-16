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
        ResponseDto<PaymentTipSettlementDto> _responsePaymentTipSettlementDto;
        private ILogger<PaymentController> _logger;
        private readonly ModalView _modalView;
        private readonly dynamic artistList;
        public PaymentController(IBusinessLayer businessLayer, INotyfService notyf, IEmailSender emailSender, ILogger<PaymentController> logger, JavaScriptEncoder javaScriptEncoder) : base(businessLayer, notyf, emailSender, javaScriptEncoder)
        {
            _responsePaymentDto = new ResponseDto<PaymentDto>();
            _modalView = new ModalView("Delete Confirmation !", "Delete", "Are you sure to delete the selected Payment?", "");
            artistList = _businessLayer.UserService.GetAllActiveTattooArtist();
            _logger = logger;

        }

        public IActionResult AllPayments()
        {
            RequestDto requestDto = _businessLayer.AppointmentService.GetInitialRequestDtoFilter();
            requestDto.ParameterFilter = "Status";
            ViewBag.ModalInformation = _modalView;
            ViewBag.AppointmentStatus = new SelectList(SD.ApointmentStatus, "Key", "Value");
            return View(requestDto);
        }
        public IActionResult MyPayments()
        {
            RequestDto requestDto = _businessLayer.AppointmentService.GetInitialRequestDtoFilter();
            requestDto.ParameterFilter = "Status";
            ViewBag.ModalInformation = _modalView;
            ViewBag.AppointmentStatus = new SelectList(SD.ApointmentStatus, "Key", "Value");
            return View(requestDto); 
        }
        public IActionResult PaymentSettlement()
        {
            RequestDto requestDto = _businessLayer.AppointmentService.GetInitialRequestDtoFilter();
            requestDto.ParameterFilter = "User,Status,Settlement";
            ViewBag.ModalInformation = _modalView;
            ViewBag.AppointmentStatus = new SelectList(SD.ApointmentStatus, "Key", "Value");
            ViewBag.ArtistList = new SelectList(artistList, "Id", "Name");
            return View(requestDto);
        }

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
        [HttpPost]
        public IActionResult GetPaymentTipSettlementData([FromBody] RequestDto requestDto)
         {
            _responsePaymentTipSettlementDto = _businessLayer.PaymentService.GetPaymentTipSettlement(requestDto);
             if (_responsePaymentTipSettlementDto.StatusCode == HttpStatusCode.OK || _responsePaymentTipSettlementDto.StatusCode == HttpStatusCode.NotFound) 
                return Ok(_responsePaymentTipSettlementDto);
            else
                return BadRequest();
        }

        [HttpPost]
        //Update the payment Settlement and Tip Settlement and then add the payment history
        public IActionResult UpdatePaymentTipSettlementData([FromBody] PaymentTipSettlementDto paymentTipSettlementDto)
        {
            var response =_businessLayer.PaymentService.UpdatePaymentTipSettlement(paymentTipSettlementDto);
            //need to update the payment history as well.
            return Ok(response);
        }
        #endregion         
    }
} 
