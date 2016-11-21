using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WebCore
{
    public class KendoColumnAttribute : Attribute
    {
        public string Title
        {
            get;
            private set;
        }

        public string Width
        {
            get;
            private set;
        }

        public bool Hidden
        {
            get;
            private set;
        }

        public KendoColumnAttribute(string title, string width, bool hidden = false)
        {
            this.Title = title;
            this.Width = width;
            this.Hidden = hidden;
        }
    }

    public class Column
    {
        public string field { get; set; }
        public string title { get; set; }
        public string width { get; set; }
        public bool hidden { get; set; }
    }

    public class KendoColumnHelper
    {
        public static List<Column> GenColumns<T>()
        {
            var cs = new List<Column>();
            foreach (var m in typeof(T).GetProperties())
            {
                var a = m.GetCustomAttribute<KendoColumnAttribute>();
                Column c = new Column();
                c.field = m.Name;
                c.title = a.Title;
                c.width = a.Width;
                c.hidden = a.Hidden;
                cs.Add(c);
            }
            return cs;
        }
    }
}
