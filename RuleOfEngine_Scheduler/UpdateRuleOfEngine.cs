using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleOfEngine_Scheduler
{
    public interface IUpdateRuleOfEngine
    {
        bool GetCompany();

        bool UpdateCompany(Int64 CompanyID);
    }

    public class UpdateRuleOfEngine : IUpdateRuleOfEngine
    {
        FinocartDemo1Entities finocartEntities = new FinocartDemo1Entities();
        public bool GetCompany()
        {
            try
            {

                var company = (from data in finocartEntities.Companies select data).ToList();
                //var setBankAmountLimit = (from data in finocartEntities.SetBankAmountLimit where ) 
                foreach(var companies in company)
                {
                    UpdateCompany(companies.CompanyID);
                }
                
                //if (totalApprovalAmt >= company.InternalFundLimit)
                //{
                //    company.BankLimit = (company.InternalFundLimit + company.BankLimit) - totalApprovalAmt;
                //    company.InternalFundLimit = 0;
                //}
                //else
                //{
                //    company.InternalFundLimit = company.InternalFundLimit - totalApprovalAmt;
                //}

                finocartEntities.SaveChanges();


                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateCompany(Int64 CompanyID)
        {
            try
            {

                var company = (from data in finocartEntities.Companies where data.CompanyID == CompanyID select data).FirstOrDefault();

                company.InternalFundLimit = company.InvoiceLimitAmt;

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
