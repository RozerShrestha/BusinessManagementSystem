﻿using AspNetCoreHero.ToastNotification.Abstractions;
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
        [Authorize(Roles = "superadmin,admin_tattoo")]
        public IActionResult Index()
        {
            RequestDto requestDto = _businessLayer.BaseService.GetInitialRequestDtoFilter();
            requestDto.ParameterFilter = "Settlement"; 
            ViewBag.ModalInformation = _modalView;
            return View(requestDto);
        }
        [Authorize(Roles = "superadmin,admin_tattoo,employee_tattoo")]
        public IActionResult MyTips()
        {
            RequestDto requestDto = _businessLayer.BaseService.GetInitialRequestDtoFilter();
            requestDto.ParameterFilter = "Settlement";
            ViewBag.ModalInformation = _modalView;
            return View(requestDto);
        }

        #region API
        [Authorize(Roles = "superadmin,admin_tattoo")]
        public IActionResult GetAllTips([FromBody] RequestDto requestDto)
        {
            _responseTipDto = _businessLayer.TipService.GetAllTips(requestDto);
            if (_responseTipDto.StatusCode == HttpStatusCode.OK || _responseTipDto.StatusCode==HttpStatusCode.NotFound) return Ok(_responseTipDto.Datas);
            else
                return BadRequest();
        }

        [Authorize(Roles = "superadmin,admin_tattoo,employee_tattoo")]
        public IActionResult GetMyTips([FromBody] RequestDto requestDto)
        {
            _responseTipDto = _businessLayer.TipService.GetMyTips(userId, requestDto);
            if (_responseTipDto.StatusCode == HttpStatusCode.OK || _responseTipDto.StatusCode == HttpStatusCode.NotFound   ) return Ok(_responseTipDto.Datas);
            else
                return BadRequest();
        }
        #endregion
    }
}
