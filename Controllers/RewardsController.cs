using Microsoft.AspNetCore.Mvc;

namespace Shuddhi.Controllers
{
    public class RewardsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


       public IActionResult Points() 
        {
            return View();
        } 
    }
}
