using Serilog;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Utilities
{
    public static class LoggerConfigurationManager
    {
        private static readonly ILogger Logger;

        static LoggerConfigurationManager()
        {
            Logger = new LoggerConfiguration()
                .Enrich.With(new ThreadIDEnricher())
                .WriteTo.File("log.log", outputTemplate: "{Timestamp:HH:mm:ss} ({ThreadID}) {Message:lj}{NewLine}{Exception}")              
                .CreateLogger();
        }

        public static ILogger GetLogger()
        {
            return Logger;
        }
    }

    public class ThreadIDEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(
              "ThreadID", Thread.CurrentThread.ManagedThreadId.ToString("D4")));
        }
    }
}
