#pragma checksum "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\Template\AdminRegEmail.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c8e4ca0e5a6437e2b738fc36591a37448e999308"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Template_AdminRegEmail), @"mvc.1.0.view", @"/Views/Template/AdminRegEmail.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Template/AdminRegEmail.cshtml", typeof(AspNetCore.Views_Template_AdminRegEmail))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\_ViewImports.cshtml"
using Finocart.Web;

#line default
#line hidden
#line 2 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\_ViewImports.cshtml"
using Finocart.Web.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c8e4ca0e5a6437e2b738fc36591a37448e999308", @"/Views/Template/AdminRegEmail.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6b23c2871a53ca46bac6eeea28bffed3162ffb9c", @"/Views/_ViewImports.cshtml")]
    public class Views_Template_AdminRegEmail : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 27, true);
            WriteLiteral("\r\n<!DOCTYPE html>\r\n<html>\r\n");
            EndContext();
            BeginContext(27, 785, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c8e4ca0e5a6437e2b738fc36591a37448e9993083465", async() => {
                BeginContext(33, 772, true);
                WriteLiteral(@"
    <meta charset=""utf-8"" />
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
    <link href=""../../wwwroot/Content/bootstrap.min.css"" rel=""stylesheet"" type=""text/css"" />
    <link rel=""stylesheet"" href=""https://use.fontawesome.com/releases/v5.8.1/css/all.css"" integrity=""sha384-50oBUHEmvpQ+1lW4y57PTFmhCaXp0ML5d60M1M7uH2+nqUivzIebhndOJK28anvf"" crossorigin=""anonymous"">
    <link href=""../../wwwroot/Content/css/style.css"" rel=""stylesheet"" type=""text/css"" />
    <link href=""../../wwwroot/Content/css/responsive.css"" rel=""stylesheet"" type=""text/css"" />
    <script src=""../../wwwroot/Scripts/jquery-1.11.0.min.js""></script>
    <script src=""../../wwwroot/Scripts/bootstrap.min.js""></script>
    <style type=""text/css"">
    </style>
");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(812, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(814, 2628, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c8e4ca0e5a6437e2b738fc36591a37448e9993085455", async() => {
                BeginContext(820, 786, true);
                WriteLiteral(@"
    <div>
        <table class=""main-mail-box"" cellpadding=""0"" cellspacing=""0"" style=""margin:0;background: #fff4f4;height: 475px;width: 500px;border: 1px solid #dadbdc;"">
            <tr>
                <td style=""height: 125px;"" valign=""top""><p class=""logo-box"" style=""position:relative; margin: 40px 60px 10px;border-bottom: 1px solid #dadbdc;""><img style=""padding-bottom: 30px;width: 200px; display:block;margin:auto;"" src=""http://dotnet.brainvire.com/FinoDemo/Content/images/logo.png"" class=""img-responsive"" /></p></td>
            </tr>
            <tr>
                <td valign=""top"">
                    <h4 style=""text-align: center;font-weight: bold; margin:0;font-family: century gothic;font-size: 16px;letter-spacing: 0.5px;line-height: 1.5;"">SUBJECT :<uppercase>");
                EndContext();
                BeginContext(1607, 18, true);
                WriteLiteral("@InterestedAsRoles");
                EndContext();
                BeginContext(1626, 970, true);
                WriteLiteral(@"@</uppercase> REGISTRATION</h4>
                    <h4 style=""margin:22px 60px 0px;font-family: century gothic;font-size: 16px;letter-spacing: 0.5px;line-height: 1.5;"">Hi,<br />Welcome!</h4>
                    <p style=""font-family:century gothic;margin: 22px 60px 0px;font-size: 14px;letter-spacing: 0.5px;line-height: 1.5;"">This is to inform that you have been registered with us. We request you to please find the below link along with your credentials:</p>
                    <p style=""font-family:century gothic;margin: 22px 60px 0px;font-size: 14px;letter-spacing: 0.5px;line-height: 1.5;"">URL&nbsp;:&nbsp;<span style=""font-family: century gothic;font-weight: bolder;color: #e46734;"">##$$PAYMENT_LINK$$##</span></p>
                    <p style=""font-family:century gothic;margin: 22px 60px 0px;font-size: 14px;letter-spacing: 0.5px;line-height: 1.5;"">Username&nbsp;:&nbsp;&nbsp;<span style=""font-family: century gothic;font-weight: bolder;color: #e46734;"">");
                EndContext();
                BeginContext(2597, 5, true);
                WriteLiteral("@User");
                EndContext();
                BeginContext(2603, 261, true);
                WriteLiteral(@"@</span></p>
                    <p style=""font-family:century gothic;margin: 22px 60px 0px;font-size: 14px;letter-spacing: 0.5px;line-height: 1.5;"">Password&nbsp;&nbsp;:&nbsp;&nbsp;<span style=""font-family: century gothic;font-weight: bolder;color: #e46734;"">");
                EndContext();
                BeginContext(2865, 9, true);
                WriteLiteral("@Password");
                EndContext();
                BeginContext(2875, 560, true);
                WriteLiteral(@"@</span></p>
                    <p style=""font-family:century gothic;margin: 22px 60px 0px;font-size: 14px;letter-spacing: 0.5px;line-height: 1.5;"">We appreciate your sevice and looking forward for future business with us. Request you to please share your invoices with us on this portal.</p>
                    <p style=""font-family:century gothic;margin: 22px 60px 0px;font-size: 14px;letter-spacing: 0.5px;line-height: 1.5;margin-bottom:40px"">Thanks and Regards<br />Finocart</p>
                </td>
            </tr>
        </table>
    </div>
");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(3442, 11, true);
            WriteLiteral("\r\n</html>\r\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
