using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Top10Covid19.Services.Models;

namespace Top10Covid19.Web.ViewModels
{
    public class SelectReportVM
    {
        public IEnumerable<ReportModel> Data { set; get; }
        public IEnumerable<SelectListItem> Regions { set; get; }
        public string SelectedRegion { set; get; }
        public string SelectedExportFormat { set; get; }
    }
}
