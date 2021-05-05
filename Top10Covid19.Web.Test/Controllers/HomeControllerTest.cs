using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Top10Covid19.Controllers;
using Top10Covid19.Services.Models;
using Top10Covid19.Services.Services;
using Top10Covid19.Web.Helpers.DataExporters;
using Top10Covid19.Web.ViewModels;
using Xunit;

namespace Top10Covid19.Web.Test
{
    public class HomeControllerTest
    {

        private readonly Mock<IOptions<ApiSettingsModel>> _settings;
        private readonly Mock<IReportService> _reportService;
        private readonly Mock<IRegionService> _regionService;
        private readonly HomeController _controller;
        public HomeControllerTest()
        {

            var settingMocked = new ApiSettingsModel() { ApiHost = "test.api.host", ApiKey = "test.api.key", BaseApiUrl = "test.url.api" };
            _settings = new Mock<IOptions<ApiSettingsModel>>();
            _settings.Setup(repo => repo.Value).Returns(settingMocked);

            _regionService = new Mock<IRegionService>();
            _regionService.Setup(region => region.GetAllAsync()).ReturnsAsync(new List<RegionModel>() { new RegionModel("CHINA","CHN")});
            _reportService = new Mock<IReportService>();

            _controller = new HomeController(_settings.Object, _reportService.Object, _regionService.Object);
        }


        [Fact]
        public async void Index_Action_ReturnsViewForIndex()
        {
            var result = await _controller.Index("Regions");
            Assert.IsType<ViewResult>(result);
        }


        [Fact]
        public async void Index_Returns_Regions()
        {
            //Arrange
            var selectedRegion = "Regions";
            _reportService.Setup(p => p.GetTop10Regions()).ReturnsAsync(GetTestRegionData());
            //Act
            var result = await _controller.Index(selectedRegion);


            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<SelectReportVM>(
                viewResult.ViewData.Model);

            Assert.Equal(selectedRegion, model.SelectedRegion);
            Assert.Equal(3, model.Data.Count());
        }


        private List<ReportModel> GetTestRegionData()
        {
            var data = new List<ReportModel>()
            {
                new ReportModel("US",100,15),
                new ReportModel("CHINA",80,10),
                new ReportModel("PANAMA",60,5),
            };

            return data;
        }

        private List<ReportModel> GetTestProvincesData()
        {
            var data = new List<ReportModel>()
            {
                new ReportModel("CHINA01",900,150),
                new ReportModel("CHINA02",4,7),
            };

            return data;
        }


        [Fact]
        public async void Index_ReturnsProvinces_ByRegion()
        {
            //Arrange
            var selectedRegion = "CHN";
            _reportService.Setup(p => p.GetTop10ProvincesByIsoRegion(selectedRegion)).ReturnsAsync(GetTestProvincesData());
            //Act
            var result = await _controller.Index(selectedRegion);


            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<SelectReportVM>(
                viewResult.ViewData.Model);

            Assert.Equal(selectedRegion, model.SelectedRegion);
            Assert.Equal(2, model.Data.Count());
        }


        [Fact]
        public async void Index_ExportToCSV_Regions()
        {
            //Arrange
            var selectedRegion = "Regions";
            _reportService.Setup(p => p.GetTop10Regions()).ReturnsAsync(GetTestRegionData());
            var fileName = "REGION_Reports.csv";
            var contentType = "text/csv";

            //Act
            var result = await _controller.ExportToCSV(selectedRegion);


            //Assert
            var viewResult = Assert.IsType<ExportToCSV>(result);
            Assert.Equal(contentType, viewResult.ContentType);
            Assert.Equal(fileName, viewResult.FileDownloadName);
        }


        [Fact]
        public async void Index_ExportToJSON_Regions()
        {
            //Arrange
            var selectedRegion = "Regions";
            _reportService.Setup(p => p.GetTop10Regions()).ReturnsAsync(GetTestRegionData());
            var fileName = "REGION_Reports.json";
            var contentType = "text/json";

            //Act
            var result = await _controller.ExportToJSON(selectedRegion);


            //Assert
            var viewResult = Assert.IsType<ExportToJSON>(result);
            Assert.Equal(contentType, viewResult.ContentType);
            Assert.Equal(fileName, viewResult.FileDownloadName);
        }


        [Fact]
        public async void Index_ExportToXML_Regions()
        {
            //Arrange
            var selectedRegion = "Regions";
            _reportService.Setup(p => p.GetTop10Regions()).ReturnsAsync(GetTestRegionData());
            var fileName = "REGION_Reports.xml";
            var contentType = "text/xml";

            //Act
            var result = await _controller.ExportToXML(selectedRegion);


            //Assert
            var viewResult = Assert.IsType<ExportToXML>(result);
            Assert.Equal(contentType, viewResult.ContentType);
            Assert.Equal(fileName, viewResult.FileDownloadName);
        }
    }
}
