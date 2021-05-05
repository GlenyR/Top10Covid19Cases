using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Top10Covid19.ApiClient;
using Top10Covid19.ApiClient.Utilities;

namespace Top10Covid19.Services.Factories
{
    internal static class ApiClientFactory
    {
        private static Uri BaseApiUrl { get; set; }
        private static string ApiKey { get; set; }
        private static string ApiHost { get; set; }

        private static readonly Lazy<ApiClient.ApiClient> restClient = new(
          () => new ApiClient.ApiClient(BaseApiUrl, ApiKey, ApiHost),
          LazyThreadSafetyMode.ExecutionAndPublication);

        static ApiClientFactory()
        {
            BaseApiUrl = new Uri(ApiSettings.BaseApiUrl);
            ApiKey = ApiSettings.ApiKey;
            ApiHost = ApiSettings.ApiHost;
        }

        public static ApiClient.ApiClient Instance
        {
            get
            {
                return restClient.Value;
            }
        }
    }
}
