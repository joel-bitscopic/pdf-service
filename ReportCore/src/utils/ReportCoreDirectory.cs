using System.IO;
using System.Reflection;

using com.bitscopic.reportcore.svc;

namespace com.bitscopic.reportcore.utils {
    public static class ReportCoreDirectory {
        public static string GetReportCoreDirectory() {
            Assembly entryAssembly = Assembly.GetEntryAssembly();
            string entryAssemblyName = entryAssembly.GetName().Name;
            string entryassemblyPath = Path.GetDirectoryName(entryAssembly.Location);
            
            string parentDirectoryPath;

            if (entryassemblyPath.IndexOf(entryAssemblyName) == -1) {
                Assembly executingAssembly = Assembly.GetExecutingAssembly();
                string executingAssemblyName = executingAssembly.GetName().Name;
                string executingAssemblyPath = Path.GetDirectoryName(executingAssembly.Location);

                if (executingAssemblyPath.IndexOf(executingAssemblyName) == -1)
                    throw new DirectoryNotFoundException("Could not dynamically locate ReportCore's directory based off of assembly information");
                else
                    parentDirectoryPath = executingAssemblyPath.Substring(0, executingAssemblyPath.IndexOf(executingAssemblyName));
            }
            else
                parentDirectoryPath = entryassemblyPath.Substring(0, entryassemblyPath.IndexOf(entryAssemblyName));

            string reportCoreName = typeof(TemplatedReportGenerator).Assembly.GetName().Name;
            string reportCoreDirectory = $"{parentDirectoryPath}{reportCoreName}";

            return reportCoreDirectory;
        }
        public static string GetReportTemplateDirectory() {
            return $"{GetReportCoreDirectory()}{Path.DirectorySeparatorChar}{TemplatedReportGenerator.TemplateDirectoryName}";
        }
    }
}