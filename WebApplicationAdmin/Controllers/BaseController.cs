using IdentityCore;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace WebApplicationAdmin.Controllers
{
    public class BaseController : Controller
    {
        public ApplicationDbContext _db;

        public ApplicationDbContext DBContext
        {
            get
            {
                return _db ?? HttpContext.GetOwinContext().Get<ApplicationDbContext>();
            }
            private set
            {
                _db = value;
            }
        }

        public BaseController()
        {

        }

        public BaseController(ApplicationDbContext db)
        {
            _db = db;
        }

        protected string GetModelStateText(ModelStateDictionary modelState)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var v in this.ModelState.Values)
            {
                foreach (ModelError e in v.Errors)
                {
                    sb.AppendLine(e.ErrorMessage);
                }
            }
            return sb.ToString();
        }

        protected virtual void CheckModelState()
        {
            if (!ModelState.IsValid)
            {
                throw new Exception(GetModelStateText(this.ModelState));
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            CheckModelState();
            base.OnActionExecuting(filterContext);
        }

        protected override void OnException(ExceptionContext context)
        {
            context.ExceptionHandled = true;
            context.HttpContext.Response.Clear();
            context.Result = Json(new { error = context.Exception.Message }, JsonRequestBehavior.AllowGet);
            context.HttpContext.Response.StatusCode = 400;
            context.HttpContext.Response.TrySkipIisCustomErrors = true;
        }
    }
}