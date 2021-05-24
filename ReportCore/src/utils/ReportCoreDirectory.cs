using System.IO;
using System.Reflection;

using com.bitscopic.reportcore.svc;

namespace com.bitscopic.reportcore.utils {
    public static class ReportCoreDirectory {
        public static string GetReportCoreDirectory() {
            string executingAssemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            string reportCoreName = typeof(TemplatedReportGenerator).Assembly.GetName().Name;

            string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string parentDirectoryPath = assemblyPath.Substring(0, assemblyPath.IndexOf(executingAssemblyName));
            string reportCoreDirectory = $"{parentDirectoryPath}{reportCoreName}";

            return reportCoreDirectory;
        }
        public static string GetReportTemplateDirectory() {
            return $"{GetReportCoreDirectory()}{Path.DirectorySeparatorChar}{TemplatedReportGenerator.TemplateDirectoryName}";
        }
    }
}