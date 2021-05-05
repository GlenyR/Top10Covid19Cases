using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Top10Covid19.Services.Factories;
using Top10Covid19.Services.Models;

namespace Top10Covid19.Services.Services
{
    public class RegionService : IRegionService
    {
        public async Task<List<RegionModel>> GetAllAsync()
        {
            var regions = await ApiClientFactory.Instance.GetAllRegionsAsync();

            return regions.Data.OrderBy(o => o.Name).Select(s => new RegionModel(s.Name, s.Iso)).ToList();

        }
    }
}
