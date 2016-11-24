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
using WebApplicationAdmin.VMs;
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
            ViewBag.columns =KendoColumnHelper.GenColumns<VMUser>();
            ViewBag.models = KendoColumnHelper.GenModels<VMUser>();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Read(Search dto)
        {
            var data = (from n in DBContext.Users
                        select new VMUser
                        {
                            Id = n.Id,
                            UserName = n.UserName,
                            Email = n.Email,
                            EmailConfirmed = n.EmailConfirmed,
                            PhoneNumber = n.PhoneNumber,
                            RegistrationDate = n.RegistrationDate,
                            LockoutEnabled = n.LockoutEnabled,
                            AccessFailedCount = n.AccessFailedCount
                        });
            if (dto.sort!= null && dto.sort.Count > 0)
            {
                var s = dto.sort.FirstOrDefault();
                data =data.OrderByField(s.field ,s.dir);
            }else
            {
                data = data.OrderByField("Id", "desc");
            }
            if(dto.filter !=null && dto.filter.filters.Count() > 0)
            {
                var v = dto.filter.filters.FirstOrDefault();
                data = data.FilterByField(dto.filter.filters);
            }
            data = data.Skip(dto.skip).Take(dto.take);
            return Json(new { total = await data.CountAsync(), data = await data.ToListAsync() });
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
            user.RegistrationDate = DateTime.Now;
            DBContext.Users.Add(user);
            await DBContext.SaveChangesAsync();
            VMUser created = new VMUser();
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
            user.LockoutEnabled = dto.LockoutEnabled;
            DBContext.Entry(user).State = EntityState.Modified;
            await DBContext.SaveChangesAsync();
            return Json(new VMUser()
            {
                AccessFailedCount = user.AccessFailedCount,
                Email = user.Email,
                Id = user.Id,
                LockoutEnabled = user.LockoutEnabled,
                PhoneNumber = user.PhoneNumber,
                RegistrationDate = user.RegistrationDate,
                UserName = user.UserName,
                EmailConfirmed = user.EmailConfirmed
            });
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