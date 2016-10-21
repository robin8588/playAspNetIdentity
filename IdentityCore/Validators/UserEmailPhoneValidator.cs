using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityCore.Validators
{
    public class UserEmailPhoneValidator: UserValidator<ApplicationUser,string>
    {
        private ApplicationUserManager manager;

        public UserEmailPhoneValidator(ApplicationUserManager manager):base(manager)
        {
            this.manager = manager;
        }

        public override async Task<IdentityResult> ValidateAsync(ApplicationUser item)
        {
            IdentityResult identityResult;
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            List<string> errors = new List<string>();
            await this.ValidateUserName(item, errors);
            identityResult = (errors.Count <= 0 ? IdentityResult.Success : IdentityResult.Failed(errors.ToArray()));
            return identityResult;
        }

        private async Task ValidateUserName(ApplicationUser user, List<string> errors)
        {
            if (string.IsNullOrWhiteSpace(user.UserName))
            {
                errors.Add(string.Format("用户名不能为空"));
            }
            else
            {
                ApplicationUser tUser = await manager.FindByNameAsync(user.UserName);
                if (tUser != null && tUser.Id.Equals(user.Id))
                {
                    errors.Add(string.Format("用户名{0}已经注册", user.UserName));
                }
                tUser = await this.manager.FindByEmailAsync(user.UserName);
                if (tUser != null && tUser.Id.Equals(user.Id))
                {
                    errors.Add(string.Format("邮箱{0}已经被绑定", user.UserName));
                }
                tUser = await this.manager.FindByPhoneAsync(user.UserName);
                if (tUser != null && tUser.Id.Equals(user.Id))
                {
                    errors.Add(string.Format("手机{0}已经被绑定", user.UserName));
                }
            }
        }
    }
}
