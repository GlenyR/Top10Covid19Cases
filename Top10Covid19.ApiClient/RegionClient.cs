using System.Threading.Tasks;
using Top10Covid19.ApiClient.Models;

namespace Top10Covid19.ApiClient
{
    public partial class ApiClient
    {
        public async Task<Regions> GetAllRegionsAsync()
        {
            return await GetAsync<Regions> ("regions");
        }
    }
}
