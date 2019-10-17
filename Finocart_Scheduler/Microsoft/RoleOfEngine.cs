using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Finocart_Scheduler.Microsoft
{
    public interface IRoleOfEngine
    {
        //bool InvoiceAutoApproval();
        dynamic LimitPaymentDate(Int64 CompanyID);
        decimal? InvoiceLimitApproval(Int64? AnchorID, Int64? InvoiceID, double? NoOfDays, decimal? totalApprovalAmt);
        dynamic LimUnLimCompany();
        bool InvoiceUnLimApproval(Int64 CompanyID);
        dynamic HolidaysList();
        bool UpdateCompany(Int64 CompanyID, decimal? totalApprovalAmt);
    }
    public class RoleOfEngine : IRoleOfEngine
    {


        Logger logger = new Logger();
        FinocartEntities finocartEntities = new FinocartEntities();
        

        /// <summary>
        /// Invoice Autho approval system
        /// Date: 24 June 2019
        /// Developer: Shreyans Khandelwal (0538)
        /// </summary>
        /// <returns></returns>
        //public bool InvoiceAutoApproval()
        //{
        //    //Console.WriteLine("line3");
        //    //Console.ReadKey();
        //    try
        //    {

        //        int? typeAnchor = Convert.ToInt32(ClsConstants.AccessType.Anchor);
        //        int? typeBoth = Convert.ToInt32(ClsConstants.AccessType.Both);
        //        //getting list of all anchors
        //        var anchors = (from data in finocartEntities.Company
        //                       where (data.InterestedAs == typeAnchor
        //                       || data.InterestedAs == typeBoth)
        //                       select data).ToList();
        //        //Console.WriteLine("line4");
        //        //Console.ReadKey();
        //        foreach (var anchor in anchors)
        //        {
        //            decimal? totalApprovalAmt = 0;
        //            DateTime paymentDate;
        //            double noOfDays;
        //            int? statusPending = Convert.ToInt32(ClsConstants.Status.Pending);
        //            ClsCommon clsCommon = new ClsCommon();
        //            if (anchor.NoOfDays != null)
        //                noOfDays = Convert.ToDouble(anchor.NoOfDays);
        //            else
        //                noOfDays = 2;

        //            //getting last day of payment for anchor
        //            paymentDate = clsCommon.GetNextDate(noOfDays);
        //            //getting all invoices of anchor 
               
        //                var invoices = (from data in finocartEntities.Invoice
        //                                join invoiceBucket in finocartEntities.InvoiceBucket on data.InvoiceID equals invoiceBucket.InvoiceID
        //                                join bucket in finocartEntities.BucketManagement on invoiceBucket.BucketID equals bucket.BucketID
        //                                //where data.Discount >= anchor.PercentageRate && data.InvoiceStatus == statusPending
        //                                where data.Discount != null && data.InvoiceStatus == statusPending
        //                                // && DbFunctions.TruncateTime(data.PaymentDate) == DbFunctions.TruncateTime(paymentDate.Date)
        //                                && data.AnchorCompID == anchor.CompanyID && data.FinoLimUnLim != "FinoLim"
        //                                //orderby bucket.Discount
        //                                orderby data.PaymentDate ascending
        //                                select new
        //                                {
        //                                    data.InvoiceID,
        //                                    data.ApprovedAmt,
        //                                    data.PaymentDueDate,
        //                                    data.Discount,
        //                                    bucket.ValidUptoDate,
        //                                    data.VendorID
        //                                }).ToList();

        //                //Console.WriteLine("line5");
        //                //Console.ReadKey();
        //                decimal? totalAvailAmount = anchor.InvoiceLimitAmt;
        //                var rates = (from data in invoices orderby data.Discount select data.Discount).Distinct();
        //                foreach (var rate in rates)
        //                {
        //                    var rateInvoice = (from data in invoices where data.Discount == rate orderby data.ApprovedAmt select data).ToList();
        //                    foreach (var invoice in rateInvoice)
        //                    {
        //                        bool sendNotification = false;
        //                        int differenceDays = Convert.ToInt32((Convert.ToDateTime(invoice.PaymentDueDate).Date - Convert.ToDateTime(paymentDate).Date).TotalDays);
        //                        decimal discountAmount;
        //                        if (differenceDays > 0)

        //                            discountAmount = Convert.ToDecimal((differenceDays * invoice.Discount * invoice.ApprovedAmt) / 36500);
        //                        else
        //                            discountAmount = 0;

        //                        decimal amtAfterDiscount = Convert.ToDecimal(invoice.ApprovedAmt - discountAmount);

        //                        if (totalAvailAmount != null)
        //                        {
        //                            totalApprovalAmt = +amtAfterDiscount;
        //                            if (totalAvailAmount >= totalApprovalAmt)
        //                                sendNotification = UpdateInvoice(invoice.InvoiceID, amtAfterDiscount, discountAmount, paymentDate);
        //                            else
        //                                totalApprovalAmt = totalApprovalAmt - amtAfterDiscount;
        //                        }
        //                        else
        //                            sendNotification = UpdateInvoice(invoice.InvoiceID, amtAfterDiscount, discountAmount, paymentDate);

        //                        if (sendNotification)
        //                            clsCommon.SendNotification(Convert.ToInt32(invoice.VendorID), "VendorBillApproval", Convert.ToInt32(invoice.InvoiceID), amtAfterDiscount);

        //                        //Console.WriteLine("line6");
        //                        //Console.ReadKey();

        //                    }
        //                }
        //        }

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.WriteLog(ex);
        //        return false;
        //    }
        //}

        public bool InvoiceUnLimApproval(Int64 CompanyID)
        {
            //Console.WriteLine("line3");
            //Console.ReadKey();
            try
            {

                int? typeAnchor = Convert.ToInt32(ClsConstants.AccessType.Anchor);
                int? typeBoth = Convert.ToInt32(ClsConstants.AccessType.Both);
                //getting list of all anchors
               
                //Console.WriteLine("line4");
                //Console.ReadKey();
                    //decimal? totalApprovalAmt = 0;
                    DateTime paymentDate;
                    double noOfDays;
                    int? statusPending = Convert.ToInt32(ClsConstants.Status.Pending);
                    ClsCommon clsCommon = new ClsCommon();
                    noOfDays = Convert.ToDouble((from data in finocartEntities.Company where data.CompanyID == CompanyID select data.PaymentDays).FirstOrDefault());
                    if (noOfDays != 0)
                        noOfDays = Convert.ToDouble(noOfDays);
                    else
                        noOfDays = 2;

                    //getting last day of payment for anchor
                    paymentDate = clsCommon.GetNextDate(noOfDays);
                    //getting all invoices of anchor 

                    var invoices = (from data in finocartEntities.Invoice
                                    join invoiceBucket in finocartEntities.InvoiceBucket on data.InvoiceID equals invoiceBucket.InvoiceID
                                    join bucket in finocartEntities.BucketManagement on invoiceBucket.BucketID equals bucket.BucketID
                                    //where data.Discount >= anchor.PercentageRate && data.InvoiceStatus == statusPending
                                    where data.Discount != null && data.InvoiceStatus == statusPending 
                                    //&& data.Discount >= (from data in finocartEntities.Company where data.CompanyID == CompanyID select data.PercentageRate).FirstOrDefault()
                                    // && DbFunctions.TruncateTime(data.PaymentDate) == DbFunctions.TruncateTime(paymentDate.Date)
                                    && data.AnchorCompID == CompanyID && data.FinoLimUnLim!= "FinoLim"
                                    //orderby bucket.Discount
                                    orderby data.PaymentDate ascending
                                    select new
                                    {
                                        data.InvoiceID,
                                        data.ApprovedAmt,
                                        data.PaymentDueDate,
                                        data.Discount,
                                        bucket.ValidUptoDate,
                                        data.VendorID,
                                        data.PaymentDate,
                                        data.FinoLimUnLim
                                    }).ToList();

                //Console.WriteLine("line5");
                //Console.ReadKey();
                    decimal? totalAvailAmount = (from data in finocartEntities.Company where data.CompanyID == CompanyID select data.InvoiceLimitAmt).FirstOrDefault();
                    bool? IsUnLimited = (from data in finocartEntities.Company where data.CompanyID == CompanyID select data.IsLimitUnlimited).FirstOrDefault();
                    var rates = (from data in invoices orderby data.Discount select data.Discount).Distinct();
                    foreach (var rate in rates)
                    {
                        var rateInvoice = (from data in invoices where data.Discount == rate orderby data.ApprovedAmt select data).ToList();
                        foreach (var invoice in rateInvoice)
                        {
                           int differenceDays = 0;
                           bool sendNotification = false;
                           if (invoice.FinoLimUnLim != null)
                           {
                            differenceDays = Convert.ToInt32((Convert.ToDateTime(invoice.PaymentDueDate).Date - Convert.ToDateTime(invoice.PaymentDate).Date).TotalDays);
                           }
                           else
                           {
                            differenceDays = Convert.ToInt32((Convert.ToDateTime(invoice.PaymentDueDate).Date - Convert.ToDateTime(paymentDate).Date).TotalDays);
                           }
                            decimal discountAmount;
                            if (differenceDays > 0)

                                discountAmount = Convert.ToDecimal((differenceDays * invoice.Discount * invoice.ApprovedAmt) / 36500);
                            else
                                discountAmount = 0;

                            decimal amtAfterDiscount = Convert.ToDecimal(invoice.ApprovedAmt - discountAmount);

                        //if (totalAvailAmount != null)
                        //{
                        //    totalApprovalAmt = +amtAfterDiscount;
                        //    if (totalAvailAmount >= totalApprovalAmt)
                        //        sendNotification = UpdateInvoice(invoice.InvoiceID, amtAfterDiscount, discountAmount, paymentDate);
                        //    else
                        //        totalApprovalAmt = totalApprovalAmt - amtAfterDiscount;
                        //}
                        //else
                        if (invoice.Discount >= (from data in finocartEntities.Company where data.CompanyID == CompanyID select data.PercentageRate).FirstOrDefault())
                        {
                            sendNotification = UpdateInvoice(invoice.InvoiceID, amtAfterDiscount, discountAmount, paymentDate, Convert.ToInt32(ClsConstants.Status.Approved), totalAvailAmount, IsUnLimited);
                        }
                        else
                        {
                            sendNotification = UpdateInvoice(invoice.InvoiceID, 0, 0, paymentDate, Convert.ToInt32(ClsConstants.Status.Rejected), totalAvailAmount, IsUnLimited);
                        }

                            if (sendNotification)
                                clsCommon.SendNotification(Convert.ToInt32(invoice.VendorID), "VendorBillApproval", Convert.ToInt32(invoice.InvoiceID), amtAfterDiscount);

                            //Console.WriteLine("line6");
                            //Console.ReadKey();

                        }
                    }

                return true;
            }
            catch (Exception ex)
            {
                logger.WriteLog(ex);
                return false;
            }
        }

        public decimal? InvoiceLimitApproval(Int64? AnchorID, Int64? InvoiceID, double? NoOfDays, decimal? totalApprovalAmt)
        {
            //Console.WriteLine("line3");
            //Console.ReadKey();
            try
            {

                //getting list of all anchors
                var anchors = (from data in finocartEntities.Company
                               where data.CompanyID == AnchorID 
                               select data).ToList();
                //Console.WriteLine("line4");
                //Console.ReadKey();
                var PercentageRate = anchors[0].PercentageRate;
                    //decimal? totalApprovalAmt = 0;
                    DateTime paymentDate;
                    double noOfDays;
                    int? statusPending = Convert.ToInt32(ClsConstants.Status.Pending);
                    ClsCommon clsCommon = new ClsCommon();
                    if (NoOfDays != null)
                        noOfDays = Convert.ToDouble(NoOfDays);
                    else
                        noOfDays = 2;

                    //getting last day of payment for anchor
                    paymentDate = clsCommon.GetNextDate(noOfDays);
                    //getting all invoices of anchor                     
                        var invoices = (from data in finocartEntities.Invoice
                                        join invoiceBucket in finocartEntities.InvoiceBucket on data.InvoiceID equals invoiceBucket.InvoiceID
                                        join bucket in finocartEntities.BucketManagement on invoiceBucket.BucketID equals bucket.BucketID
                                        //where data.Discount >= anchor.PercentageRate && data.InvoiceStatus == statusPending
                                        where data.Discount != null && data.InvoiceStatus == statusPending
                                        //&& data.Discount >= PercentageRate
                                        && data.AnchorCompID == AnchorID && data.InvoiceID == InvoiceID
                                        orderby bucket.Discount
                                        select new
                                        {
                                            data.InvoiceID,
                                            data.ApprovedAmt,
                                            data.PaymentDueDate,
                                            data.Discount,
                                            bucket.ValidUptoDate,
                                            data.VendorID,
                                            data.PaymentDate
                                        }).ToList();

                //Console.WriteLine("line5");
                //Console.ReadKey();
                bool sendNotification = false;
                bool? IsUnLimited = anchors[0].IsLimitUnlimited;
                decimal? totalAvailAmount = 0;
                if (anchors[0].BankLimit == null)
                {
                    totalAvailAmount = anchors[0].InvoiceLimitAmt;
                }
                else
                {
                    totalAvailAmount = anchors[0].InvoiceLimitAmt + anchors[0].BankLimit;
                }
                if (invoices.ElementAt(0).Discount >= PercentageRate)
                {       
                    var rates = (from data in invoices orderby data.Discount select data.Discount).Distinct();
                    foreach (var rate in rates)
                    {
                        var rateInvoice = (from data in invoices where data.Discount == rate orderby data.ApprovedAmt select data).ToList();
                        foreach (var invoice in rateInvoice)
                        {
                            
                            int differenceDays = Convert.ToInt32((Convert.ToDateTime(invoice.PaymentDueDate).Date - Convert.ToDateTime(paymentDate).Date).TotalDays);
                            decimal discountAmount;
                            if (differenceDays > 0)

                                discountAmount = Convert.ToDecimal((differenceDays * invoice.Discount * invoice.ApprovedAmt) / 36500);
                            else
                                discountAmount = 0;

                            decimal amtAfterDiscount = Convert.ToDecimal(invoice.ApprovedAmt - discountAmount);

                            if (totalAvailAmount != null)
                            {
                                totalApprovalAmt += Math.Round(amtAfterDiscount);
                                if (totalAvailAmount >= totalApprovalAmt)
                                    sendNotification = UpdateInvoice(invoice.InvoiceID, amtAfterDiscount, discountAmount, paymentDate, Convert.ToInt32(ClsConstants.Status.Approved), totalAvailAmount, IsUnLimited);
                                else
                                {
                                    totalApprovalAmt = totalApprovalAmt - Math.Round(amtAfterDiscount);
                                    sendNotification = UpdateInvoice(invoice.InvoiceID, 0, 0, paymentDate, Convert.ToInt32(ClsConstants.Status.Rejected), totalAvailAmount, IsUnLimited);
                                }
                            }

                            

                            //Console.WriteLine("line6");
                            //Console.ReadKey();

                        }
                    }
                }
                else
                {
                    sendNotification = UpdateInvoice(InvoiceID, 0, 0, paymentDate, Convert.ToInt32(ClsConstants.Status.Rejected), totalAvailAmount, IsUnLimited);
                }

                if (sendNotification)
                                clsCommon.SendNotification(Convert.ToInt32(invoices.ElementAt(0).VendorID), "VendorBillApproval", Convert.ToInt32(InvoiceID), 0);
                return totalApprovalAmt;
            }
            catch (Exception ex)
            {
                logger.WriteLog(ex);
                return totalApprovalAmt;
            }
        }

        /// <summary>
        /// Updating invoice status and payment date
        /// on accepting discount
        /// Date: 25 June 2019
        /// Developer: Shreyans Khandelwal (0538)
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param name="paymentDate"></param>
        /// <returns></returns>
        private bool UpdateInvoice(long? invoiceId, decimal amountPaid, decimal discountAmount,DateTime paymentdate, Int32 InvoiceStatus, decimal? AvailableLimits, bool? IsUnLimited)
        {
            try
            {

                var invoice = (from data in finocartEntities.Invoice where data.InvoiceID == invoiceId select data).FirstOrDefault();
                invoice.InvoiceStatus = InvoiceStatus;
                if (invoice.FinoLimUnLim != "FinoLim" && invoice.FinoLimUnLim != "FinoUnLim")
                {
                    invoice.PaymentDate = paymentdate;
                }
                invoice.AmountPaid = Math.Round(amountPaid);
                invoice.Earning = Math.Round(discountAmount);
                invoice.InvoiceApprovalDate = DateTime.Now.Date;
                if(IsUnLimited != true && AvailableLimits!=null)
                {
                    invoice.Limits = AvailableLimits.ToString();
                }
                else
                {
                    invoice.Limits = "Unlimited";
                }
                
                finocartEntities.SaveChanges();


                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic LimitPaymentDate(Int64 CompanyID)
        {
            ClsCommon clsCommon = new ClsCommon();
            DateTime paymentDate;
            int? statusPending = Convert.ToInt32(ClsConstants.Status.Pending);
            var Invoices = (from data in finocartEntities.Invoice where data.AnchorCompID == CompanyID && data.Discount!=null && data.InvoiceStatus == statusPending select data).ToList();
            
            foreach (var invoice in Invoices)
            {
                paymentDate = clsCommon.GetNextDate(Convert.ToDouble(invoice.InvoiceApprovePayDays));
                int differenceDays = Convert.ToInt32((Convert.ToDateTime(invoice.PaymentDueDate).Date - Convert.ToDateTime(invoice.PaymentDate).Date).TotalDays);
                decimal discountAmount;
                if (differenceDays > 0)

                    invoice.Earning =Math.Round(Convert.ToDecimal((differenceDays * invoice.Discount * invoice.ApprovedAmt) / 36500));
                else
                    discountAmount = 0;

                invoice.AmountPaid = Math.Round(Convert.ToDecimal(invoice.ApprovedAmt - invoice.Earning));
            }
            var PaymentDate =(from data in Invoices where data.AnchorCompID == CompanyID orderby data.Earning descending select data).ToList();
            return PaymentDate;
        }

        public dynamic LimUnLimCompany()
        {
            int? typeAnchor = Convert.ToInt16((from data in finocartEntities.LookupDetails where data.LookupValue == "Anchor Company" select data.ID ).FirstOrDefault());
            int? typeBoth = Convert.ToInt16((from data in finocartEntities.LookupDetails where data.LookupValue == "Both" select data.ID).FirstOrDefault());
            var Company =(from data in finocartEntities.Company where (data.InterestedAs == typeAnchor || data.InterestedAs == typeBoth)  select data).ToList();
            return Company;
        }

        public dynamic HolidaysList()
        {
            var HolidayList = (from data in finocartEntities.HolidayList select data.Date).ToList();
            return HolidayList;
        }

        public bool UpdateCompany(Int64 CompanyID, decimal? totalApprovalAmt)
        {
            try
            {

                var company = (from data in finocartEntities.Company where data.CompanyID == CompanyID select data).FirstOrDefault();
                //var setBankAmountLimit = (from data in finocartEntities.SetBankAmountLimit where ) 
                if (totalApprovalAmt >= company.InternalFundLimit)
                {
                    company.BankLimit = (company.InternalFundLimit + company.BankLimit) - totalApprovalAmt;
                    company.InternalFundLimit = 0;
                }
                else
                {
                    company.InternalFundLimit = company.InternalFundLimit - totalApprovalAmt;
                }                

                finocartEntities.SaveChanges();


                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
