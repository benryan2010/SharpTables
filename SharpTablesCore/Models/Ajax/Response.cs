using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpTablesCore.Models.Ajax
{
    public class SimpleResponse<T>
    {
        public IList<T> data { get; set; }
    }

    public class Response<T>
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }

        public List<T> data { get; set; }
    }

}
