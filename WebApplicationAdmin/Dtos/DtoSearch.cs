using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationAdmin.Dtos
{
    public class DtoSearch
    {
        public int take { get; set; }
        public int skip { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
    }
}