using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Extension;
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
                columns.Add(BuildColumn(m));
            }

            var command = new JObject();
            command.Add("command", new JArray("edit", "destroy"));
            command.Add("title", "&nbsp;");
            columns.Add(command);
            return columns.ToString() ;
        }

        private static JObject BuildColumn(PropertyInfo p)
        {
            JObject column = new JObject();
            column.Add("field", p.Name);
            foreach (var a in p.GetCustomAttributes())
            {
                switch (a.GetType().FullName)
                {
                    case "System.ComponentModel.DisplayNameAttribute":
                        column.Add("title", ((DisplayNameAttribute)a).DisplayName);
                        break;
                    case "System.ComponentModel.DataAnnotations.DisplayFormatAttribute":
                        column.Add("format", ((DisplayFormatAttribute)a).DataFormatString);
                        break;
                    case "System.ComponentModel.DataAnnotations.Extension.HiddenAttribute":
                        column.Add("hidden", ((HiddenAttribute)a).Hidden);
                        break;
                    case "System.ComponentModel.DataAnnotations.Extension.ColumnWidthAttribute":
                        column.Add("width", ((ColumnWidthAttribute)a).Width);
                        break;
                    default:
                        break;
                }
            }
            return column;
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

        public static string GenCreateForm<T>()
        {
            JObject schema = new JObject();
            
            foreach (var m in typeof(T).GetProperties())
            {
                var c = m.GetCustomAttribute<CreatableAttribute>();
                if (c != null)
                {
                    schema.Add(m.Name, GenCreateInput(m));
                }
            }
            return schema.ToString();
        }

        private static JObject GenCreateInput(PropertyInfo p)
        {
            JObject property = new JObject();

            foreach (var a in p.GetCustomAttributes())
            {
                switch (a.GetType().FullName)
                {
                    case "System.ComponentModel.DisplayNameAttribute":
                        property.Add("title", ((DisplayNameAttribute)a).DisplayName);
                        break;
                    case "System.ComponentModel.DataAnnotations.DataTypeAttribute":
                        property.Add("type", (a as DataTypeAttribute).CustomDataType);
                        break;
                    case "System.ComponentModel.DataAnnotations.RequiredAttribute":
                        property.Add("required", true);
                        break;
                    default:
                        break;
                }
            }
            return property;
        }
    }
}
