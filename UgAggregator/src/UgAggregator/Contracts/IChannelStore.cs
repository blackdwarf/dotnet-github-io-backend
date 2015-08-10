using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UgAggregator.Contracts
{
    public interface IChannelStore
    {
        IList<IChannel> RegisteredChannels { get; set; }
    }
}
