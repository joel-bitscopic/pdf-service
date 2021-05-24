using System;
using System.IO;
using System.Linq;

using Xunit;

using com.bitscopic.reportcore.utils;
using com.bitscopic.reportcore.models;
using com.bitscopic.reportcore.svc;
using Newtonsoft.Json.Linq;

namespace com.bitscopic.reportcore.tests
{
    public class ReportGeneratorTests
    {
        public void assertReportID(ReportBaseModel reportModel, ReportID reportID) {
            Assert.Equal(reportModel.ReportID, reportID);
        }
        //Tests if a fake model for several report types can be successfully created with their corresponding ID
        [Fact]
        public void testReportModels()
        {
            var hivin = PlaceholderReportUtilities.GenerateFakeHCVINModel();
            assertReportID(hivin, ReportID.HIVINResistance);

            var phrl = PlaceholderReportUtilities.GenerateFakePHRLInvoiceModel();
            assertReportID(phrl, ReportID.PHRLChargebackInvoice);

            var covid = PlaceholderReportUtilities.GenerateFakeCOVID19SequencingModel();
            assertReportID(covid, ReportID.COVIDSequencing2B);
        }

        //Tests that any report that should be supported (as seen in report metadata) has a corresponding template file
        [Fact]
        private void testReportTemplateDirectoryMatchesMetadata() {
            var reportTemplateDirectory = ReportCoreDirectory.GetReportTemplateDirectory();
            var templateFiles = Directory.GetFiles(reportTemplateDirectory).Select(absoluteFilePath => Path.GetFileName(absoluteFilePath)).ToArray();

            foreach (var metadataPair in StaticReportMetadata.ReportMetadata) {
                if (!templateFiles.Contains(metadataPair.Value.TemplateFilename))
                    throw new FileNotFoundException($"Could not find template named '{metadataPair.Value.TemplateFilename}'");
            }
        }

        //Tests if fake strongly typed models can generate reports successfully
        [Fact]
        public void testTypedReportGeneration() 
        {
            var hcvin = PlaceholderReportUtilities.GenerateFakeHCVINModel();
            TemplatedReportGenerator.GenerateReport(hcvin);

            var phrl = PlaceholderReportUtilities.GenerateFakePHRLInvoiceModel();
            TemplatedReportGenerator.GenerateReport(phrl);

            var covid = PlaceholderReportUtilities.GenerateFakeCOVID19SequencingModel();
            TemplatedReportGenerator.GenerateReport(covid);
        }

        //Tests if fake json models can generate reports successfully
        [Fact]
        public void testJsonReportGeneration() 
        {
            var hcvin = JObject.FromObject(PlaceholderReportUtilities.GenerateFakeHCVINModel());
            TemplatedReportGenerator.GenerateReport(hcvin);

            var phrl = JObject.FromObject(PlaceholderReportUtilities.GenerateFakePHRLInvoiceModel());
            TemplatedReportGenerator.GenerateReport(phrl);

            var covid = JObject.FromObject(PlaceholderReportUtilities.GenerateFakeCOVID19SequencingModel());
            TemplatedReportGenerator.GenerateReport(covid);
        }

        //Tests if an invalid json model properly fails to generate a report
        [Fact]
        public void testInvalidJsonReportGeneration() 
        {
            var invalidModel = JObject.FromObject(new {
                foo = "foo",
                bar = "bar"
            });

            Assert.ThrowsAny<Exception>(() => TemplatedReportGenerator.GenerateReport(invalidModel));
        }

        //TemplatedReportGenerator.GenerateReport is not deterministic and while reports seem to be consistent visually, they almost always seem to have different byte code
        /*
        [Fact]
        public void testTypedReportConsistency() 
        {
            var hcvin = PlaceholderReportUtilities.GenerateFakeHCVINModel();
            
            var result1 = TemplatedReportGenerator.GenerateReport(hcvin).ToByteArray();
            var result2 = TemplatedReportGenerator.GenerateReport(hcvin).ToByteArray();

            Assert.True(result1.SequenceEqual(result2));
        }
        
        [Fact]
        public void testConvertedReportConsistency()
        {
            var hcvin = PlaceholderReportUtilities.GenerateFakeHCVINModel();
            var jsonHCVIN = JObject.FromObject(hcvin);
            
            var result1 = TemplatedReportGenerator.GenerateReport(hcvin).ToByteArray();
            var result2 = TemplatedReportGenerator.GenerateReport(jsonHCVIN).ToByteArray();

            Assert.True(result1.SequenceEqual(result2));
        }
        */
    }
}
