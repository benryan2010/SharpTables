using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using SharpTablesCore.Models.Ajax;
using System.Web.Http;

namespace SharpTablesCore.Components
{
    /// <summary>
    /// Model Binder for DTParameterModel (DataTables)
    /// </summary>
    public class RequestModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            base.BindModel(controllerContext, bindingContext);
            var request = controllerContext.HttpContext.Request;
            // Retrieve request data
            var draw = Convert.ToInt32(request["draw"]);
            var start = Convert.ToInt32(request["start"]);
            var length = Convert.ToInt32(request["length"]);
            // Search
            var search = new Search
            {
                Value = request["search[value]"],
                Regex = Convert.ToBoolean(request["search[regex]"])
            };
            // Order
            var o = 0;
            var order = new List<OrderLink>();
            while (request["order[" + o + "][column]"] != null)
            {
                order.Add(new OrderLink
                {
                    Column = Convert.ToInt32(request["order[" + o + "][column]"]),
                    Dir = request["order[" + o + "][dir]"]
                });
                o++;
            }
            // Columns
            var c = 0;
            var columns = new List<Column>();
            while (request["columns[" + c + "][name]"] != null)
            {
                columns.Add(new Column
                {
                    Data = request["columns[" + c + "][data]"],
                    Name = request["columns[" + c + "][name]"],
                    Orderable = Convert.ToBoolean(request["columns[" + c + "][orderable]"]),
                    Searchable = Convert.ToBoolean(request["columns[" + c + "][searchable]"]),
                    Search = new Search
                    {
                        Value = request["columns[" + c + "][search][value]"],
                        Regex = Convert.ToBoolean(request["columns[" + c + "][search][regex]"])
                    }
                });
                c++;
            }

            return new Request
            {
                Draw = draw,
                Start = start,
                Length = length,
                Search = search,
                Order = order,
                Columns = columns
            };
        }
    }
}