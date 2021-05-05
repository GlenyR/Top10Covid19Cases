using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Top10Covid19.Services.Models;

namespace Top10Covid19.Web.Helpers.DataExporters
{
    public class ExportToJSON:  FileResult
    {
        private readonly IEnumerable<ReportModel> _reportData;
        private readonly string _fileName;
        public ExportToJSON(IEnumerable<ReportModel> reportData,  string fileDownloadName) : base("text/json")
        {
            _reportData = reportData;
            _fileName = fileDownloadName;
            FileDownloadName = fileDownloadName;
        }
        public async override Task ExecuteResultAsync(ActionContext context)
        {

            var response = context.HttpContext.Response;

            string jsonReport = JsonSerializer.Serialize(_reportData);

            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = _fileName,
                Inline = false,
             };

            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.Headers.Add("Content-Length", jsonReport.Length.ToString());
            context.HttpContext.Response.Headers.Add("Content-Disposition", cd.ToString());

            using var streamWriter = new StreamWriter(response.Body);
            await streamWriter.WriteLineAsync(jsonReport);
            await streamWriter.FlushAsync();
        }

    }
}
