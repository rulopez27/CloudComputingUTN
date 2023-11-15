using CloudComputingUTN.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CloudComputingUTN.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {

        }

        public IActionResult Index()
        {
            ViewData["HostName"] = Request.Host.Host;
            string environment = DatabaseEngine.InstanceDatabaseEngine().GetEnvironment() == AppEnvironments.Development ?
                "localhost"
                : "AWS RDS";
            ViewData["DatabaseEngine"] = $"{DatabaseEngine.InstanceDatabaseEngine().GetDatabaseEngine()} en {environment}";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}