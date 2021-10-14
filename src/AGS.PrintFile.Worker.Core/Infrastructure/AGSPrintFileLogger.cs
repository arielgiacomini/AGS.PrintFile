using System;
using System.IO;

namespace AGS.PrintFile.Worker.Core.Infrastructure
{
    public static class AGSPrintFileLogger
    {
        private static AGSPrintFileConfiguration _config { get; set; }

        public static void Logger(string message)
        {
            _config = AGSPrintFileConfiguration.LoadFile();

            var beginMessage = $"{DateTime.Now}";

            var messageWithDateTime = $"{beginMessage} | {message}";

            string path = $"{_config.PathLogDefault}";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string filepath = $"{_config.PathLogDefault}{_config.ServiceName}_{DateTime.Now.Date.ToShortDateString().Replace('/', '_')}.txt";

            if (!File.Exists(filepath))
            {
                // Create a file to write to.   
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(messageWithDateTime);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(messageWithDateTime);
                }
            }
        }
    }
}