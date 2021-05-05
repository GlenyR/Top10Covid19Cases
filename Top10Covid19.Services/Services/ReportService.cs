using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Top10Covid19.Services.Factories;
using Top10Covid19.Services.Models;

namespace Top10Covid19.Services.Services
{
    public class ReportService : IReportService
    {

        public async Task<List<ReportModel>> GetTop10ProvincesByIsoRegion(string isoRegion)
        {
            var regions = await ApiClientFactory.Instance.GetTop10ProvincesByIsoRegion(isoRegion);

            return regions.Data.OrderByDescending(o => o.Confirmed).Take(10).Select(s => new ReportModel(s.Region.Province, s.Confirmed, s.Deaths)).ToList();

        }

        public async Task<List<ReportModel>> GetTop10Regions()
        {
            var regions = await ApiClientFactory.Instance.GetTop10Regions();

            return regions.Data.OrderByDescending(o => o.Confirmed).Take(10).Select(s => new ReportModel(s.Region.Name, s.Confirmed, s.Deaths)).ToList();
        }
    }
}
