using Microsoft.AspNetCore.Builder;

namespace Finocart.Web.Controllers
{
    /// <summary>
    /// Requesr handler middelware extension
    /// </summary>
    public static class RequestMiddlewareExtensions
    {
        #region Static method
        public static IApplicationBuilder RequestHanlderMiddleware
                                      (this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestHanlderMiddleware>();
        }
        #endregion
    }
}
