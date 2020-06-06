using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PUC.LDSI.Domain.Exception;
using PUC.LDSI.MVC.Models;
using System.Diagnostics;

namespace PUC.LDSI.MVC.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            var errorViewModel = new ErrorViewModel() { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };

            if (exceptionFeature.Error is DomainException) {
                errorViewModel.ErrorTitle = exceptionFeature.Error.Message;

                errorViewModel.Errors = (exceptionFeature.Error as DomainException).Erros;
            }
            else {
                var ex = exceptionFeature.Error;

                while (ex.InnerException != null) ex = ex.InnerException;

                errorViewModel.Trace = ex.ToString();

                errorViewModel.Errors = new string[] { ex.Message };

                errorViewModel.ErrorTitle = "Ocorreu um erro inesperado!";
            }

            return View(errorViewModel);
        }
    }
}
