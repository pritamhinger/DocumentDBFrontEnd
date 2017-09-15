using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Threading.Tasks;
using DocumentDBFrontEnd.Models;

namespace DocumentDBFrontEnd.Controllers
{
    public class LogController : Controller
    {
        // GET: Log Async
        [ActionName("Index")]
        public async Task<ActionResult> IndexAsync()
        {
            var customerName = "Microsoft";
            int maxCount = 10;
            if (Request.QueryString["customerName"] != null)
            {
                customerName = Request.QueryString["customerName"].ToString();
            }

            if(Request.QueryString["maxCount"] != null)
            {
                int.TryParse(Request.QueryString["maxCount"].ToString(), out maxCount);
            }

            var items = await DocumentDBRepository<Log>.GetItemsAsync(d => d.CustomerName == customerName, d => d.ActivityDateTimeTicks, maxCount);
            return View(items);
        }
    }
}