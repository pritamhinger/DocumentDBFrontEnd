using DocumentDBFrontEnd.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace DocumentDBFrontEnd.Controllers.api
{
    public class LogController : ApiController
    {
        TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public async Task<HttpResponseMessage> GetAsync(string customerName)
        {
            customerName = textInfo.ToTitleCase(customerName);
            var items = await DocumentDBRepository<Log>.GetItemsAsyncAll(d => d.CustomerName == customerName, d => d.ActivityDateTimeTicks);
            return Request.CreateResponse(HttpStatusCode.OK, items);
        }

        public string Get(string customerName, int size) {
            return customerName  + " with size " + size;
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int customerName, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int customerName)
        {
        }
    }
}