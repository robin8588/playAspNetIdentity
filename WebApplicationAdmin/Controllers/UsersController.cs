using AutoMapper;
using AutoMapper.QueryableExtensions;
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
            ViewBag.columns =KendoGridHelper.GenColumns<VMUser>();
            ViewBag.models = KendoGridHelper.GenModels<VMUser>();
            ViewBag.create = KendoGridHelper.GenCreateForm<VMUser>();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Read(Search dto)
        {
            var data = DBContext.Users.AsQueryable(); ;
                        
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
            var total = await data.CountAsync();
            var result = await data.Skip(dto.skip).Take(dto.take).ProjectToListAsync<VMUser>();
            return Json(new { total = total, data = result });
        }

        [HttpPost]
        public async Task<ActionResult> Create(DtoUserCreate dto)
        {
            ApplicationUser user = new ApplicationUser();
            user.UserName = dto.Email;
            user.PhoneNumber = dto.PhoneNumber;
            user.Email = dto.Email;
            user.LockoutEnabled = true;
            user.PasswordHash = new PasswordHasher().HashPassword("123456");
            user.SecurityStamp = Guid.NewGuid().ToString();
            user.RegistrationDate = DateTime.Now;
            DBContext.Users.Add(user);
            await DBContext.SaveChangesAsync();
            return Json(new { });
        }

        [HttpPost]
        public async Task<ActionResult> Update(DtoUserUpdate dto)
        {
            var user =await DBContext.Users.FirstOrDefaultAsync(v=>v.Id==dto.Id);
            Mapper.Map(dto, user);
            DBContext.Entry(user).State = EntityState.Modified;
            await DBContext.SaveChangesAsync();
            return Json(Mapper.Map<VMUser>(user));
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