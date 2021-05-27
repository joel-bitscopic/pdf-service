using System;
using System.Web;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Xunit;
using RestSharp;

using com.bitscopic.reportcore.utils;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;

namespace com.bitscopic.reportcore.tests
{
    public class ReportServiceTests
    {
        private const string SERVICE_URL_DOMAIN = "https://localhost:5001/";
        private const string SERVICE_URL_PATH = "svc/createReport";

        public RestClient createClient() {
            var client = new RestClient(SERVICE_URL_DOMAIN);
            client.Timeout = -1;

            return client;
        }
        public RestRequest createSvcRequest() {
            var request = new RestRequest(SERVICE_URL_PATH, Method.POST);
            request.AddHeader("Content-Type", "text/json");

            return request;
        }

        public string getValidRequestBody() {
            return "{\"Patient\":{\"PraediGeneAccessionNumber\":\"PG35544-2\",\"Name\":\"Max\",\"DateOfBirth\":\"1991-07-17T00:00:00\",\"FormattedDateOfBirth\":\"5/5/21\",\"CollectionDate\":\"2021-05-05T10:30:00\",\"FormattedCollectionDate\":\"5/5/21 10:30\",\"SiteAccessionNumber\":\"PALO 17 2526\",\"SocialSecurityNumber\":\"xxx-xx-xxxx\",\"OrderingSite\":\"MA\",\"OrderingPhysician\":\"Dr. Phil\",\"PHLRAccessionNumber\":null},\"ResistanceTest\":{\"TestPerformed\":\"SARS-CoV-2 Sequencing\",\"TestDate\":\"2020-12-04T00:00:00\",\"FormattedTestDate\":\"12/4/20\",\"ReceivedDate\":\"2021-03-03T00:00:00\",\"FormattedReceivedDate\":\"3/3/21\",\"SampleType\":\"NP swab\",\"ReportDate\":\"2021-04-09T00:00:00\",\"FormattedReportDate\":\"4/9/21\"},\"ResultSet\":[{\"Gene\":\"Spike\",\"MutationDetected\":\"L5F\",\"Zygosity\":\"Homozygous\",\"MutationType\":\"Missense\"},{\"Gene\":\"Spike\",\"MutationDetected\":\"L5F\",\"Zygosity\":\"Homozygous\",\"MutationType\":\"Missense\"},{\"Gene\":\"Spike\",\"MutationDetected\":\"L5F\",\"Zygosity\":\"Homozygous\",\"MutationType\":\"Missense\"},{\"Gene\":\"Spike\",\"MutationDetected\":\"L5F\",\"Zygosity\":\"Homozygous\",\"MutationType\":\"Missense\"},{\"Gene\":\"Spike\",\"MutationDetected\":\"L5F\",\"Zygosity\":\"Homozygous\",\"MutationType\":\"Missense\"},{\"Gene\":\"ORF1A\",\"MutationDetected\":\"Δ3675-3677\",\"Zygosity\":\"Homozygous\",\"MutationType\":\"Deletion\"},{\"Gene\":\"N\",\"MutationDetected\":\"A119S\",\"Zygosity\":\"Homozygous\",\"MutationType\":\"Missense\"},{\"Gene\":\"N\",\"MutationDetected\":\"A119S\",\"Zygosity\":\"Homozygous\",\"MutationType\":\"Missense\"},{\"Gene\":\"N\",\"MutationDetected\":\"A119S\",\"Zygosity\":\"Homozygous\",\"MutationType\":\"Missense\"},{\"Gene\":\"N\",\"MutationDetected\":\"A119S\",\"Zygosity\":\"Homozygous\",\"MutationType\":\"Missense\"}],\"ResultSummary\":\"POSITIVE: VARIANT OF INTEREST B.1.526 DETECTED\",\"NextClade\":\"20C\",\"PangoLineage\":\"B.1.526\",\"Interpretation\":\"The SARS-CoV-2 variant of interest B.1.526 has the following characterized amino acid changes: Spike L5F, T95I, D253G, S477N, E484K, D614G, ORF1a L3201P, T265I, Δ3675-3677, ORF1b P314L, Q1011H, ORF3a Q57H, ORF8 T11I, and 5’UTR R81C. These changes are present in this specimen. This variant was first detected in New York in November 2020, and is predicted to have potentially reduced neutralization by monoclonal antibody treatments, convalescent and post-vaccination sera.\",\"ReportID\":4,\"OutputFormat\":\"pdf\",\"Comments\":\"The sequence also contains the N variants A119S, R203K, G204R, and M234I, which are all characteristic of the P.2 variant of interest. Further investigation may be warranted. Whole SARS-CoV-2 genome analyzed (30 Kb). Coverage: 99.3%; Sequencing depth: 1,022.\",\"Footer\":\"Mark Holodniy, MD, FACP, Director, VHA Public Health Reference Laboratory, 3801 Miranda Avenue (132), Palo Alto, CA 94304, V21PHRL@va.gov, CLIA# 05D2125891\"}";
        }
        public string getInvalidRequestBody() {
            return "{\"foo\": \"foo\"}";
        }

