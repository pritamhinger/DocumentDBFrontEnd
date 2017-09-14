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
            if (Request.QueryString["customerName"] != null)
            {
                customerName = Request.QueryString["customerName"].ToString();
            }

            var items = await DocumentDBRepository<Log>.GetItemsAsync(d => d.CustomerName == customerName);
            return View(items);
        }
    }
}