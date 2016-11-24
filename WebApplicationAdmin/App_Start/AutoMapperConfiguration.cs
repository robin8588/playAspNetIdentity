using AutoMapper;
using AutoMapper.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace WebApplicationAdmin
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {
                Assembly.GetExecutingAssembly().MapTypes(x);
            });
        }
    }
}