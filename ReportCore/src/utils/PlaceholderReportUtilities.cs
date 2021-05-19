using System;
using System.Collections.Generic;
using System.IO;

using Adobe.DocumentServices.PDFTools.options.documentmerge;
using Newtonsoft.Json.Linq;
using TemplatedReportGenerator.Model;
using TemplatedReportGenerator.ReportModel;

namespace TemplatedReportGenerator.utils {
    static class PlaceholderReportUtilities {
        public static HIVINResistanceReportModel GenerateFakeHCVINModel() {
            var encodedHeaderImage = ReportID.HIVINResistance.GetReportHeaderImage();

            var patientModel = new PatientModel("Max");
            patientModel.DateOfBirth = new DateTime(1991, 7, 17);
            patientModel.CollectionDate = new DateTime(2021, 5, 5, 10, 30, 0);
            patientModel.SiteAccessionNumber = "PALO 17 2526";
            patientModel.SocialSecurityNumber = "xxx-xx-xxxx";
            patientModel.OrderingSite = "MA";
            patientModel.OrderingPhysician = "Dr. Phil";
            patientModel.PHLRAccessionNumber = "P15-0133";

            var hcvTestModel = new HIVINResistanceTestModel();
            hcvTestModel.TestPerformed = "HIV-1 Integrase (IN) Resistance Genotype"; //TODO should this be static for HCVResistanceTestModels?
            hcvTestModel.TestDate = new DateTime(2014, 12, 22);
            hcvTestModel.ReceivedDate = new DateTime(2014, 12, 22, 13, 15, 0);
            hcvTestModel.SampleType = "plasma"; //TODO should this come from a enum or domain?
            hcvTestModel.ReportDate = new DateTime(2014, 12, 29, 9, 7, 0);
            hcvTestModel.ReferenceRange = "susceptible"; //TODO should this come from a enum or domain?

            var resultsModel = new InhibitorResistanceResultsModel<InhibitorResistanceStandardModel>("Integrase Strand Transfer Inhibitor Resistance Interpretation"); //TODO should this name be a constant somewhere?
            resultsModel.Results.Add(new InhibitorResistanceStandardModel("dolutegravir (DTG) -- Tivicay®", "Susceptible"));
            resultsModel.Results.Add(new InhibitorResistanceStandardModel("elvitegravir (EVG) --  Stribild®", "Susceptible"));
            resultsModel.Results.Add(new InhibitorResistanceStandardModel("raltegravir (RAL) -- Isentress®", "Susceptible"));

            var reportModel = new HIVINResistanceReportModel(encodedHeaderImage, patientModel, hcvTestModel, resultsModel);
            reportModel.IntegraseHIVSubtype = "B";
            reportModel.IntegraseCodonsAnalyzed = "1-288";
            reportModel.IntegraseMajorResistanceDetected = "None";
            reportModel.IntegraseAccessoryResistanceDetected = "None";
            reportModel.OtherAminoAcidChanges = "S17N, R20K, A23V, G59E, L101I, T206S, S230N, D256E";

            reportModel.SuperscriptAContent = "The test uses RT-PCR and population-based sequencing to determine the consensus nucleotide and resulting amino acid sequence of the 289 codons of the HIV-1 integrase gene (Varghese et al., AIDS Res Human Retro 26(12):1323-1326).  This test was developed and its performance characteristics determined by PHRL. The FDA has not approved or cleared this test; however, FDA clearance or approval is not currently required for clinical use. The results are not intended to be used as the sole means for clinical diagnosis or patient management decisions.";
            reportModel.SuperscriptBContent = "Classification of resistance-associated mutations and integrase drug resistance interpretation provided by the Stanford HIV Resistance Database version 8.3 (hivdb.stanford.edu).";

            reportModel.Footer = "Mark Holodniy, MD, FACP, Director, VHA Public Health Reference Laboratory, Veterans Affairs Palo Alto Health Care System, 3801 Miranda Avenue, Palo Alto, CA 94304,  V21PHRL@va.gov CLIA #05D2125891";

            return reportModel;
        }
        public static PHRLChargebackInvoiceReportModel GenerateFakePHRLInvoiceModel() {
            var headerImage = ReportID.PHRLChargebackInvoice.GetReportHeaderImage();
            
            var invoice1 = new PHRLInvoiceModel();
            invoice1.PHRLNumber = "P16-7117";
            invoice1.Description = "Zika Virus IgM";
            invoice1.CPTCode = "86790";
            invoice1.Price = 50;
            invoice1.DateReceived = new DateTime(2016, 11, 1);
            invoice1.SiteSpecimenNumber = "SREF 16 4841";

            var invoice2 = new PHRLInvoiceModel();
            invoice2.PHRLNumber = "P16-7124";
            invoice2.Description = "Zika Virus Trioplex";
            invoice2.CPTCode = "87798";
            invoice2.Price = 90;
            invoice2.DateReceived = new DateTime(2016, 11, 1);
            invoice2.SiteSpecimenNumber = "SREF 16 4852";

            var invoices = new List<PHRLInvoiceModel>();
            for (int i = 0; i < 10; i++) {
                invoices.Add(invoice1);
                invoices.Add(invoice2);
            }

            var reportModel = new PHRLChargebackInvoiceReportModel(headerImage, invoices);
            reportModel.Site = "672";
            reportModel.Date = new DateTime(2017, 1, 17);
            reportModel.Attention = "email1@fake-email.com, email2@fake-email.com, email3@fake-email.com";
            reportModel.Total = 27940;

            return reportModel;
        }
        public static COVID19Sequencing2BReportModel GenerateFakeCOVID19SequencingModel() {
            var encodedHeaderImage = ReportID.COVIDSequencing2B.GetReportHeaderImage();
            
            var patientModel = new COVID19Sequencing2BPatientModel("Max");
            patientModel.DateOfBirth = new DateTime(1991, 7, 17);
            patientModel.CollectionDate = new DateTime(2021, 5, 5, 10, 30, 0);
            patientModel.SiteAccessionNumber = "PALO 17 2526";
            patientModel.SocialSecurityNumber = "xxx-xx-xxxx";
            patientModel.OrderingSite = "MA";
            patientModel.OrderingPhysician = "Dr. Phil";
            patientModel.PraediGeneAccessionNumber = "PG35544-2";

            var testModel = new ResistanceTestModel();
            testModel.ReceivedDate = new DateTime(2021, 3, 3);
            testModel.ReportDate = new DateTime(2021, 4, 9);
            testModel.TestPerformed = "SARS-CoV-2 Sequencing";
            testModel.TestDate = new DateTime(2020, 12, 4);
            testModel.SampleType = "NP swab";
            
            var results = new List<COVID19Sequencing2BResultsModel>();
            
            //add enough fake data points to get an idea of what the actual report will look like with real data
            var fakeResult1 = new COVID19Sequencing2BResultsModel("Spike", "L5F", "Homozygous", "Missense");
            for (int iFakeResult1 = 0; iFakeResult1 < 5; iFakeResult1++) {
                results.Add(fakeResult1);
            }

            results.Add(new COVID19Sequencing2BResultsModel("ORF1A", "Δ3675-3677", "Homozygous", "Deletion"));

            var fakeResult2 = new COVID19Sequencing2BResultsModel("N", "A119S", "Homozygous", "Missense");
            for (int iFakeResult2 = 0; iFakeResult2 < 4; iFakeResult2++) {
                results.Add(fakeResult2);
            }
            
            var reportModel = new COVID19Sequencing2BReportModel(encodedHeaderImage, patientModel, testModel, results);
            reportModel.ResultSummary = "POSITIVE: VARIANT OF INTEREST B.1.526 DETECTED";
            reportModel.NextClade = "20C";
            reportModel.PangoLineage = "B.1.526";
            reportModel.Interpretation = "The SARS-CoV-2 variant of interest B.1.526 has the following characterized amino acid changes: Spike L5F, T95I, D253G, S477N, E484K, D614G, ORF1a L3201P, T265I, Δ3675-3677, ORF1b P314L, Q1011H, ORF3a Q57H, ORF8 T11I, and 5’UTR R81C. These changes are present in this specimen. This variant was first detected in New York in November 2020, and is predicted to have potentially reduced neutralization by monoclonal antibody treatments, convalescent and post-vaccination sera.";
            reportModel.Comments = "The sequence also contains the N variants A119S, R203K, G204R, and M234I, which are all characteristic of the P.2 variant of interest. Further investigation may be warranted. Whole SARS-CoV-2 genome analyzed (30 Kb). Coverage: 99.3%; Sequencing depth: 1,022.";
            reportModel.Footer = "Mark Holodniy, MD, FACP, Director, VHA Public Health Reference Laboratory, 3801 Miranda Avenue (132), Palo Alto, CA 94304, V21PHRL@va.gov, CLIA# 05D2125891";
            reportModel.SuperscriptAContent = "This test uses next generation sequencing to determine the consensus nucleotide and resulting amino acid sequence of SARS-CoV-2 compared to a reference sequence. The FDA has not approved or cleared this test; however, FDA clearance or approval is not currently required for public health use. Interpretations of CDC variants of concern and interest are drawn from updated guidance of the CDC website (https://www.cdc.gov/coronavirus/2019-ncov/cases-updates/variant-surveillance/variant-info.html). Results were analyzed with the Pangolin lineage assignment application (https://cov-lineages.org/pangolin.html) and Nextclade assignment and mutation calling application (https://clades.nextstrain.org). The results are not intended to be used as the sole means for clinical diagnosis or patient management decisions.";
            reportModel.SuperscriptBContent = "Human Genome Variation Society nomenclature.";
            reportModel.SuperscriptCContent = "https://www.biorxiv.org/content/10.1101/2021.03.24.436620v1.full https://www.biorxiv.org/content/10.1101/2021.02.14.431043v2.full";

            return reportModel;
        }

        public static void GenerateFakeReport(ReportBaseModel reportModel) {
            //Console.WriteLine(JsonSerializer.Serialize(reportModel));

            var pdfResult = TemplatedReportGenerator.GenerateReport(reportModel);

            var outputFormat = TemplatedReportGenerator.ConvertReportOutputFormat(reportModel);
            pdfResult.SaveAs(Directory.GetCurrentDirectory() + "/output/" + TemplatedReportGenerator.GetReportDefaultFilename(reportModel.ReportID, outputFormat));
        }
        public static void GenerateFakeReportFromJObject(JObject jsonModel) {
            var outputFormat = TemplatedReportGenerator.ConvertReportOutputFormat(jsonModel);
            var reportID = TemplatedReportGenerator.ConvertReportID(jsonModel);

            var pdfResult = TemplatedReportGenerator.GenerateReport(jsonModel);

            pdfResult.SaveAs(Directory.GetCurrentDirectory() + "/output/" + TemplatedReportGenerator.GetReportDefaultFilename(reportID, outputFormat));
        }
    }
}