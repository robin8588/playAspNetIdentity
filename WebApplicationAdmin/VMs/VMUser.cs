using AutoMapper.Attributes;
using IdentityCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplicationAdmin.VMs
{
    [MapsFrom(typeof(ApplicationUser))]
    public class VMUser
    {
        [Key]
        [Editable(false)]
        [DataType("string")]
        [DisplayName("ID")]
        public string Id { get; set; }

        [Editable(false)]
        [DataType("string")]
        [DisplayName("用户名")]
        public string UserName { get; set; }

        [Required]
        [Editable(false)]
        [DataType("string")]
        [DisplayName("电子邮箱")]
        public string Email { get; set; }


        [Editable(false)]
        [DataType("boolean")]
        [DisplayName("邮箱确认")]
        public bool EmailConfirmed { get; internal set; }

        [Editable(true)]
        [DataType("string")]
        [DisplayName("手机号")]
        public string PhoneNumber { get; set; }

        [Editable(false)]
        [DataType("date")]
        [DisplayName("注册日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime RegistrationDate { get; set; }

        [Editable(false)]
        [DataType("number")]
        [DisplayName("登录失败")]
        public int AccessFailedCount { get; set; }

        [Editable(true)]
        [DefaultValue(false)]
        [DataType("boolean")]
        [DisplayName("锁定功能")]
        public bool LockoutEnabled { get; set; }
    }
}