        public JObject parseEscapedJson(string strJson) {
            if (strJson.StartsWith('"')) {
                var unescapedJson = JToken.Parse(strJson);

                return JObject.Parse((string)unescapedJson);
            }
            else
                return JObject.Parse(strJson);            
        }

        [Fact]
        public void testServiceAvailable()
        {
            var client = createClient();

            var request = createSvcRequest();
            request.AddParameter("text/json", getInvalidRequestBody(), ParameterType.RequestBody);

            var response = client.Execute(request);
            Assert.True(response.IsSuccessful);
        }
        [Fact]
        public void testInvalidServiceUnavailable()
        {
            var client = createClient();
            var request = new RestRequest("invalidSvc", Method.POST);
            request.AddHeader("Content-Type", "text/json");

            var response = client.Execute(request);
            Assert.False(response.IsSuccessful);
        }

        [Fact]
        public void testServiceWithValidJson()
        {
            var client = createClient();

            var request = createSvcRequest();
            request.AddParameter("text/json", getValidRequestBody(), ParameterType.RequestBody);

            var response = client.Execute(request);
            //test is only meaningful when a response was successfully received. otherwise you'll get misleading errors
            Assert.True(response.IsSuccessful, "Unable to connect to service, check testServiceAvailable test");

            var responseContent = parseEscapedJson(response.Content);
            Assert.True(responseContent.ContainsKey("data"));
        }
        [Fact]
        public void testServiceWithInvalidJson()
        {
            var client = createClient();

            var request = createSvcRequest();
            request.AddParameter("text/json", getInvalidRequestBody(), ParameterType.RequestBody);

            var response = client.Execute(request);
            //test is only meaningful when a response was successfully received. otherwise you'll get misleading errors
            Assert.True(response.IsSuccessful, "Unable to connect to service, check testServiceAvailable test");

            var responseContent = parseEscapedJson(response.Content);
            Assert.True(responseContent.ContainsKey("errorCode"));
        }
    
        //i think this is one of four preferred ways of sending a request to the service from a strongly typed source. there's minimal conversion/encoding involved and it should match client-side workflows closely. works around the 3rd party RestSharp library synchronously
        [Fact]
        public void testServiceWithTypedModelSerializedRSSync() 
        {
            var client = createClient();
            var covidModel = PlaceholderReportUtilities.GenerateFakeCOVID19SequencingModel();

            IRestResponse response = GenerateReportFromService.GenerateReportRS(covidModel, client, SERVICE_URL_PATH);
            //test is only meaningful when a response was successfully received. otherwise you'll get misleading errors
            Assert.True(response.IsSuccessful, "Unable to connect to service, check testServiceAvailable test");

            var responseContent = parseEscapedJson(response.Content);
            Assert.True(responseContent.ContainsKey("data"));
        }
        //i think this is another ways of sending a request to the service from a strongly typed source. there's minimal conversion/encoding involved and it should match client-side workflows closely. works around the 3rd party RestSharp library asynchrounously
        [Fact]
        public async void testServiceWithTypedModelSerializedRSAsync() 
        {
            var client = createClient();
            var covidModel = PlaceholderReportUtilities.GenerateFakeCOVID19SequencingModel();

            //allows the test to run to completion, waiting for the async callback to finish executing
            var awaiter = new TaskCompletionSource();

            GenerateReportFromService.GenerateReportAsyncRS(covidModel, client, SERVICE_URL_PATH, (IRestResponse response) => {
                //test is only meaningful when a response was successfully received. otherwise you'll get misleading errors
                Assert.True(response.IsSuccessful, "Unable to connect to service, check testServiceAvailable test");

                var responseContent = parseEscapedJson(response.Content);
                Assert.True(responseContent.ContainsKey("data"));

                awaiter.SetResult();
            });

            await awaiter.Task;
        }

