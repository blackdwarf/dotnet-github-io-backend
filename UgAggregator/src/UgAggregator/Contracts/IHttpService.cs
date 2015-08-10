using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UgAggregator.Contracts
{
    public interface IHttpService
    {
        Task<string> GetAsync(string url);
        string Get(string url);

        void PostAsync(string url, string data);
        void Post(string url, string data);

        void AddHeader(string name, string value);

        void AddHeaders(Dictionary<string, string> headers);

    }
}
