using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Disarmingly_Simple.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Disarmingly_Simple.Controllers
{
    public class ApplicationController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            Application newApp = new Application();
            //newApp.GetModelsForSet();
            //newApp.GetCreateNewSession();
            //newApp.GetSessionStatus_of_GetCreateNewSession();
            //newApp.GetSpeechRecogEventWithSessionId();
            //;
            return View();
        }
        [Route("/AudioCapture")]
        public IActionResult AudioCapture()
        {
            return View();
        }
        [Route("/TextOutput")]
        public IActionResult TextOutput(string fileName)
        {
            Application newApp = new Application();
            var result = newApp.GetSpeechRecogEventWithSessionId(fileName);
            return View(result);
        }
    }
}
