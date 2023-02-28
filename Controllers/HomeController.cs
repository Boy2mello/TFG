using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Web;
using TFG.Models;
using System.IO;
using static System.Net.WebRequestMethods;
using ExcelDataReader;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using TFG.Interface;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Diagnostics;

namespace TFG.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        readonly IExcelRepository _excelRepository;

        public HomeController(ILogger<HomeController> logger, IExcelRepository excelRepository)
        {
            _logger = logger;
            _excelRepository = excelRepository; 
        }

        public IActionResult Index(List<ExcelModel> excel = null)
        {
            excel = excel == null ? new List<ExcelModel>() : excel;
            return View(excel);
        }

        [HttpPost]
        public IActionResult Index([FromForm(Name = "files")]  IFormFile file, [FromServices] IHostingEnvironment hostingEnvironment)
        {
            var fileFullPath = $"{hostingEnvironment.WebRootPath}\\excelFiles\\{file.FileName}";

            var result = _excelRepository.GetExcelRow(file, fileFullPath);
            return View(result.Result);
        }       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var requestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            var exceptionMessage = string.Empty;
            
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();            
            
            _logger.LogInformation(exceptionHandlerPathFeature?.Error.ToString(),  DateTime.UtcNow.ToLongTimeString());

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}