using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Top10Covid19.Services.Models;

namespace Top10Covid19.Services.Services
{
    public interface IReportService
    {
        Task<List<ReportModel>> GetTop10Regions();

        Task<List<ReportModel>> GetTop10ProvincesByIsoRegion(string isoRegion);

    }
}
