using AspNetCore;
using AspNetCoreHero.ToastNotification.Abstractions;
using BusinessManagementSystem.BusinessLayer.Services;
using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Dto.Chart;
using BusinessManagementSystem.Models;
using BusinessManagementSystem.Services;
using BusinessManagementSystem.Utility;
using BusinessManagementSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text.Encodings.Web;

namespace BusinessManagementSystem.Controllers
{
    [Authorize]
    public class DashboardController : BaseController
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<DashboardController> _logger;
        //private readonly ModalView _modalView;
        private ResponseDto<DashboardVM> _responseDto;
        public DashboardController(IWebHostEnvironment env, IBusinessLayer businessLayer, ILogger<DashboardController> logger, INotyfService notyf, IEmailSender emailSender, JavaScriptEncoder javaScriptEncoder) : base(businessLayer, notyf, emailSender, javaScriptEncoder)
        {
            _env = env;
            _logger = logger;
            //_modalView = new ModalView();
        }

        public IActionResult Index()
        {
            ViewBag.DataPointsIncomeSegregation = _businessLayer.DashboardService.GetIncomeSegregation();
            string PaymentTipCombined= _businessLayer.DashboardService.GetPaymentTipSegregation();
            string Payment = PaymentTipCombined.Split("##")[0];
            string Tips = PaymentTipCombined.Split("##")[1];

            ViewBag.DataPointsPaymentSegregation = Payment;
            ViewBag.DataPointsTipSegregation = Tips;
            return View();
        }

        #region API

        #endregion
    }
}
