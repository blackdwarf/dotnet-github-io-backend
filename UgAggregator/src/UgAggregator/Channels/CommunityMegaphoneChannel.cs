using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UgAggregator.Contracts;
using UgAggregator.Models;
using UgAggregator.Services;

namespace UgAggregator.Channels
{
    public class CommunityMegaphoneChannel : IChannel
    {
        private readonly IHttpService _transport;
        private readonly CommunityMegaphoneConfiguration _conf;


        public CommunityMegaphoneChannel(IHttpService t, CommunityMegaphoneConfiguration c)
        {
            _transport = t;
            _conf = c;
        }


        public async Task<IEnumerable<UgEvent>> GetEvents(int count = 25) {
            var meetups = new List<UgEvent>();

            _transport.AddHeader("Accept", "application/json");
            //var resultsJson = await _transport.GetAsync("http://communitymegaphone.com/ws/CMEventDS.svc/ApprovedEvents");
            var resultsJson = await _transport.GetAsync(_conf.Url);
            var resultsObject = JsonConvert.DeserializeObject<JObject>(resultsJson);


            Func<JToken, string> getValue = (j) => {
                return j == null ? null : j.ToString();
            };

            foreach (var mObj in resultsObject["d"]) {
                var m = new UgEvent();
                meetups.Add(m);

                m.Description = getValue(mObj["description"]);
                m.Name = getValue(mObj["title"]);
                m.EventUrl = getValue(mObj["eventUrl"]);
                m.GroupUrl = getValue(mObj["eventUrl"]);
                m.EventDateTime = DateTime.Parse(mObj["starttime"].ToString());
                m.EventSource = "communitymegaphone.com";
            }

            return meetups;
        }
    }
}
