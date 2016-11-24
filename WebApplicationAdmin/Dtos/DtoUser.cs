using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplicationAdmin.Dtos
{
    public class DtoUserCreate
    {
        [Required]
        [RegularExpression("^1[3-8]+\\d{9}$", ErrorMessage = "手机号码格式错误")]
        public string PhoneNumber { get; set; }
        [Required]
        [RegularExpression(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", ErrorMessage = "邮箱格式错误")]
        public string Email { get; set; }
    }

    public class DtoUserUpdate
    {
        [Required]
        public string Id { get; set; }
        [Required]
        [RegularExpression("^1[3-8]+\\d{9}$", ErrorMessage = "手机号码格式错误")]
        public string PhoneNumber { get; set; }

        public bool LockoutEnabled { get; set; }
    }

    public class DtoUserDestory
    {
        [Required]
        public string Id { get; set; }
    }
}