using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.ComponentModel.DataAnnotations.Extension
{

    public class NullableAttribute : Attribute
    {
        public bool Nullable { get; set; }

        public NullableAttribute(bool nullable)
        {
            this.Nullable = nullable;
        }
    }

    public class HiddenAttribute : Attribute
    {
        public bool Hidden { get; set; }
        public HiddenAttribute(bool hidden)
        {
            this.Hidden = hidden;
        }
    }

    public class ColumnWidthAttribute : Attribute
    {
        public string Width { get; set; }
        public ColumnWidthAttribute(string width)
        {
            this.Width = width;
        }
    }

    public class CreatableAttribute:Attribute
    {
        public CreatableAttribute()
        {

        }
    }
}