        //i think this is another preferred ways of sending a request to the service from a strongly typed source. there's minimal conversion/encoding involved and it should match client-side workflows closely. works around the standard .NET HttpClient library synchronously
        [Fact]
        public async void testServiceWithTypedModelSerializedHttpSync() 
        {
            using(var client = new HttpClient()) {
                var covidModel = PlaceholderReportUtilities.GenerateFakeCOVID19SequencingModel();
                HttpResponseMessage response = GenerateReportFromService.GenerateReportHttp(covidModel, client, SERVICE_URL_DOMAIN + SERVICE_URL_PATH);
                //test is only meaningful when a response was successfully received. otherwise you'll get misleading errors
                Assert.True(response.IsSuccessStatusCode, "Unable to connect to service, check testServiceAvailable test");

                var strResponseContent = await response.Content.ReadAsStringAsync();
                var responseContent = parseEscapedJson(strResponseContent);
                Assert.True(responseContent.ContainsKey("data"));
            }
        }
        //i think this is another preferred ways of sending a request to the service from a strongly typed source. there's minimal conversion/encoding involved and it should match client-side workflows closely. works around the standard .NET HttpClient library asynchrounously
        [Fact]
        public async void testServiceWithTypedModelSerializedHttpAsync() 
        {
            using(var client = new HttpClient()) {
                var covidModel = PlaceholderReportUtilities.GenerateFakeCOVID19SequencingModel();
                HttpResponseMessage response = await GenerateReportFromService.GenerateReportAsyncHttp(covidModel, client, SERVICE_URL_DOMAIN + SERVICE_URL_PATH);
                //test is only meaningful when a response was successfully received. otherwise you'll get misleading errors
                Assert.True(response.IsSuccessStatusCode, "Unable to connect to service, check testServiceAvailable test");

                var strResponseContent = await response.Content.ReadAsStringAsync();
                var responseContent = parseEscapedJson(strResponseContent);
                Assert.True(responseContent.ContainsKey("data"));
            }
        }


        //another way to send a request to the service from a strongly typed source. there's a significant amount of conversion/encoding involved and some of it is unintuitive to me. this test mostly exists in case client-side workflows follow this approach for some reason. directly works around the 3rd party RestSharp library
        [Fact]
        public void testServiceWithTypedModelEncoded() 
        {
            var client = createClient();
            var request = createSvcRequest();

            var covidModel = PlaceholderReportUtilities.GenerateFakeCOVID19SequencingModel();
            var strJson = JsonConvert.SerializeObject(covidModel);
            var escapedStrJson = "\"" + HttpUtility.JavaScriptStringEncode(strJson) + "\"";

            request.AddParameter("text/json", escapedStrJson, ParameterType.RequestBody);
            
            var response = client.Execute(request);
            //test is only meaningful when a response was successfully received. otherwise you'll get misleading errors
            Assert.True(response.IsSuccessful, "Unable to connect to service, check testServiceAvailable test");

            var responseContent = parseEscapedJson(response.Content);
            Assert.True(responseContent.ContainsKey("data"));
        }

