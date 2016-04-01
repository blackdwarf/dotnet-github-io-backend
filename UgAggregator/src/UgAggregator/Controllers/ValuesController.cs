using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UgAggregator.Contracts;
using UgAggregator.Models;

namespace UgAggregator.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController
    {

        private readonly IChannelStore _store;

        public ValuesController(IChannelStore s) {
            _store = s;
        }

        // GET: api/values
        [HttpGet]
        public async Task<IEnumerable<UgEvent>> Get()
        {
            List<Task<IEnumerable<UgEvent>>> tasks = new List<Task<IEnumerable<UgEvent>>>();

            foreach (var channel in _store.RegisteredChannels) {
                tasks.Add(channel.GetEvents(25));
            }
            var agmeets = await Task.WhenAll(tasks);
            return agmeets.SelectMany(m => m).OrderBy(m => m.EventDateTime).Take(25);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
