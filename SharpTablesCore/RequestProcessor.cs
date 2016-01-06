using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SharpTablesCore.Models.Ajax;

namespace SharpTablesCore
{
    public class RequestProcessor<T>
    {
        private List<T>  data { get; set; }
        private List<T> result { get; set; }
        private Request r { get; set; }
        private Response<T> response { get; set; }

        public RequestProcessor(List<T> Data, Request R)
        {
            this.data = Data;
            this.r = R;

            response = new Response<T>();
            response.draw = r.Draw;
            response.recordsTotal = data.Count();

            Filter();
            Sort();
            Page();
            
        }

        private void Filter()
        {
            var p = PredicateBuilder.False<T>();

            foreach (Column c in r.Columns)
                p = p.Or(i => i.GetType().GetProperty(c.Data).GetValue(i, null).ToString().Contains(r.Search.Value));

            result = data.AsQueryable().Where(p).ToList();
            response.recordsFiltered = result.Count();
        }

        private void Sort()
        {
            var order = r.Order.First();
            if(order.Dir == OrderDirection.asc.ToString())
                result = result.OrderBy(i => i.GetType().GetProperty(r.Columns[order.Column].Data).GetValue(i,null)).ToList();
            if (order.Dir == OrderDirection.desc.ToString())
                result = result.OrderByDescending(i => i.GetType().GetProperty(r.Columns[order.Column].Data).GetValue(i,null)).ToList();
        }

        private void Page()
        {
            result = result.Skip(r.Start).Take(r.Length).ToList();
            response.data = result;
        }

        public Response<T> BuildResponse()
        {
            return response;
        }
    }
}
