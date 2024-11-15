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
        private ILogger<TipController> _logger;
        private readonly ModalView _modalView;
        private readonly dynamic artistList;
        private readonly dynamic referalList;
        public TipController(IBusinessLayer businessLayer, INotyfService notyf, IEmailSender emailSender, ILogger<TipController> logger, JavaScriptEncoder javaScriptEncoder) : base(businessLayer, notyf, emailSender, javaScriptEncoder)
        {
            _responseDto = new ResponseDto<Tip>();
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
        [HttpGet]
        public IActionResult GetAllTips()
        {
            _responseDto = _businessLayer.TipService.GetAllTips();
            if (_responseDto.StatusCode == HttpStatusCode.OK) return Ok(_responseDto.Datas);
            else
                return BadRequest();
        }

        [HttpGet]
        public IActionResult GetMyTips()
        {
            _responseDto = _businessLayer.TipService.GetMyTips(userId);
            if (_responseDto.StatusCode == HttpStatusCode.OK) return Ok(_responseDto.Datas);
            else
                return BadRequest();
        }
        #endregion
    }
}
