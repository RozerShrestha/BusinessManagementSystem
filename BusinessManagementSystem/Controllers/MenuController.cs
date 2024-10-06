using AspNetCore;
using AspNetCoreHero.ToastNotification.Abstractions;
using BusinessManagementSystem.BusinessLayer.Services;
using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Models;
using BusinessManagementSystem.Repositories;
using BusinessManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using System.Text.Encodings.Web;

namespace BusinessManagementSystem.Controllers
{
    [Authorize(Roles = "superadmin,admin_tattoo,admin_kaffe,admin_apartment")]
    public class MenuController : BaseController
    {
        private ResponseDto<Menu> _responseDto;
        private readonly dynamic parentList;
        private readonly dynamic roleList;
        private ILogger<MenuController> _logger;
        public MenuController(IBusinessLayer businessLayer, INotyfService notyf, IEmailSender emailSender, ILogger<MenuController> logger, JavaScriptEncoder javaScriptEncoder) : base(businessLayer, notyf, emailSender, javaScriptEncoder)
        {
            _responseDto = new ResponseDto<Menu>();
            _logger = logger;
            parentList = _businessLayer.MenuService.ParentList();
            roleList = _businessLayer.MenuService.RoleList();
            

        }
        // GET: MenuController
        public ActionResult Index()
        {
            _responseDto = _businessLayer.MenuService.GetAllMenu();
            return View(_responseDto.Datas);
        }

        // GET: MenuController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MenuController/Create
        public ActionResult Create()
        {
            ViewData["ParentList"] = new SelectList(parentList, "Parent", "Name");
            ViewData["RoleList"] = new SelectList(roleList, "Id", "Name");
            return View();
        }

        // POST: MenuController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Menu menu)
        {
            if (ModelState.IsValid)
            {
                _responseDto = _businessLayer.MenuService.Create(menu);
                if (_responseDto.StatusCode == HttpStatusCode.OK)
                {
                    _notyf.Success(_responseDto.Message);
                }
                else
                {
                    _notyf.Error(_responseDto.Message);
                }
            }
            else
            {
                IEnumerable<ModelError> errors = ModelState.Values.SelectMany(v => v.Errors).ToList();
                foreach (var error in errors)
                {
                    _notyf.Error(error.ErrorMessage);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: MenuController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MenuController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MenuController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MenuController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            _responseDto = _businessLayer.MenuService.Delete(id);
            if (_responseDto.StatusCode == HttpStatusCode.OK)
            {
                _notyf.Success(_responseDto.Message);
            }
            else
            {
                _notyf.Error(_responseDto.Message);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
