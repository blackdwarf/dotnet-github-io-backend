﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UgAggregator.Channels;
using UgAggregator.Contracts;
using UgAggregator.Models;
using UgAggregator.Services;

namespace HttpProxyWebRole.Controllers {
    [Route("api/[controller]")]
    public class UgController : Controller {
        HttpClient _client = new HttpClient();
        private readonly IChannelStore _store;


        public UgController(IChannelStore s) {
            _store = s;
        }

        // GET api/meetup
        public async Task<IEnumerable<UgEvent>> Get(int count = 25) {

            //var cmEvents = _cmChannel.GetEvents(count);
            //var mEvents = _meetupChannel.GetEvents(count);
            //var agMeetups = await Task.WhenAll(cmEvents, mEvents);

            //var meetups = agMeetups.SelectMany(m => m).OrderBy(m => m.EventDateTime).Take(count);
            //return meetups;

            List<Task<IEnumerable<UgEvent>>> tasks = new List<Task<IEnumerable<UgEvent>>>();

            foreach (var channel in _store.RegisteredChannels) {
                    tasks.Add(channel.GetEvents(count));
            }
            var agmeets = await Task.WhenAll(tasks);
            return agmeets.SelectMany(m => m).OrderBy(m => m.EventDateTime).Take(count);

        }
    }
}