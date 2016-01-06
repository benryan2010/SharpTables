using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpTablesCore.TableComponents
{
    public class Column
    {
        public int ColumnID { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public bool IsSearchable { get; set; }
        public bool IsOrderable { get; set; }
        //public Search search { get; set; }
    }

    public class RequestColumn
    {
        public string data { get; set; }
        public string name { get; set; }
        public string searchable { get; set; }
        public string orderable { get; set; }
    }

    class ColumnDefinitionViewModel
    {
        public string name { get; set; }
        public string targets { get; set; }
    }
}
