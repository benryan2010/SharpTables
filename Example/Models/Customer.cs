using SharpTablesCore.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Example.Models
{
    public class Customer
    {
        [SharpTable(ColumnLabel="Customer ID Yeah!", ColumnIndex = 0, IsFilterable = true, IsSortable = true)]
        public int CustomerID { get; set; }

        [SharpTable(ColumnLabel = "Customer Name", ColumnIndex = 2, IsFilterable = true, IsSortable = true)]
        public string CustomerName { get; set; }

        [SharpTable(ColumnLabel = "Customer Address", ColumnIndex = 1, IsFilterable = true, IsSortable = true)]
        public string Address { get; set; }

    }
}