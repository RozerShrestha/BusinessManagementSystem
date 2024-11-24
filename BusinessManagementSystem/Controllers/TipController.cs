using AspNetCoreHero.ToastNotification.Abstractions;
using BusinessManagementSystem.BusinessLayer.Services;
using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Models;
using BusinessManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Encodings.Web;

namespace BusinessManagementSystem.Controllers
{
    [Authorize(Roles = "superadmin,admin_tattoo,employee_tattoo")]
    public class TipController : BaseController
    {
        public ResponseDto<Tip> _responseDto;
        public ResponseDto<TipDto> _responseTipDto;
        private ILogger<TipController> _logger;
        private readonly ModalView _modalView;
        private readonly dynamic artistList;
        private readonly dynamic referalList;
        public TipController(IBusinessLayer businessLayer, INotyfService notyf, IEmailSender emailSender, ILogger<TipController> logger, JavaScriptEncoder javaScriptEncoder) : base(businessLayer, notyf, emailSender, javaScriptEncoder)
        {
            _responseDto = new ResponseDto<Tip>();
            _responseTipDto = new ResponseDto<TipDto>();
            _modalView = new ModalView("Delete Confirmation !", "Delete", "Are you sure to delete the selected Tip?", "");
            _logger = logger;
        }
        public IActionResult Index()
        {
            ViewBag.ModalInformation = _modalView;
            return View();
        }
        public IActionResult MyTips()
        {
            ViewBag.ModalInformation = _modalView;
            return View();
        }

        #region API
        public IActionResult GetAllTips()
        {
            _responseTipDto = _businessLayer.TipService.GetAllTips();
            if (_responseTipDto.StatusCode == HttpStatusCode.OK || _responseTipDto.StatusCode==HttpStatusCode.NotFound) return Ok(_responseTipDto.Datas);
            else
                return BadRequest();
        }

        
        public IActionResult GetMyTips()
        {
            _responseTipDto = _businessLayer.TipService.GetMyTips(userId);
            if (_responseTipDto.StatusCode == HttpStatusCode.OK || _responseTipDto.StatusCode == HttpStatusCode.NotFound) return Ok(_responseTipDto.Datas);
            else
                return BadRequest();
        }
        #endregion
    }
}
