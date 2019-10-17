using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleOfEngine_Scheduler
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                IUpdateRuleOfEngine roleOfEngine = new UpdateRuleOfEngine();
                ISendEmailAndNotification sendEmailAndNotification = new SendEmailAndNotification();

                sendEmailAndNotification.GetRemainingAvailLimit();
                sendEmailAndNotification.GetInvoice();
                roleOfEngine.GetCompany();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
