using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using WebCore;

namespace WebApplicationAdmin.Dtos
{
    public class DtoUser
    {
        [KendoColumn(title: "ID", width: "100px",hidden:true)]
        public string Id { get; set; }
        [KendoColumn(title: "用户名", width: "100px")]
        public string UserName { get; set; }
        [KendoColumn(title: "手机号", width: "100px")]
        public string PhoneNumber { get; set; }
        [KendoColumn(title: "电子邮件", width: "100px")]
        public string Email { get; set; }
     }
}