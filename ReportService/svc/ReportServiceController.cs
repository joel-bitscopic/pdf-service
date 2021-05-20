using System;
using System.Net;
using System.IO;
using System.Reflection;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using com.bitscopic.reportcore.svc;
using com.bitscopic.reportservice.src;

namespace com.bitscopic.reportservice.svc
{
    [ApiController]
    [Route("svc")]
    public class ReportServiceController : ControllerBase
    {
        private readonly ILogger<ReportServiceController> _logger;

        private string GetReportTemplateDirectory() {
            string reportServiceName = Assembly.GetExecutingAssembly().GetName().Name;
            string reportCoreName = typeof(TemplatedReportGenerator).Assembly.GetName().Name;

            string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string parentDirectoryPath = assemblyPath.Substring(0, assemblyPath.IndexOf(reportServiceName));
            string reportTemplateDirectory = $"{parentDirectoryPath}{reportCoreName}{Path.DirectorySeparatorChar}{TemplatedReportGenerator.TemplateDirectoryName}";

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
                byte[] reportBytes = TemplatedReportGenerator.GenerateReport(model, reportTemplatePath).ToByteArray();

                FileSystemFile reportDTO = new FileSystemFile(TemplatedReportGenerator.GetReportDefaultFilename(model), reportBytes);

                return JsonConvert.SerializeObject(reportDTO);
            }
            catch (JsonReaderException jsonException) {
                return JsonConvert.SerializeObject(new RequestFault(jsonException.Message, ((int)HttpStatusCode.BadRequest).ToString(), jsonException.InnerException));
            }
            catch (Exception e) {
                return JsonConvert.SerializeObject(new RequestFault(e.Message, ((int)HttpStatusCode.InternalServerError).ToString(), e.InnerException));
            }
        }
    }
}
