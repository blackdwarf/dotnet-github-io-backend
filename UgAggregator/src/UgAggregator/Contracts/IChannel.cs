using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UgAggregator.Models;

namespace UgAggregator.Contracts
{
    public interface IChannel
    {
        Task<IEnumerable<UgEvent>> GetEvents(int count = 25);
    }
}
