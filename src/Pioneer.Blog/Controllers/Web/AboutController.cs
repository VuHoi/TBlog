using Microsoft.AspNetCore.Mvc;

namespace Pioneer.Blog.Controllers.Web
{
    public class AboutController : Controller
    {
        /// <summary>
        /// GET: About
        /// </summary>
        public IActionResult Index()
        {
            ViewBag.Description = "Các thông tin về khoa học công nghệ";
            ViewBag.Selected = "about";
            return View();
        }
    }
}