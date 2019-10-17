using Finocart.Model;
using System.Collections.Generic;

namespace Finocart.Interface
{
    public interface ILookupDetails
    {
        /// <summary>
        /// Get Lookup details by Key
        /// </summary>
        /// <param name="LookupKey"></param>
        /// <returns></returns>
        IEnumerable<LookupDetails> getLookupDetailByKey(string LookupKey);

        IEnumerable<GetUserMailTemplate> getUserMailTemplate(int? AccessViewId = 0);

        IEnumerable<GetForgetPasswordMailTemplate> getForgetPasswordMailTemplate();


        IEnumerable<GetEmailId> GetEmailId(int ID, string RoleName);
    }
}
