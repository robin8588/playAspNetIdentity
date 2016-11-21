using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebCore;

namespace WebApplicationAdmin.Dtos
{
    public class DtoUser
    {
        [Key]
        [KendoColumn(title: "ID", width: "100",hidden:true)]
        [KendoFieldArrtibute(type:"string",editable:false)]
        public string Id { get; set; }
        [KendoColumn(title: "用户名", width: "100")]
        [KendoFieldArrtibute(type: "string", editable: false)]
        public string UserName { get; set; }
        [KendoColumn(title: "手机号", width: "100")]
        [KendoFieldArrtibute(type: "string")]
        public string PhoneNumber { get; set; }
        [KendoColumn(title: "电子邮件", width: "100")]
        [KendoFieldArrtibute(type: "string")]
        public string Email { get; set; }
     }

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
    }

    public class DtoUserDestory
    {
        [Required]
        public string Id { get; set; }
    }
}