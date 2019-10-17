using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.IO;
using Finocart.SAPService.Common.SQLTransaction;

namespace Finocart.SAPService
{
    public partial class Service1 : ServiceBase
    {
        Timer timer = new Timer();
        InvoiceDet objInvoiceDet = new InvoiceDet();
        VendorDet objVendorDet = new VendorDet();
        VendorDetModel VendorDetModel = new VendorDetModel();
        SQLServerTransaction SQLServerTransaction = new SQLServerTransaction();
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            WriteToFile("Service is started at " + DateTime.Now);
            //timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            //timer.Interval = 5000; //number in milisecinds  
            //timer.Enabled = true;
            objInvoiceDet.GetInvoiceValue("1000000020", "2013");
            VendorDetModel = objVendorDet.GetVendorsList();
            var result=SQLServerTransaction.InsertVendorDetailsInSQLTable(VendorDetModel);
        }

        protected override void OnStop()
        {
        }

       
        //private void OnElapsedTime(object source, ElapsedEventArgs e)
        //{
        //    WriteToFile("Service is recall at " + DateTime.Now);
        //}

        public void WriteToFile(string Message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!File.Exists(filepath))
            {
                // Create a file to write to.   
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
        }
    }
}
