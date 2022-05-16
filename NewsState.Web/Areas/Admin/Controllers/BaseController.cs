using Microsoft.AspNetCore.Mvc;

namespace NewsState.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin")]
    public class BaseController : Controller
    {
        protected string ErrorMessage = "ErrorMessage";
        protected string InfoMessage = "InfoMessage";
        protected string SuccessMessage = "SuccessMessage";
        protected string WarningMessage = "WarningMessage";
    }
}
