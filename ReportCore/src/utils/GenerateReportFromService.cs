using System;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json;
using RestSharp;

using com.bitscopic.reportcore.models;

namespace com.bitscopic.reportcore.utils
{
    public static class GenerateReportFromService {
        public static IRestResponse GenerateReportRS(ReportBaseModel model, RestClient client, string serviceURL)
        {
            RestRequest request = new RestRequest(serviceURL, Method.POST);
            request.AddHeader("Content-Type", "text/json");

            var serializedModel = JsonConvert.SerializeObject(model);
            request.AddParameter("text/json", serializedModel, ParameterType.RequestBody);
            
            return client.Execute(request);
        }
        public static RestRequestAsyncHandle GenerateReportAsyncRS(ReportBaseModel model, RestClient client, string serviceURL, Action<IRestResponse> callback)
        {
            RestRequest request = new RestRequest(serviceURL, Method.POST);
            request.AddHeader("Content-Type", "text/json");

            var serializedModel = JsonConvert.SerializeObject(model);
            request.AddParameter("text/json", serializedModel, ParameterType.RequestBody);
            
            return client.ExecuteAsync(request, callback);
        }
    
        public static HttpResponseMessage GenerateReportHttp(ReportBaseModel model, HttpClient client, string serviceURL) 
        {
            var request = new HttpRequestMessage(HttpMethod.Post, serviceURL);
            request.Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "text/json");
            
            return client.Send(request);        
        }
        public static Task<HttpResponseMessage> GenerateReportAsyncHttp(ReportBaseModel model, HttpClient client, string serviceURL) 
        {
            var request = new HttpRequestMessage(HttpMethod.Post, serviceURL);
            request.Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            
            return client.SendAsync(request);        
        }
    }
}