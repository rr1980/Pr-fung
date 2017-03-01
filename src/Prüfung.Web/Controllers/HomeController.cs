using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Prüfung.ViewModels;

namespace Prüfung.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Authorize(Policy = "ReadPolicy")]
        public IActionResult Index()
        {
            return View(new HomeViewModel());
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
