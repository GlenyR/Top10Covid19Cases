using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Top10Covid19.ApiClient.Utilities;
using Top10Covid19.Models;
using Top10Covid19.Services.Models;
using Top10Covid19.Services.Services;
using Top10Covid19.Web.ViewModels;
using System.Linq;
using System.Text;
using Top10Covid19.Web.Helpers.DataExporters;

namespace Top10Covid19.Controllers
{
    public class HomeController : Controller
    {
     
        private readonly IOptions<ApiSettingsModel> _apiSettingsModel;
        private readonly IReportService _reportService;
        private readonly IRegionService _regionService;

        public HomeController(IOptions<ApiSettingsModel> apiSettingsModel, IReportService reportService, IRegionService regionService)
        {
            _apiSettingsModel = apiSettingsModel;
            _reportService = reportService;
            _regionService = regionService;

            ApiSettings.BaseApiUrl = _apiSettingsModel.Value.BaseApiUrl;
            ApiSettings.ApiKey = _apiSettingsModel.Value.ApiKey;
            ApiSettings.ApiHost = _apiSettingsModel.Value.ApiHost;

        }

        public async Task<IActionResult> Index(string selectedRegion)
        {
            var viewModel = await PopulateViewModel(selectedRegion);
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task<SelectReportVM> PopulateViewModel(string region = "")
        {
            var selectedRegion = String.IsNullOrEmpty(region) ? "Regions" : region;


            ViewData["Region"] = selectedRegion;

            ViewData["NameTitle"] = selectedRegion == "Regions" ? "REGION" : "PROVINCE";
            List<ReportModel> data;

            if (selectedRegion == "Regions")
            {
                data = await _reportService.GetTop10Regions();
            }else
            {
                data = await _reportService.GetTop10ProvincesByIsoRegion(selectedRegion);
            }

            var regions = await _regionService.GetAllAsync();
            var regionsModel = regions.Select(s => new SelectListItem { Value = s.Iso, Text = s.Name });

            var viewModel = new SelectReportVM
            {
                Data = data,
                Regions = regionsModel,
                SelectedRegion = selectedRegion
            };

            return viewModel;
        }

        private async Task<List<ReportModel>> GetReportData(string region)
        {
            var selectedRegion = String.IsNullOrEmpty(region) ? "Regions" : region;

            List<ReportModel> data;

            if (selectedRegion == "Regions")
            {
                data = await _reportService.GetTop10Regions();
            }
            else
            {
                data = await _reportService.GetTop10ProvincesByIsoRegion(selectedRegion);
            }

            return data;
        }

        private string GetTitle(string listType)
        {
            return listType == "Regions" ? "REGION" : "PROVINCE";
        }


        [HttpPost]
        public async Task<FileResult> ExportToCSV(string selectedRegion)
        {
             selectedRegion = String.IsNullOrEmpty(selectedRegion) ? "Regions" : selectedRegion;
            
            var data = await GetReportData(selectedRegion);
            var nameTitle = GetTitle(selectedRegion);
            var fileName = $"{nameTitle}_Reports.csv";

            return new ExportToCSV(data, fileName);
        }


        [HttpPost]
        public async Task<FileResult> ExportToJSON(string selectedRegion)
        {
            selectedRegion = String.IsNullOrEmpty(selectedRegion) ? "Regions" : selectedRegion;

            var data = await GetReportData(selectedRegion);
            var nameTitle = GetTitle(selectedRegion);
            var fileName = $"{nameTitle}_Reports.json";

            return new ExportToJSON(data, fileName);
        }


        [HttpPost]
        public async Task<FileResult> ExportToXML(string selectedRegion)
        {
            selectedRegion = String.IsNullOrEmpty(selectedRegion) ? "Regions" : selectedRegion;

            var data = await GetReportData(selectedRegion);
            var nameTitle = GetTitle(selectedRegion);

            var fileName = $"{nameTitle}_Reports.xml";
            return new ExportToXML(data, fileName);
        }

    }
}
