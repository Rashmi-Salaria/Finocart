using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelUpload.Scheduler
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //    UploadExcelDetails uploadExcelDetails = new UploadExcelDetails();

        //    IEnumerable<UploadExcelPath> objDatawithSP = uploadExcelDetails.GetExcelPathsDetails();

        //    if (objDatawithSP.ElementAt(0).Name == "Vendor")
        //    {
        //        uploadExcelDetails.UploadVendors(objDatawithSP.ElementAt(0).Path, objDatawithSP.ElementAt(0).CompanyID, objDatawithSP.ElementAt(0).FileName, objDatawithSP.ElementAt(0).ID, objDatawithSP.ElementAt(0).CompanyName);
        //    }
        //    else
        //    {
        //        uploadExcelDetails.UploadInvoice(objDatawithSP.ElementAt(0).Path, Convert.ToInt16(objDatawithSP.ElementAt(0).CompanyID), objDatawithSP.ElementAt(0).FileName, objDatawithSP.ElementAt(0).ID);
        //    }
        //    //using (StreamWriter sw = File.AppendText(@"E:\txt.txt"))
        //    //{
        //    //    sw.WriteLine("Deleted a list with id:at" + DateTime.Now.ToString());
        //    //    sw.Close();
        //    //}
        //}

        public static void Main(string[] args)
        {
           string path = ConfigurationManager.AppSettings["UploadExcelPath"];
            if (Directory.Exists(path))
            {
                // This path is a directory
                ProcessDirectory(path);
            }
            else
            {
                Console.WriteLine("{0} is not a valid file or directory.", path);
            }
            
        }

        public static void ProcessDirectory(string targetDirectory)
        {
            
            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            //string[] FilesName = Directory.GetF
            try
            {
                foreach (string fileName in fileEntries)
                {
                    //ProcessFile(fileName);
                    string FileName = Path.GetFileName(fileName);
                    string[] ExcelNameSplit  = FileName.Split('_');
                    UploadExcelDetails uploadExcelDetails = new UploadExcelDetails();

                    //IEnumerable<UploadExcelPath> objDatawithSP = uploadExcelDetails.GetExcelPathsDetails();

                    if (ExcelNameSplit[0] == "Vendor")
                    {
                        uploadExcelDetails.UploadVendors(fileName, Convert.ToInt64(ExcelNameSplit[3].Split('.')[0]), FileName, ExcelNameSplit[1]);
                    }
                    else if(ExcelNameSplit[0] == "Invoice")
                    {
                        uploadExcelDetails.UploadInvoice(fileName, Convert.ToInt16(ExcelNameSplit[3].Split('.')[0]), FileName, ExcelNameSplit[1]);
                    }
                    if (File.Exists(fileName))
                    {
                        File.Delete(fileName);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Insert logic for processing found files here.
        //public static void ProcessFile(string path)
        //{
        //    Console.WriteLine("Processed file '{0}'.", path);
        //}

    }
}
