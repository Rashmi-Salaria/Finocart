using System;
using Microsoft.AspNetCore.Mvc;

namespace Finocart.Web.Controllers
{
    public class DisbursementController : Controller
    {
        /// <summary>
        /// Payment disbursement view
        /// </summary>
        /// <returns></returns>
        public IActionResult PaymentDisbursement()
        {
            try
            {
                return View("PaymentDisbursementList");
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}