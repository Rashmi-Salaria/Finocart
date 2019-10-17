using Finocart.Model;

namespace Finocart.Interface
{
    public interface IFinoCartMaster
    {
        /// <summary>
        /// Validate login
        /// </summary>
        /// <param name="EmailId"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        FinocartMaster ValidateLogin(string EmailId, string Password);
    }
}