        //This test only asserts that a report was able to be generated when header image is not included. To check if the default header image was used you would need to debug the service or examine the output report
        [Fact]
        public void testServiceWithoutImage()
        {
            var client = createClient();
            var request = createSvcRequest();

            var model = PlaceholderReportUtilities.GenerateFakeHCVINModel();
            model.HeaderImage.Image = "";
            var serializedModel = JsonConvert.SerializeObject(model);

            request.AddParameter("text/json", serializedModel, ParameterType.RequestBody);
            
            var response = client.Execute(request);
            //test is only meaningful when a response was successfully received. otherwise you'll get misleading errors
            Assert.True(response.IsSuccessful, "Unable to connect to service, check testServiceAvailable test");

            var responseContent = parseEscapedJson(response.Content);
            Assert.True(responseContent.ContainsKey("data"));
        }

        [Fact]
        public void testServiceWithPDFOutput()
        {
            var client = createClient();
            var request = createSvcRequest();

            var model = PlaceholderReportUtilities.GenerateFakeCOVID19SequencingModel();
            model.OutputFormat = "pdf";
            var serializedModel = JsonConvert.SerializeObject(model);

            request.AddParameter("text/json", serializedModel, ParameterType.RequestBody);
            
            var response = client.Execute(request);
            //test is only meaningful when a response was successfully received. otherwise you'll get misleading errors
            Assert.True(response.IsSuccessful, "Unable to connect to service, check testServiceAvailable test");

            var responseContent = parseEscapedJson(response.Content);
            Assert.True(responseContent["fileName"].ToString().EndsWith(".pdf"));
        }

        [Fact]
        public void testServiceWithDocxOutput()
        {
            var client = createClient();
            var request = createSvcRequest();

            var model = PlaceholderReportUtilities.GenerateFakeCOVID19SequencingModel();
            model.OutputFormat = "docx";
            var serializedModel = JsonConvert.SerializeObject(model);

            request.AddParameter("text/json", serializedModel, ParameterType.RequestBody);
            
            var response = client.Execute(request);
            //test is only meaningful when a response was successfully received. otherwise you'll get misleading errors
            Assert.True(response.IsSuccessful, "Unable to connect to service, check testServiceAvailable test");

            var responseContent = parseEscapedJson(response.Content);
            Assert.True(responseContent["fileName"].ToString().EndsWith(".docx"));
        }

        [Fact]
        public void testServiceWithoutOutputType()
        {
            var client = createClient();
            var request = createSvcRequest();

            var model = PlaceholderReportUtilities.GenerateFakeCOVID19SequencingModel();
            model.OutputFormat = "";
            var serializedModel = JsonConvert.SerializeObject(model);

            request.AddParameter("text/json", serializedModel, ParameterType.RequestBody);
            
            var response = client.Execute(request);
            //test is only meaningful when a response was successfully received. otherwise you'll get misleading errors
            Assert.True(response.IsSuccessful, "Unable to connect to service, check testServiceAvailable test");

            var responseContent = parseEscapedJson(response.Content);
            Assert.True(responseContent["fileName"].ToString().EndsWith(".pdf"));
        }

        //simulates making 10 requests to the service api asynchronously
        [Fact]
        public void testServiceWithTypedModelSerializedHttpBulkAsync() 
        {
            using(var client = new HttpClient()) {
                var covidModel = PlaceholderReportUtilities.GenerateFakeCOVID19SequencingModel();
                
                List<Task<Task>> threads = new List<Task<Task>>();
                for (int iThread = 0; iThread < 10; iThread++) {
                    var thread = GenerateReportFromService.GenerateReportAsyncHttp(covidModel, client, SERVICE_URL_DOMAIN + SERVICE_URL_PATH);

                    Task<Task> unitTestThread = thread.ContinueWith(async (Task<HttpResponseMessage> taskResponse) => {
                        var response = await taskResponse;

                        //test is only meaningful when a response was successfully received. otherwise you'll get misleading errors
                        Assert.True(response.IsSuccessStatusCode, "Unable to connect to service, check testServiceAvailable test");

                        var strResponseContent = await response.Content.ReadAsStringAsync();
                        var responseContent = parseEscapedJson(strResponseContent);
                        Assert.True(responseContent.ContainsKey("data"));
                    });

                    threads.Add(unitTestThread);
                }

                Task.WaitAll(threads.ToArray());                
            }
        }
    }
}
