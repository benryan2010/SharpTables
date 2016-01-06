using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpTablesCore.DataAnnotations
{
    public class SharpTable : Attribute
    {
        public string ColumnLabel { get; set; }
        public int ColumnIndex { get; set; }
        public bool IsFilterable { get; set; }
        public bool IsSortable { get; set; }
    }
}
