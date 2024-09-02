using AspNetCore;
using AspNetCoreHero.ToastNotification.Abstractions;
using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Models;
using BusinessManagementSystem.Services;
using BusinessManagementSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text.Encodings.Web;

namespace BusinessManagementSystem.Controllers
{
    public class DashboardController : BaseController<DashboardController>
    {
        private readonly IWebHostEnvironment _env;
        private readonly ModalView _modalView;
        private ResponseDto<DashboardVM> _responseDto;
        public DashboardController(IWebHostEnvironment env, IUnitOfWork unitOfWork, INotyfService notyf, ILogger<DashboardController> logger, JavaScriptEncoder javaScriptEncoder) : base(unitOfWork, notyf, logger, javaScriptEncoder)
        {
            _env = env;
            _modalView = new ModalView();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
