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
            List<DataPoint> dataPoints = new List<DataPoint>();

            dataPoints.Add(new DataPoint("Payment", 403400));
            dataPoints.Add(new DataPoint("Tips", 57000));

            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

            return View();
        }
    }
}
