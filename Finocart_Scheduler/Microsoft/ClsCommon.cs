using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finocart_Scheduler.Microsoft
{
    public class ClsCommon
    {
        ClsConstants clsConstants = new ClsConstants();
        FinocartEntities finocartEntities = new FinocartEntities();

        /// <summary>
        /// Date addition and skipping date
        /// in case of Sat and Sun
        /// Date: 25 June 2019
        /// Developer: Shreyans Khandelwal(0538)
        /// </summary>
        /// <param name="days"></param>
        /// <returns></returns>
        public DateTime GetNextDate(double days)
        {
            try
            {
                DateTime result = DateTime.Today;
                var HolidayList = (from data in finocartEntities.HolidayList select data.Date).ToList();
                for (int i = 1; i <= days; i++)
                {
                    result = result.AddDays(1);
                    if (result.DayOfWeek == DayOfWeek.Saturday)
                        result = result.AddDays(2);
                    else if (result.DayOfWeek == DayOfWeek.Sunday)
                        result = result.AddDays(1);
                    for (int j = 0; j < HolidayList.Count(); j++)
                    {
                        if (Convert.ToDateTime(HolidayList[j]).Date == result.Date && result.DayOfWeek != DayOfWeek.Saturday && result.DayOfWeek != DayOfWeek.Sunday)
                        {
                            //result = result.AddDays(1);
                            i--;
                            break;
                        }
                    }
                    
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DateTime GetPrevDate(double days, DateTime PaymentDate)
        {
            try
            {
                DateTime result = PaymentDate;
                var HolidayList = (from data in finocartEntities.HolidayList select data.Date).ToList();
                for (int i = 1; i <= days; i++)
                {
                    result = result.AddDays(-1);
                    if (result.DayOfWeek == DayOfWeek.Sunday)
                        result = result.AddDays(-2);
                    else if (result.DayOfWeek == DayOfWeek.Saturday)
                        result = result.AddDays(-1);
                    for (int j = 0; j < HolidayList.Count(); j++)
                    {
                        if (Convert.ToDateTime(HolidayList[j]).Date == result.Date && result.DayOfWeek != DayOfWeek.Saturday && result.DayOfWeek != DayOfWeek.Sunday)
                        {
                            result = result.AddDays(1);
                            break;
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Sending notification to vendor 
        /// on approval of invoice
        /// Date: 26 June 2019
        /// Developer: Shreyans Khandelwal (0538)
        /// </summary>
        /// <returns></returns>
        public bool SendNotification(int companyId, string messageType, int billId, decimal billAmount)
        {
            try
            {
                bool insert = false;
                string message = GetMessage(messageType, billId, billAmount);
                List<int> userId = GetUserForNotification(companyId, messageType);
                List<Notification> lstNotification = new List<Notification>();
                foreach (var id in userId)
                {
                    Notification notification = new Notification();
                    notification.CreatedOn = DateTime.Now;
                    notification.Description = message;
                    notification.UserID = id;
                    lstNotification.Add(notification);
                    insert = true;
                }
                if (insert)
                {
                    finocartEntities.Notification.AddRange(lstNotification);
                    finocartEntities.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Getting message as per message type
        /// Date: 26 June 2019
        /// Developer: Shreyans Khandelwal (0538)
        /// </summary>
        /// <param name="messageType"></param>
        /// <param name="billId"></param>
        /// <param name="billAmount"></param>
        /// <returns></returns>
        private string GetMessage(string messageType, int billId, decimal billAmount)
        {
            try
            {
                string message = string.Empty;
                if (messageType == "VendorBillApproval")
                {
                    message = ClsConstants.VendorBillApprovalMsg;
                    message = message.Replace("#billId", billId.ToString());
                    message = message.Replace("#amount", billAmount.ToString());
                }
                return message;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Getting list of user to send notification
        /// Date: 26 June 2019
        /// Developer: Shreyans Khandelwal (0538)
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="messageType"></param>
        /// <returns></returns>
        private List<int> GetUserForNotification(int companyId, string messageType)
        {
            try
            {
                int? vendorAccess = Convert.ToInt32(ClsConstants.AccessType.Vendor);
                int? bothAccess = Convert.ToInt32(ClsConstants.AccessType.Both);
                List<int> userList = new List<int>();
                if (messageType == "VendorBillApproval")
                {
                    userList = (from data in finocartEntities.User
                                where data.CompanyID == companyId &&
                                (data.AccessViewID == vendorAccess || data.AccessViewID == bothAccess)
                                select data.UserID).ToList();
                }
                return userList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DateTime GetNextDate(DateTime days)
        {
            try
            {
                
               // for (int i = 1; i <= days; i++)
               //
                    if (days.DayOfWeek == DayOfWeek.Saturday)
                    days = days.AddDays(2);
                    else if (days.DayOfWeek == DayOfWeek.Sunday)
                    days = days.AddDays(1);
               // }
                return days;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
