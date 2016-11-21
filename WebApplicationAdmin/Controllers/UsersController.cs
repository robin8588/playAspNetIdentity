using IdentityCore;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplicationAdmin.Dtos;
using WebCore;

namespace WebApplicationAdmin.Controllers
{
    public class UsersController : Controller
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

        public UsersController()
        {
        }

        public UsersController(ApplicationDbContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            ViewBag.column =JsonConvert.SerializeObject(KendoColumnHelper.GenColumns<DtoUser>());
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Get(DtoSearch dto)
        {
            var data = (from n in DBContext.Users
                       select new DtoUser
                       {
                           Id = n.Id,
                           UserName = n.UserName,
                           Email = n.Email,
                           PhoneNumber = n.PhoneNumber
                       }).OrderBy(v => v.UserName).Skip(dto.skip).Take(dto.take);

            return Json(await data.ToListAsync(),JsonRequestBehavior.AllowGet);
        }
    }
}