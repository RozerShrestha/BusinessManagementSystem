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
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public DashboardController(IWebHostEnvironment env, IBusinessLayer businessLayer, ILogger<DashboardController> logger, INotyfService notyf, IEmailSender emailSender, JavaScriptEncoder javaScriptEncoder) : base(businessLayer, notyf, emailSender, javaScriptEncoder)
        {
            _env = env;
            _logger = logger;
            //_modalView = new ModalView();
        }

        public IActionResult Index(RequestDto requestDto1)
        {
            RequestDto requestDto = new RequestDto();
            if (requestDto1.StartDate==DateTime.MinValue)
                requestDto = _businessLayer.BaseService.GetInitialRequestDtoFilterDashboard();
            else
            {
                requestDto = requestDto1;
            }
            requestDto.ParameterFilter = ""; // this means only start date and end date
            base.DashboardViewBagList(requestDto);

            return View(requestDto);
        }

        private IDictionary<string, dynamic> PrepareDashboardInfo(RequestDto requestDto)
        {
            requestDto.ParameterFilter = ""; // This means only start date and end date

            string PaymentTipCombined = _businessLayer.DashboardService.GetPaymentTipSegregation(requestDto);
            string Payment = PaymentTipCombined.Split("##")[0];
            string Tips = PaymentTipCombined.Split("##")[1];
            var AppointmentSegregationLoginEmployee = _businessLayer.DashboardService.GetDashboardInfo(requestDto, userId);

            IDictionary<string, dynamic> DashboardInfoDict = new Dictionary<string, dynamic>
            {
                { "DataPointsIncomeSegregation", _businessLayer.DashboardService.GetIncomeSegregation(requestDto) },
                { "DataPointsPaymentSegregation", Payment },
                { "DataPointsTipSegregation", Tips },
                { "AppointmentSegregationLoginEmployee", AppointmentSegregationLoginEmployee }
            };

            if (roleName == SD.Role_TattooAdmin || roleName == SD.Role_Superadmin)
            {
                var AppointmentSegregationAllEmployee = _businessLayer.DashboardService.GetDashboardInfoAllEmployee(requestDto);
                DashboardInfoDict.Add("AppointmentSegregationAllEmployee", AppointmentSegregationAllEmployee);
            }

            return DashboardInfoDict;
        }

        #region API
        [HttpPost]
        public IActionResult GetDashboardInfo([FromBody] RequestDto requestDto)
        {
            var dashboardInfo = PrepareDashboardInfo(requestDto);
            return Json(dashboardInfo);
        }
        #endregion
    }
}
