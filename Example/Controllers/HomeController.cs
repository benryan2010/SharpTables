using Example.Models;
using SharpTablesCore;
using SharpTablesCore.Models.Ajax;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Example.Controllers
{
    public class HomeController : Controller
    {
        private FakeDbContext db = new FakeDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Examples()
        {
            var vm = new ExampleViewModel();
           
            vm.Table1 = new SharpTable<Customer>(db.Customers, new TableConfig
            {
                TableID = "Table1",
                DataConfig = new DataConfiguration
                {
                    IsAjaxData = false
                }
            });

            vm.Table2 = new SharpTable<Customer>(typeof(Customer), new TableConfig
            {
                TableID = "Table2",
                DataConfig = new DataConfiguration
                {
                    IsAjaxData = true,
                    IsServerSide = true,
                    Action = ActionType.POST,
                    URL = "/Home/GetData"
                }
            });

            return View(vm);
        }

        [HttpGet]
        public ActionResult GetData()
        {
            SimpleResponse<Customer> r = new SimpleResponse<Customer>();
            return Json(db.Customers, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetData(Request r)
        {
            RequestProcessor<Customer> p = new RequestProcessor<Customer>(db.Customers, r);
            return Json(p.BuildResponse(), JsonRequestBehavior.AllowGet);
        }
    }

    public class FakeDbContext
    {
        public List<Customer> Customers {get;set;}

        public FakeDbContext()
        {
            Customers = new List<Customer>();
            for (var i = 1; i < 101; i++)
            {
                Customers.Add(new Customer { CustomerID = i, CustomerName = "Customer " + i, Address = i + " Main Street" });
            }
        }

        public IQueryable<Customer> GetCustomers()
        {
            return Customers.AsQueryable();
        }
    }
}