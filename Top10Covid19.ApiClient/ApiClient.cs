using System;
using System.Collections.Specialized;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using System.Web;

namespace Top10Covid19.ApiClient
{
    public partial class ApiClient
    {
        private Uri BaseApiUrl { get; set; }
        private string ApiKey { get; set; }
        private string ApiHost { get; set; }

		public ApiClient(Uri baseApiUrl, string apiKey, string apiHost)
        {
			BaseApiUrl = baseApiUrl ?? throw new ArgumentNullException(nameof(baseApiUrl));
			ApiKey = apiKey;
			ApiHost = apiHost;
        }

		private async Task<T> GetAsync<T>(string method, NameValueCollection queryStrings = null)
		{
			string urlServer = BaseApiUrl + method;

			var builder = new UriBuilder(urlServer);

			if (queryStrings is not null)
			{
				var query = HttpUtility.ParseQueryString(builder.Query);
				query.Add(queryStrings);
				builder.Query = query.ToString();
			}
			string url = builder.ToString();


			var client = new HttpClient();
			var request = new HttpRequestMessage
			{
				Method = HttpMethod.Get,
				RequestUri = new Uri(url),
				Headers =
			{
				{ "x-rapidapi-key", ApiKey },
				{ "x-rapidapi-host", ApiHost },
			},
			};

			var settings = new DataContractJsonSerializerSettings
			{
				DateTimeFormat = new System.Runtime.Serialization.DateTimeFormat("yyyy-MM-dd'T'HH:mm:ss")
			};

            using var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var resultBody = await response.Content.ReadAsStreamAsync();
            var serializer = new DataContractJsonSerializer(typeof(T), settings);
            var returnObject = (T)serializer.ReadObject(resultBody);
            return returnObject;
        }

	}
}
