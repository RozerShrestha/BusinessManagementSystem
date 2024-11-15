using Microsoft.AspNetCore.Mvc;

namespace BusinessManagementSystem.Controllers
{
    public class PaymentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
