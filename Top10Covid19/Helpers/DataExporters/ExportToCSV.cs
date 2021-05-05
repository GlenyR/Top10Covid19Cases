using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Top10Covid19.Services.Models;

namespace Top10Covid19.Web.Helpers.DataExporters
{
    public class ExportToCSV:  FileResult
    {
        private readonly IEnumerable<ReportModel> _reportData;
        private readonly string _fileName;
        public ExportToCSV(IEnumerable<ReportModel> reportData, string fileDownloadName) : base("text/csv")
        {
            _reportData = reportData;
            _fileName = fileDownloadName;
            FileDownloadName = fileDownloadName;
        }
        public async override Task ExecuteResultAsync(ActionContext context)
        {
            var response = context.HttpContext.Response;
            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = _fileName,
                Inline = false,
             };
             context.HttpContext.Response.Headers.Add("Content-Disposition", cd.ToString());

            using var streamWriter = new StreamWriter(response.Body);
            await streamWriter.WriteLineAsync("Name,Cases,Deaths");
            foreach (var p in _reportData)
            {
                await streamWriter.WriteLineAsync(
                  $"{p.Name}, {p.Confirmed}, {p.Deaths}"
                );
                await streamWriter.FlushAsync();
            }
            await streamWriter.FlushAsync();
        }

    }
}
