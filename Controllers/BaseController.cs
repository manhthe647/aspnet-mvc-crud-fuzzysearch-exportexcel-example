using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace QuanLySinhVien.WebAppMvc.Controllers
{
    public class BaseController: Controller
    {
        [Authorize]
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var sessions = context.HttpContext.Session.GetString("Token");
            if (sessions == null)
            {
                context.Result = new RedirectToActionResult("Login", "User", null);
            }
            base.OnActionExecuting(context);
        }
    }
}
