using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UgAggregator.Contracts;

namespace UgAggregator.Services
{
    public class HttpService : IHttpService {

        private readonly HttpClient _client;

        public HttpService() {
            _client = new HttpClient();
        }

        public void AddHeader(string name, string value) {
            _client.DefaultRequestHeaders.Add(name, value);
        }

        public void AddHeaders(Dictionary<string, string> headers) {
            throw new NotImplementedException();
        }

        public async Task<string> GetAsync(string url) {
            return await _client.GetStringAsync(url);
        }

        public string Get(string url) {
            throw new NotImplementedException();
        }

        public void Post(string url, string data) {
            throw new NotImplementedException();
        }

        public void PostAsync(string url, string data) {
            throw new NotImplementedException();
        }
    }
}
