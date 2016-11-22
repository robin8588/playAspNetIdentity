using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

    public class KendoFieldArrtibute : Attribute
    {
        public string Type
        {
            get;
            private set;
        }

        public bool Editable
        {
            get;
            private set;
        }

        public KendoFieldArrtibute(string type, bool editable = true)
        {
            this.Type = type;
            this.Editable = editable;
        }
    }

    public class Column
    {
        public string field { get; set; }
        public string title { get; set; }
        public string width { get; set; }
        public bool hidden { get; set; }
    }

    public class Sort
    {
        public string field { get; set; }
        public string dir { get; set; }
    }

    public class Filter
    {
        public string logic { get; set; }
        public List<FilterItem> filters { get; set; }
    }

    public class FilterItem
    {
        public string Field { get; set; }
        public Op Operator { get; set; }
        public string Value { get; set; }
    }

    public class Search
    {
        public int take { get; set; }
        public int skip { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
        public List<Sort> sort { get; set; }
        public Filter filter { get; set; }
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

        public static string GenModels<T>()
        {
            JObject models = new JObject();
            JObject fields = new JObject();
            foreach (var m in typeof(T).GetProperties())
            {
                if (m.GetCustomAttribute<KeyAttribute>() != null)
                { models.Add("id", m.Name); }
                JObject field = new JObject();

                var a = m.GetCustomAttribute<KendoFieldArrtibute>();
                field.Add("type", a.Type);
                field.Add("editable", a.Editable);
                fields.Add(m.Name, field);
            }
            models.Add("fields", fields);
            return models.ToString();
        }
    }
}
