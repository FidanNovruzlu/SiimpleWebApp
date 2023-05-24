using Microsoft.AspNetCore.Mvc;
using SiimpleWebApp.DAL;
using SiimpleWebApp.Models;
using SiimpleWebApp.ViewModels;
using System.Diagnostics;

namespace SiimpleWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly SiimpleDbContect _siimpleDbContect;

        public HomeController(SiimpleDbContect siimpleDbContect)
        {
           _siimpleDbContect = siimpleDbContect;
        }

        public IActionResult Index()
        {
            List<UsSection> usSections = _siimpleDbContect.UsSections.ToList();
            HomeVM homeVM = new HomeVM()
            {
                UsSections=usSections
            };
            return View(homeVM);
        }

    }
}