﻿using Newtonsoft.Json.Linq;
using com.bitscopic.reportcore.utils;

namespace com.bitscopic.reportcore
{
    class Program
    {
        static void Main(string[] args)
        {
            PlaceholderReportUtilities.GenerateFakeReport(PlaceholderReportUtilities.GenerateFakeHCVINModel());
            PlaceholderReportUtilities.GenerateFakeReport(PlaceholderReportUtilities.GenerateFakePHRLInvoiceModel());
            PlaceholderReportUtilities.GenerateFakeReport(PlaceholderReportUtilities.GenerateFakeCOVID19SequencingModel());
            
            PlaceholderReportUtilities.GenerateFakeReport(JObject.FromObject(PlaceholderReportUtilities.GenerateFakeHCVINModel()));
            PlaceholderReportUtilities.GenerateFakeReport(JObject.FromObject(PlaceholderReportUtilities.GenerateFakePHRLInvoiceModel()));
            PlaceholderReportUtilities.GenerateFakeReport(JObject.FromObject(PlaceholderReportUtilities.GenerateFakeCOVID19SequencingModel()));
        }
    }
}

//TODOs
//turn into a service - this one will be hosted in IIS
//refactor 
    //namespace com.bitscopic.<folder>
    //camel case property and method names
//deal with consequences - https://community.adobe.com/t5/document-services-apis/adobe-document-services-performance-issues/m-p/12030323/thread-id/1768
    //call adobe and figure out if any on prem solutions for generating pdf's from docx templates + json models or similar alternatives for our purpose
    //explore open source, care about input template + model and output quality