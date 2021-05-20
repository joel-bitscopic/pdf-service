using System;
using System.Net;
using System.IO;
using System.Reflection;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using TemplatedReportGenerator;
using com.bitscopic.src;

namespace com.bitscopic.svc
{
    [ApiController]
    [Route("svc")]
    public class ReportServiceController : ControllerBase
    {
        private readonly ILogger<ReportServiceController> _logger;

        private string GetReportTemplateDirectory() {
            string reportServiceName = Assembly.GetExecutingAssembly().GetName().Name;
            string reportCoreName = typeof(TemplatedReportGenerator.TemplatedReportGenerator).Assembly.GetName().Name;

            string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string parentDirectoryPath = assemblyPath.Substring(0, assemblyPath.IndexOf(reportServiceName));
            string reportTemplateDirectory = $"{parentDirectoryPath}{reportCoreName}{Path.DirectorySeparatorChar}{TemplatedReportGenerator.TemplatedReportGenerator.TemplateDirectoryName}";

            return reportTemplateDirectory;
        }

        public ReportServiceController(ILogger<ReportServiceController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public string post([FromBody]string strModel)
        {
            try {
                JObject model = JObject.Parse(strModel);

                string reportTemplatePath = GetReportTemplateDirectory();
                byte[] reportBytes = TemplatedReportGenerator.TemplatedReportGenerator.GenerateReport(model, reportTemplatePath).ToByteArray();

                FileSystemFile reportDTO = new FileSystemFile(TemplatedReportGenerator.TemplatedReportGenerator.GetReportDefaultFilename(model), reportBytes);

                return JsonConvert.SerializeObject(reportDTO);
            }
            catch (JsonReaderException jsonException) {
                return JsonConvert.SerializeObject(new RequestFault(jsonException.Message, HttpStatusCode.BadRequest.ToString(), jsonException.InnerException));
            }
            catch (Exception e) {
                return JsonConvert.SerializeObject(new RequestFault(e.Message, HttpStatusCode.InternalServerError.ToString(), e.InnerException));
            }
        }
    }
}
