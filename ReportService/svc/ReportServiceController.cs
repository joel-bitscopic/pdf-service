using System;
using System.Net;
using System.IO;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using com.bitscopic.reportcore.svc;
using com.bitscopic.reportcore.utils;
using com.bitscopic.reportservice.src;
using System.Net.Http;

namespace com.bitscopic.reportservice.svc
{
    [ApiController]
    [Route("svc")]
    public class ReportServiceController : ControllerBase
    {
        private readonly ILogger<ReportServiceController> _logger;

        private void useDefaultHeaderImageWhenEmpty(JObject model) {
            if (!model.ContainsKey("HeaderImage") 
                || !((JObject)model["HeaderImage"]).ContainsKey("Image") 
                || string.IsNullOrWhiteSpace(((JObject)model["HeaderImage"])["Image"].ToString())
            )

                model["HeaderImage"] = JObject.FromObject(new {
                    Image = "data:image/png;base64,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",
                    AltText = "Placeholder image"
                });
        }
        private void useDefaultOutputTypeWhenEmpty(JObject model) {
            if (!model.ContainsKey("OutputFormat") || string.IsNullOrWhiteSpace(model["OutputFormat"].ToString()))
                model["OutputFormat"] = "pdf";
        }

        public ReportServiceController(ILogger<ReportServiceController> logger)
        {
            _logger = logger;
        }

        ///<summary>
        ///Generate a PDF or MSWord report from a supported model. The template used will be inferred by the model's ReportID property and the resulting report format will be determined by the model's OutputFormat. The model should be supplied inside the request's body as json.
        ///</summary>
        ///<remarks>The request body should contain a single json model.</remarks>
        ///<returns>
        ///If the request is valid, a stringified json object representing a FileSystemFile will be returned. This will include the report as a byte array, a default filename for the report and other metadata about the byte array. If the request is invalid, a stringified json object representing a RequestFault will be returned containing information about the exception.
        ///</returns>
        [HttpPost("createReport")]
        public string createReport([FromBody]object objModel)
        {
            try {
                JObject model = JObject.Parse(objModel.ToString());

                useDefaultHeaderImageWhenEmpty(model);
                useDefaultOutputTypeWhenEmpty(model);

                string reportTemplatePath = ReportCoreDirectory.GetReportTemplateDirectory();
                byte[] reportBytes = TemplatedReportGenerator.GenerateReport(model, reportTemplatePath).ToByteArray();

                FileSystemFile reportDTO = new FileSystemFile(TemplatedReportGenerator.GetReportDefaultFilename(model), reportBytes);

                //useful for debugging
                //System.IO.File.WriteAllBytes($"{Directory.GetCurrentDirectory()}/output/{reportDTO.fileName}", reportDTO.data);
            
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
