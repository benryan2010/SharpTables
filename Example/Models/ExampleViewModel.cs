using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SharpTablesCore;
using System.ComponentModel.DataAnnotations;

namespace Example.Models
{
    public class ExampleViewModel
    {
        [UIHint("SharpTable")]
        public SharpTable<Customer> Table1 { get; set; }

        [UIHint("SharpTable")]
        public SharpTable<Customer> Table2 { get; set; }
    }
}