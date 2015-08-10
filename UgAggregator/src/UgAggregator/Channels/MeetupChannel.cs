using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Framework.Caching.Memory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UgAggregator.Contracts;
using UgAggregator.Models;
using UgAggregator.Services;

namespace UgAggregator.Channels
{
    public class MeetupChannel : IChannel {

        private readonly IHttpService _transport;
        private readonly MeetupConfiguration _conf;
        private readonly IMemoryCache _cache;

        public MeetupChannel(IHttpService t, MeetupConfiguration c) {
            _transport = t;
            _conf = c;
            _cache = new MemoryCache(new MemoryCacheOptions());
        }

        public async Task<IEnumerable<UgEvent>> GetEvents(int count = 25) {
            var meetups = new List<UgEvent>();
            var meetupUrl = string.Format(_conf.Url, _conf.ApiKey, count);
            // TODO check cache here...extension method?
            var meetupJson = await _transport.GetAsync(meetupUrl);
            var meetupObject = JsonConvert.DeserializeObject<JObject>(meetupJson);

            // TODO decide where to put in the code to store stuff in the 
            // cache...

            Func<JToken, string> getValue = (j) => {
                return j == null ? null : j.ToString();
            };

            foreach (var mObj in meetupObject["results"]) {
                var m = new UgEvent();
                meetups.Add(m);

                m.Description = getValue(mObj["description"]);
                m.Name = getValue(mObj["name"]);
                m.EventUrl = getValue(mObj["event_url"]);
                var group = mObj["group"];
                if (group != null) {
                    m.GroupName = getValue(group["name"]);
                    m.GroupUrl = String.Format("http://meetup.com/{0}", getValue(group["urlname"]));
                    m.EventSource = "meetup.com";
                }

                DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0);
                m.EventDateTime = epoch.AddMilliseconds((double)mObj["time"]);
            }
            return meetups;
        }
    }
}
