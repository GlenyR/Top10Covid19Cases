using System;
using System.Collections.Specialized;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Top10Covid19.ApiClient.Models;

namespace Top10Covid19.ApiClient
{
    public partial class ApiClient
    {
        public async Task<Reports> GetTop10Regions()
        {
            return await GetAsync<Reports> ("reports");
        }

        public async Task<Reports> GetTop10ProvincesByIsoRegion(string isoRegion)
        {
            var queryStrings = new NameValueCollection
            {
                { "iso", isoRegion }
            };
            return await GetAsync<Reports>("reports", queryStrings);
        }

    }
}
