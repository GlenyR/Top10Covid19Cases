using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Top10Covid19.Services.Models;

namespace Top10Covid19.Web.Helpers.DataExporters
{
    public class ExportToXML : FileResult
    {
        private readonly IEnumerable<ReportModel> _reportData;
        private readonly string _fileName;
        public ExportToXML(IEnumerable<ReportModel> reportData,string fileDownloadName) : base("text/xml")
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

            context.HttpContext.Response.ContentType = "application/xml";
            context.HttpContext.Response.Headers.Add("Content-Disposition", cd.ToString());

            XmlDocument xml = new XmlDocument();
            XmlElement root = xml.CreateElement("Report");
            xml.AppendChild(root);
            foreach (var product in _reportData)
            {
                XmlElement child = xml.CreateElement("Product");
                child.SetAttribute("Name", product.Name.ToString());
                child.SetAttribute("Cases", product.Confirmed.ToString());
                child.SetAttribute("Deaths", product.Deaths.ToString());
                root.AppendChild(child);
            }

            using var streamWriter = new StreamWriter(response.Body);
            await streamWriter.WriteLineAsync(xml.OuterXml.ToString());
            await streamWriter.FlushAsync();
        }

    }
}
                                 