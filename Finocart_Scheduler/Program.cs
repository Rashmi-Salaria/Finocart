using System;
using System.Collections.Generic;
using Finocart_Scheduler.Microsoft;

namespace Finocart_Scheduler
{
    public class Program
    {
        
        public static void Main(string[] args)
        {
            Logger logger = new Logger();
            FinocartEntities finocartEntities = new FinocartEntities();
            //Console.WriteLine("line1");
            //Console.ReadKey();
            try
            {
                IRoleOfEngine roleOfEngine = new RoleOfEngine();
                ClsCommon clsCommon = new ClsCommon();
                //IMsVendor msVendor = new MsVendor();
                //msVendor.SaveVendor();
                //GetCombination(new List<int> { 1, 2, 3});
                var HolidayList = roleOfEngine.HolidaysList();
                bool holiday = HolidayList.Contains(DateTime.Today.Date);
                if ((DateTime.Today.DayOfWeek != DayOfWeek.Saturday && DateTime.Today.DayOfWeek != DayOfWeek.Sunday && holiday == false))
                {
                    //Console.WriteLine("line2");
                    //Console.ReadKey();
                    var Company = roleOfEngine.LimUnLimCompany();
                    decimal result = 0;
                    bool result1 = false;
                    if (Company != null)
                    {
                        foreach (var company in Company)
                        {
                            decimal totalApprovalAmt = 0;
                            if (company.InvoiceLimitAmt != null)
                            {
                                var PaymentDates = roleOfEngine.LimitPaymentDate(company.CompanyID);
                                foreach (var PaymentDate in PaymentDates)
                                {
                                        if (PaymentDate.FinoLimUnLim == "FinoLim")
                                        {
                                            DateTime PaymentDate1 = clsCommon.GetPrevDate(Convert.ToDouble(PaymentDate.InvoiceApprovePayDays), PaymentDate.PaymentDate);
                                            if (DateTime.Now.Date == PaymentDate1)
                                            {
                                                result = roleOfEngine.InvoiceLimitApproval(PaymentDate.AnchorCompID, PaymentDate.InvoiceID, Convert.ToDouble(PaymentDate.InvoiceApprovePayDays), totalApprovalAmt);
                                                totalApprovalAmt = result;
                                            }
                                        }
                                        else
                                        {
                                            result = roleOfEngine.InvoiceLimitApproval(PaymentDate.AnchorCompID, PaymentDate.InvoiceID, Convert.ToDouble(PaymentDate.InvoiceApprovePayDays), totalApprovalAmt);
                                            totalApprovalAmt = result;
                                        }
                                } 
                                
                                   roleOfEngine.UpdateCompany(company.CompanyID, totalApprovalAmt);
                            }
                            else
                            {
                                result1 = roleOfEngine.InvoiceUnLimApproval(company.CompanyID);
                            }
                        }
                    }
                    
                }

            }
            catch (Exception ex)
            {
                logger.WriteLog(ex);
            }
           
        }
    }
}
