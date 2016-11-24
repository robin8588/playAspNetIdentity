using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WebCore
{
    public class KendoGridHelper
    {
        public static string GenColumns<T>()
        {
            JArray columns = new JArray();
            foreach (var m in typeof(T).GetProperties())
            {
                JObject column = new JObject();
                var title = m.GetCustomAttribute<DisplayNameAttribute>();
                column.Add("field", m.Name);
                column.Add("title", title.DisplayName);

                var format = m.GetCustomAttribute<DisplayFormatAttribute>();
                if (format!= null)
                {
                    column.Add("format", format.DataFormatString);
                }
                columns.Add(column);
            }

            var command = new JObject();
            command.Add("command", new JArray("edit", "destroy"));
            command.Add("title", "&nbsp;");
            columns.Add(command);
            return columns.ToString() ;
        }

        public static string GenModels<T>()
        {
            JObject models = new JObject();
            JObject fields = new JObject();
            foreach (var m in typeof(T).GetProperties())
            {
                if (m.GetCustomAttribute<KeyAttribute>() != null)
                { models.Add("id", m.Name); }
                var field = GenFeild(m);
                fields.Add(m.Name, field);
            }
            models.Add("fields", fields);
            return models.ToString();
        }

        private static JObject GenFeild(PropertyInfo p)
        {
            JObject j = new JObject();

            foreach (var a in p.GetCustomAttributes())
            {
                switch (a.GetType().FullName)
                {
                    case "System.ComponentModel.DataAnnotations.DataTypeAttribute":
                        j.Add("type", (a as DataTypeAttribute).CustomDataType);
                        break;
                    case "System.ComponentModel.DataAnnotations.EditableAttribute":
                        j.Add("editable", (a as EditableAttribute).AllowEdit);
                        break;
                    case "System.ComponentModel.DefaultValueAttribute":
                        j.Add("defaultValue", new JValue((a as DefaultValueAttribute).Value));
                        break;
                    case "System.ComponentModel.DataAnnotations.RequiredAttribute":
                        JObject r = new JObject();
                        r.Add("required", true);
                        j.Add("validation", r);
                        break;
                    default:
                        break;
                }
            }
            return j;
        }
    }
}
