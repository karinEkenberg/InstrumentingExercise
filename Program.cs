
using System.Diagnostics;//is needed
using Microsoft.Extensions.Configuration;//for packages

namespace InstrumentingExercise
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string logPaths = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "log.txt");
            Console.WriteLine($"Writing to: {logPaths}.");
            TextWriterTraceListener logFile = new (File.CreateText(logPaths));
            Trace.Listeners.Add(logFile);
            Trace.AutoFlush = true;
            Debug.WriteLine("Debyg: I am looking..");
            Trace.WriteLine("Trace: I am looking..");
            Console.WriteLine($"Reading from appsettings.json in {0}", arg0: Directory.GetCurrentDirectory());
            ConfigurationBuilder builder = new();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            TraceSwitch ts = new(
                displayName: "MinSwitch",
                description: "This switch is set via a JSON config");
            configuration.GetSection("MinSwitch").Bind(ts);
            Trace.WriteLine(ts.TraceError, "Trace errors..");
            Trace.WriteLine(ts.TraceWarning, "Trace warnings..");
            Trace.WriteLine(ts.TraceInfo, "Trace information..");
            Trace.WriteLine(ts.TraceVerbose, "Trace verboses..");
            Console.ReadLine();
        }
    }
}
