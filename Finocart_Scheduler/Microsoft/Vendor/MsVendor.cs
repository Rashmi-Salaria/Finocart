using System;
using System.Linq;
using Finocart_Scheduler.Model;
//using Finocart_Scheduler.SAP;

namespace Finocart_Scheduler.Microsoft
{
    public class MsVendor : IMsVendor
    {
        FinocartEntities finocartEntities = new FinocartEntities();
        //ISAPVendor objSapVendor = new SAPVendor();
        Logger logger = new Logger();

        /// <summary>
        /// Saving vendor from SAP to Database
        /// Date: 20 June 2019
        /// Developer: Shreyans Khandelwal (0538)
        /// </summary>
        public void SaveVendor()
        {
            try
            {

                VendorDetail vendorDetail = new VendorDetail();
                //vendorDetail = objSapVendor.GetVendor();
                vendorDetail.PanNumber = "0192873661";
                int venId = 0;
                bool addAssoCompany = false;

                //allowing to add vendor only if PAN number is available
                if (!string.IsNullOrEmpty(vendorDetail.PanNumber))
                {
                    //checking if vendor already exist on basis of PAN number
                    var vendorId = (from data in finocartEntities.Company where data.Pan_number == vendorDetail.PanNumber select data.CompanyID).FirstOrDefault();
                    
                    //if vendor exist linking it to new company
                    if (vendorId > 0)
                    {
                        var vendorAssociate = (from data in finocartEntities.VendorAssociatedCompany where data.VendorId == vendorId && data.CompanyId == 5678 select data).FirstOrDefault();
                        if (vendorAssociate == null)
                        {
                            venId = vendorId;
                            addAssoCompany = true;
                        }
                    }
                    //If vendor does not exist adding it to DB
                    else
                    {
                        vendorDetail.InstretedAs = (from data in finocartEntities.LookupDetails
                                                    where data.LookupValue == "Vendor" && data.LookupFor == "AccessView"
                                                    select data.ID).FirstOrDefault();
                        Company company = MapCompany(vendorDetail);
                        finocartEntities.Company.Add(company);
                        finocartEntities.SaveChanges();
                        addAssoCompany = true;
                        venId = company.CompanyID;

                    }
                    //Linking vendor to the company
                    if (addAssoCompany)
                    {
                        if (!finocartEntities.VendorAssociatedCompany.Where(data => data.VendorId == venId && data.CompanyId == 57).Any())
                        {
                            VendorAssociatedCompany vendorAssociatedCompany = new VendorAssociatedCompany();
                            vendorAssociatedCompany.VendorId = venId;
                            vendorAssociatedCompany.CompanyId = 57;

                            finocartEntities.VendorAssociatedCompany.Add(vendorAssociatedCompany);
                            finocartEntities.SaveChanges();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                logger.WriteLog(ex);
                throw ex;
            }
        }

        /// <summary>
        /// Mapping vendor details from SAP to 
        /// DB company table object
        /// </summary>
        /// <param name="vendorDetail"></param>
        /// <returns></returns>
        public Company MapCompany(VendorDetail vendorDetail)
        {
            try
            {
                
                Company company = new Company();

                company.Company_name = vendorDetail.CompanyName;
                company.Password = vendorDetail.Password;
                company.InterestedAs = vendorDetail.InstretedAs;
                company.Pan_number = vendorDetail.PanNumber;
                company.Contact_Name = vendorDetail.ContactName;
                company.Contact_Designation = vendorDetail.ContactDesignation;
                company.Contact_email = vendorDetail.ContactEmail;
                company.Contact_mobile = vendorDetail.ContactMobile;
                company.CreatedBy = 56789;
                return company;

            }
            catch (Exception ex)
            {
                logger.WriteLog(ex);
                throw ex;
            }
        }
    }
}
