using IdentityCore;
using Microsoft.AspNet.Identity;
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
    public class UsersController : BaseController
    {
        public UsersController()
        {

        }

        public UsersController(ApplicationDbContext db):base(db)
        {
        }

        public ActionResult Index()
        {
            ViewBag.columns =JsonConvert.SerializeObject(KendoColumnHelper.GenColumns<DtoUser>());
            ViewBag.models = KendoColumnHelper.GenModels<DtoUser>();
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Read(Search dto)
        {
            var data = (from n in DBContext.Users
                       select new DtoUser
                       {
                           Id = n.Id,
                           UserName = n.UserName,
                           Email = n.Email,
                           PhoneNumber = n.PhoneNumber
                       }).OrderBy(v=>v.Id).Skip(dto.skip).Take(dto.take);
            return Json(new { total = await data.CountAsync(), data = await data.ToListAsync() },JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> Create(DtoUserCreate dto)
        {
            ApplicationUser user = new ApplicationUser();
            user.UserName = dto.Email;
            user.PhoneNumber = dto.PhoneNumber;
            user.Email = dto.Email;
            user.PasswordHash = new PasswordHasher().HashPassword("123456");
            user.SecurityStamp = Guid.NewGuid().ToString();
            DBContext.Users.Add(user);
            await DBContext.SaveChangesAsync();
            DtoUser created = new DtoUser();
            created.Id = user.Id;
            created.UserName = user.UserName;
            created.PhoneNumber = user.PhoneNumber;
            created.Email = user.Email;
            return Json(created);
        }

        [HttpPost]
        public async Task<ActionResult> Update(DtoUserUpdate dto)
        {
            var user =await DBContext.Users.FirstOrDefaultAsync(v=>v.Id==dto.Id);
            user.PhoneNumber = dto.PhoneNumber;
            DBContext.Entry(user).State = EntityState.Modified;
            await DBContext.SaveChangesAsync();
            DtoUser created = new DtoUser();
            created.Id = user.Id;
            created.UserName = user.UserName;
            created.PhoneNumber = user.PhoneNumber;
            created.Email = user.Email;
            return Json(created);
        }

        [HttpPost]
        public async Task<ActionResult> Destroy(DtoUserDestory dto)
        {
            var user = await DBContext.Users.FirstOrDefaultAsync(v => v.Id == dto.Id);
            DBContext.Users.Remove(user);
            await DBContext.SaveChangesAsync();
            return Json(new { });
        }
    }
}