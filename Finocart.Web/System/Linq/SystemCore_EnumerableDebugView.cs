using Microsoft.AspNetCore.Http;

namespace System.Linq
{
    public class SystemCore_EnumerableDebugView<T>
    {
        public IRequestCookieCollection cookie;

        public SystemCore_EnumerableDebugView(IRequestCookieCollection cookie)
        {
            this.cookie = cookie;
        }
    }
}