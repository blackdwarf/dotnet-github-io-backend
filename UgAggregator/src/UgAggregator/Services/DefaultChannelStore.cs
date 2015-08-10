using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UgAggregator.Channels;
using UgAggregator.Contracts;
using UgAggregator.Models;

namespace UgAggregator.Services
{
    public class DefaultChannelStore : IChannelStore
    {


        public DefaultChannelStore(IHttpService transport, CommunityMegaphoneConfiguration cmConf, MeetupConfiguration mConf) {
            RegisteredChannels = new List<IChannel> {
                new CommunityMegaphoneChannel(transport, cmConf),
                new MeetupChannel(transport, mConf)
            };
        }
        public IList<IChannel> RegisteredChannels { get; set; }
    }
}
