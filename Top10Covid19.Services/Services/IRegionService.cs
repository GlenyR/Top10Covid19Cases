using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Top10Covid19.Services.Models;

namespace Top10Covid19.Services.Services
{
    public interface IRegionService
    {
        Task<List<RegionModel>> GetAllAsync();

    }
}
