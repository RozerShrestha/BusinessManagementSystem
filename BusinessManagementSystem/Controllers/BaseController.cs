 using AspNetCoreHero.ToastNotification.Abstractions;
using BusinessManagementSystem.BusinessLayer.Services;
using BusinessManagementSystem.Data;
using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Models;
using BusinessManagementSystem.Services;
using BusinessManagementSystem.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using System.Numerics;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace BusinessManagementSystem.Controllers
{
    [Authorize]
    public class BaseController:Controller
    {
        protected readonly IBusinessLayer _businessLayer;
        protected readonly INotyfService _notyf;
        protected readonly IEmailSender _emailSender;
        public static int roleId;
        public static int userId;
        public static Guid userGuid;
        public static string roleName = string.Empty;
        public static string username = string.Empty;
        public static string email = string.Empty;
        public static string fullName = string.Empty;
        public static string PhoneNumber = string.Empty;
        protected UserDto userDto;
        JavaScriptEncoder _javaScriptEncoder;

        
        public BaseController(IBusinessLayer businessLayer, INotyfService notyf, IEmailSender emailSender, JavaScriptEncoder javaScriptEncoder)
        {
            _notyf = notyf;
            _emailSender= emailSender;
            _businessLayer = businessLayer;
            this.userDto = new UserDto();
            _javaScriptEncoder = javaScriptEncoder;
        }
        public override void OnActionExecuting(ActionExecutingContext ctx)
        {
            ViewData["UserDetail"] = UserDetail();
            ViewData["Menu"] = MenuList();
            ViewData["Title"] = _businessLayer.BasicConfigurationService.GetBasicConfig().Data.ApplicationTitle;
        }
         protected IActionResult HandleError(Exception ex)
        {
            return StatusCode(500, new {message= ex.Message });
        }
        private UserDto UserDetail()
        {
            try
            {
                var claims = User.Identities.First().Claims;
                var loggedInEmail = claims.FirstOrDefault(x => x.Type.Contains("emailaddress", StringComparison.OrdinalIgnoreCase)).Value;
                var loggedInUserName = loggedInEmail.Split("@")[0].Trim();

                userDto = _businessLayer.BaseService.UserDetail(loggedInEmail);
                userId = userDto.UserId;
                username = userDto.UserName;
                email = userDto.Email;
                PhoneNumber = userDto.PhoneNumber;
                roleId = userDto.RoleId;
                roleName = userDto.RoleName;
                fullName = userDto.FullName;
                
            }
            catch (Exception ex)
            {

            }
            return userDto;
        }
        private List<MenuDto> MenuList()
        {
            var menuFilter = _businessLayer.BaseService.MenuList(roleName);
            return menuFilter;
        }
        protected bool isAuthorized(int _userId)
        {
            if ((roleName == "admin" || roleName == "hradmin") || userId == _userId)
                return true;
            else
                return false;
        }
        protected string EncodedString(string text)
        {
            return _javaScriptEncoder.Encode(text);
        }

        protected void AppointmentSelectListViewBag()
        {
            dynamic artistList = _businessLayer.UserService.GetAllActiveTattooArtistWithoutAll();
            dynamic referalList = _businessLayer.ReferalService.GetAllActiveReferalList();
            ViewBag.AppointmentSelectList = new Dictionary<string, SelectList>
            {
                { "ArtistList", new SelectList(artistList, "Id", "Name") },
                { "ReferalList", new SelectList(referalList, "Id", "Name") },
                { "TattooCategories", new SelectList(SD.TattooCategories, "Key", "Value") },
                { "AppointmentStatus", new SelectList(SD.ApointmentStatus.Where(p=>p.Key!="All"), "Key", "Value") },
                { "PaymentMethod", new SelectList(SD.PaymentMethods, "Key", "Value") },
                { "Outlet", new SelectList(SD.OutletList, "Key", "Value") }
            };
        }

        protected void DashboardViewBagList(RequestDto requestDto)
        {
            string PaymentTipCombined = _businessLayer.DashboardService.GetPaymentTipSegregation(requestDto);

            IDictionary<string, dynamic> DashboardInfoDict = new Dictionary<string, dynamic>();
            DashboardInfoDict.Add("DataPointsIncomeSegregation", _businessLayer.DashboardService.GetIncomeSegregation(requestDto));
            DashboardInfoDict.Add("DataPointsPaymentSegregation", PaymentTipCombined.Split("##")[0]);
            DashboardInfoDict.Add("DataPointsTipSegregation", PaymentTipCombined.Split("##")[1]);
            DashboardInfoDict.Add("AppointmentSegregationLoginEmployee", _businessLayer.DashboardService.GetDashboardInfo(requestDto, userId));
            if (roleName == SD.Role_TattooAdmin || roleName == SD.Role_Superadmin)
            {
                var AppointmentSegregationAllEmployee = _businessLayer.DashboardService.GetDashboardInfoAllEmployee(requestDto);
                DashboardInfoDict.Add("AppointmentSegregationAllEmployee", AppointmentSegregationAllEmployee);
            }

            ViewBag.DashboardInfo = DashboardInfoDict;
        }

        protected void AdvancePaymentViewBagList()
        {
            dynamic artist = ((IEnumerable<dynamic>)_businessLayer.UserService.GetAllActiveTattooArtistWithoutAll()).Where(artist => artist.Id == userId).ToList();
            dynamic artistList = ((IEnumerable<dynamic>)_businessLayer.UserService.GetAllActiveTattooArtistWithoutAll()).ToList();

            ViewBag.AdvancePaymentSelectList = new Dictionary<string, SelectList>
            {
                { "Artist", new SelectList(artist, "Id", "Name") },
                { "ArtistList", new SelectList(artistList, "Id", "Name") },
                { "PaymentMethod", new SelectList(SD.PaymentMethods, "Key", "Value") },
                { "Status", new SelectList(SD.Status, "Key", "Value") },
            };
        }
    }
}
