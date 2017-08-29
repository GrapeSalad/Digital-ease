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
            //The followind 5 lines were for testing the WATSON Nuget Package
            //Application newApp = new Application();
            //newApp.GetModelsForSet();
            //newApp.GetCreateNewSession();
            //newApp.GetSessionStatus_of_GetCreateNewSession();
            //newApp.GetSpeechRecogEventWithSessionId();
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
            var speechEventResult = newApp.GetSpeechRecogEventWithSessionId(fileName);
            var result = newApp.parseSpeechToTextResult(speechEventResult);
            //var result = newApp.splitArrayTesting(newApp.bgToGreen);
            return View(result);
        }
    }
}
