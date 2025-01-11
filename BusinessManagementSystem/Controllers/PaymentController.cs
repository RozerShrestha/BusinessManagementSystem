using AspNetCoreHero.ToastNotification.Abstractions;
using BusinessManagementSystem.BusinessLayer.Services;
using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Models;
using BusinessManagementSystem.Services;
using BusinessManagementSystem.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using System.Text.Encodings.Web;

namespace BusinessManagementSystem.Controllers
{
    [Authorize]
    public class PaymentController : BaseController
    {
        public ResponseDto<PaymentDto> _responsePaymentDto;
        ResponseDto<PaymentTipSettlementDto> _responsePaymentTipSettlementDto;
        private ILogger<PaymentController> _logger;
        private readonly ModalView _modalView;
        private readonly dynamic artistList;
        private readonly dynamic artist;
        public PaymentController(IBusinessLayer businessLayer, INotyfService notyf, IEmailSender emailSender, ILogger<PaymentController> logger, JavaScriptEncoder javaScriptEncoder) : base(businessLayer, notyf, emailSender, javaScriptEncoder)
        {
            _responsePaymentDto = new ResponseDto<PaymentDto>();
            _modalView = new ModalView("Delete Confirmation !", "Delete", "Are you sure to delete the selected Payment?", "");
            artistList = _businessLayer.UserService.GetAllActiveTattooArtist();
            artist = ((IEnumerable<dynamic>)artistList).Where(artist => artist.Id == userId).ToList();
            _logger = logger;

        }
        [Authorize(Roles = "superadmin,admin_tattoo")]
        public IActionResult AllPayments()
        {
            RequestDto requestDto = _businessLayer.AppointmentService.GetInitialRequestDtoFilter();
            requestDto.ParameterFilter = "Status";
            ViewBag.ModalInformation = _modalView;
            ViewBag.AppointmentStatus = new SelectList(SD.ApointmentStatus, "Key", "Value");
            return View(requestDto);
        }
        [Authorize(Roles = "superadmin,admin_tattoo,employee_tattoo")]
        public IActionResult MyPayments()
        {
            RequestDto requestDto = _businessLayer.AppointmentService.GetInitialRequestDtoFilter();
            requestDto.ParameterFilter = "Status";
            ViewBag.ModalInformation = _modalView;
            ViewBag.AppointmentStatus = new SelectList(SD.ApointmentStatus, "Key", "Value");
            return View(requestDto); 
        }
        [Authorize(Roles = "superadmin,admin_tattoo,employee_tattoo")]
        public IActionResult PaymentSettlement()
        {
            RequestDto requestDto = _businessLayer.AppointmentService.GetInitialRequestDtoFilter();
            requestDto.ParameterFilter = "User,Status,Settlement";
            ViewBag.ModalInformation = _modalView;
            ViewBag.AppointmentStatus = new SelectList(SD.ApointmentStatus, "Key", "Value");
            if (roleName == "superadmin")
                ViewBag.ArtistList = new SelectList(artistList, "Id", "Name");
            else
                ViewBag.ArtistList = new SelectList(artist, "Id", "Name");
            return View(requestDto);
        }
        public IActionResult PaymentHistory()
        {
            RequestDto requestDto = _businessLayer.AppointmentService.GetInitialRequestDtoFilter();
            requestDto.ParameterFilter = "User";
            ViewBag.ModalInformation = _modalView;
            if(roleName=="superadmin")
                ViewBag.ArtistList = new SelectList(artistList, "Id", "Name");
            else
                ViewBag.ArtistList = new SelectList(artist, "Id", "Name");
            return View(requestDto);
        }

        #region API
        [HttpPost]
        [Authorize(Roles = "superadmin,admin_tattoo")]
        public IActionResult GetAllPayments([FromBody] RequestDto requestDto)
        {
            _responsePaymentDto = _businessLayer.PaymentService.GetAllPayments(requestDto);
            if (_responsePaymentDto.StatusCode == HttpStatusCode.OK || _responsePaymentDto.StatusCode == HttpStatusCode.NotFound) return Ok(_responsePaymentDto.Datas);
            else
                return BadRequest();
        }
        [HttpPost]
        [Authorize(Roles = "superadmin,admin_tattoo,employee_tattoo")]
        public IActionResult GetMyPayments([FromBody] RequestDto requestDto)
        {
            _responsePaymentDto = _businessLayer.PaymentService.GetMyPayments(userId,requestDto);
            if (_responsePaymentDto.StatusCode == HttpStatusCode.OK || _responsePaymentDto.StatusCode == HttpStatusCode.NotFound) return Ok(_responsePaymentDto.Datas);
            else
                return BadRequest();
        }
        [HttpPost]
        [Authorize(Roles = "superadmin,admin_tattoo,employee_tattoo")]
        public IActionResult GetPaymentTipSettlementData([FromBody] RequestDto requestDto)
         {
            _responsePaymentTipSettlementDto = _businessLayer.PaymentService.GetPaymentTipSettlement(requestDto);
             if (_responsePaymentTipSettlementDto.StatusCode == HttpStatusCode.OK || _responsePaymentTipSettlementDto.StatusCode == HttpStatusCode.NotFound) 
                return Ok(_responsePaymentTipSettlementDto);
            else
                return BadRequest();
        }
        [HttpPost]
        [Authorize(Roles = "superadmin")]
        public IActionResult UpdatePaymentTipSettlementData([FromBody] PaymentTipSettlementDto paymentTipSettlementDto)
        {
            var response =_businessLayer.PaymentService.UpdatePaymentTipSettlement(paymentTipSettlementDto);
            #region email
            var messageArtist = _businessLayer.BasicConfigurationService.GetBasicConfig().Data.PaymentSettlementTemplateArtist;
            var userInfo = _businessLayer.UserService.GetUserById(paymentTipSettlementDto.UserId).Data;
            string artistEmail = userInfo.Email;
            paymentTipSettlementDto.ArtistName = userInfo.FullName;
            string htmlPaymentSettlementArtist = _emailSender.PrepareEmailPaymentSettlement(paymentTipSettlementDto, messageArtist);
            _emailSender.SendEmailAsync(email: artistEmail, subject: "Regarding Payment Settlement", htmlPaymentSettlementArtist);
            #endregion
            return Ok(response);
        }
        [HttpPost]
        public IActionResult GetPaymentHistory([FromBody] RequestDto requestDto)
        {
            var response = _businessLayer.PaymentService.GetPaymentHistory(requestDto);
            return Ok(response.Datas);
        }
        
        #endregion         
    }
} 
