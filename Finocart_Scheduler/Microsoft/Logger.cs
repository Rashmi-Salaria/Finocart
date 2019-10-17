using System;
using System.IO;
using System.Configuration;

namespace Finocart_Scheduler.Microsoft
{
    public class Logger
    {

        /// <summary>
        /// Write the log
        /// </summary>
        /// <param name="logMessage">Log message to be written to log file</param>
        public void WriteLog(string logMessage, string type)
        {
            try
            {

                string filePath = string.Empty;
                if (type == "info")
                {
                    filePath = ConfigurationManager.AppSettings["InfoFile"];
                }
                else if (type == "debug")
                {
                    filePath = ConfigurationManager.AppSettings["DebugFile"];
                }
                string fileName = DateTime.Today.ToString("dd MMMM yyyy") + ".txt";
                string logFile = Path.Combine(filePath, fileName);
                File.AppendAllText(logFile, Environment.NewLine);
                File.AppendAllText(logFile, Environment.NewLine);
                File.AppendAllText(filePath, DateTime.Now + " " + logMessage + Environment.NewLine);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Method to log error details
        /// Date: 20 June 2019
        /// Developer: Shreyans Khandelwal (0538)
        /// </summary>
        /// <param name="ex"></param>
        public void WriteLog(Exception ex)
        {
            string filePath = string.Empty;
            filePath = ConfigurationManager.AppSettings["ErrorFile"];

            string fileName = DateTime.Today.ToString("dd MMMM yyyy") + ".txt";
            string logFile = Path.Combine(filePath, fileName);
            File.AppendAllText(logFile, Environment.NewLine);
            File.AppendAllText(logFile, Environment.NewLine);
            File.AppendAllText(logFile, DateTime.Now + "  Message: " + ex.Message + Environment.NewLine);
            if (ex.InnerException != null)
                File.AppendAllText(logFile, DateTime.Now + "  InnerException: " + ex.InnerException + Environment.NewLine);
            File.AppendAllText(logFile, DateTime.Now + "  StackTrace: " + ex.StackTrace + Environment.NewLine);
        }

        public void update(string amountpaid,string discountamount)
        {
            string filePath = string.Empty;
            filePath = ConfigurationManager.AppSettings["ErrorFile"];

            string fileName = DateTime.Today.ToString("dd MMMM yyyy") + ".txt";
            string logFile = Path.Combine(filePath, fileName);
            File.AppendAllText(logFile, Environment.NewLine);
            File.AppendAllText(logFile, Environment.NewLine);
            File.AppendAllText(logFile, DateTime.Now + " " + amountpaid + " " + discountamount + Environment.NewLine);
            //File.AppendAllText(logFile, DateTime.Now + "  Message: " + e.Message + Environment.NewLine);
            //if (p.InnerException != null)
            //    File.AppendAllText(logFile, DateTime.Now + "  InnerException: " + ex.InnerException + Environment.NewLine);
            //File.AppendAllText(logFile, DateTime.Now + "  StackTrace: " + ex.StackTrace + Environment.NewLine);
        }
    }
}
