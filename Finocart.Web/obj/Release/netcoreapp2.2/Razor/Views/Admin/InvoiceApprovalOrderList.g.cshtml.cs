#pragma checksum "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\Admin\InvoiceApprovalOrderList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6fcae6924d0bcc7e44712ea65b58de13effaea55"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_InvoiceApprovalOrderList), @"mvc.1.0.view", @"/Views/Admin/InvoiceApprovalOrderList.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Admin/InvoiceApprovalOrderList.cshtml", typeof(AspNetCore.Views_Admin_InvoiceApprovalOrderList))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6fcae6924d0bcc7e44712ea65b58de13effaea55", @"/Views/Admin/InvoiceApprovalOrderList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6b23c2871a53ca46bac6eeea28bffed3162ffb9c", @"/Views/_ViewImports.cshtml")]
    public class Views_Admin_InvoiceApprovalOrderList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/Content/images/plusicon.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("img-responsive"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("display: inline;"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/InvoiceApprovalOrder/InvoiceApprovalOrder.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/Common/Common.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 3, true);
            WriteLiteral("   ");
            EndContext();
#line 1 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\Admin\InvoiceApprovalOrderList.cshtml"
     
        ViewData["Title"] = "User";
        Layout = "~/Views/Shared/_Layout.cshtml";
    

#line default
#line hidden
            BeginContext(102, 44, true);
            WriteLiteral("<script>\r\n    var GetInvoiceApprovedList = \'");
            EndContext();
            BeginContext(147, 45, false);
#line 6 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\Admin\InvoiceApprovalOrderList.cshtml"
                             Write(Url.Action("GetInvoiceApprovedList", "Admin"));

#line default
#line hidden
            EndContext();
            BeginContext(192, 400, true);
            WriteLiteral(@"';
</script>
<div class=""main-inner-section"">
    <div class=""content-upper-section"">
        <div class=""row"">
            <div class=""col-md-12 col-sm-12 col-xs-12"">
                <div class=""search-box-main clearfix"">
                    <div class=""col-xs-12 col-sm-2 col-md-2 padding"">
                        <button id=""btnAddNew"" class=""adduserbutton"">
                            ");
            EndContext();
            BeginContext(592, 91, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "6fcae6924d0bcc7e44712ea65b58de13effaea556407", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(683, 1188, true);
            WriteLiteral(@"Add user
                        </button>
                    </div>
                </div>
            </div>

            <div class=""col-md-12 col-sm-12 col-xs-12"">
                <div class=""main-table-box page-scroll"">
                    <div class=""table-responsive"">
                        <table id=""tbl_InvoiceApprovedList"" class=""table table-responsive table-bordered table-hover"">
                            <thead>
                                <tr>
                                    <th style=""display:none"">User ID</th>
                                    <th style=""width:10%"">From Amount</th>
                                    <th style=""width:13%"">To Amount</th>
                                    <th style=""width:12%"">Approved By</th>
                                    <th style=""width:10%"">Users</th>
                                    <th style=""width:10%"">Actions</th>
                                </tr>
                            </thead>
                       ");
            WriteLiteral("     <tbody></tbody>\r\n                        </table>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n");
            EndContext();
            BeginContext(1871, 70, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6fcae6924d0bcc7e44712ea65b58de13effaea558999", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1941, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(1943, 42, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6fcae6924d0bcc7e44712ea65b58de13effaea5510178", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1985, 56, true);
            WriteLiteral("\r\n<script type=\"text/javascript\">\r\n    var AddResult = \'");
            EndContext();
            BeginContext(2043, 21, false);
#line 46 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\Admin\InvoiceApprovalOrderList.cshtml"
                 Write(TempData["AddResult"]);

#line default
#line hidden
            EndContext();
            BeginContext(2065, 28, true);
            WriteLiteral("\';\r\n    var UpdateResult = \'");
            EndContext();
            BeginContext(2095, 24, false);
#line 47 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\Admin\InvoiceApprovalOrderList.cshtml"
                    Write(TempData["UpdateResult"]);

#line default
#line hidden
            EndContext();
            BeginContext(2120, 29, true);
            WriteLiteral("\';\r\n    var UpdateResult1 = \'");
            EndContext();
            BeginContext(2150, 20, false);
#line 48 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\Admin\InvoiceApprovalOrderList.cshtml"
                    Write(ViewBag.UpdateResult);

#line default
#line hidden
            EndContext();
            BeginContext(2170, 26, true);
            WriteLiteral("\';\r\n    var DeleteResult=\'");
            EndContext();
            BeginContext(2198, 24, false);
#line 49 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\Admin\InvoiceApprovalOrderList.cshtml"
                  Write(TempData["DeleteResult"]);

#line default
#line hidden
            EndContext();
            BeginContext(2223, 226, true);
            WriteLiteral("\';\r\n    if (AddResult == 1) {\r\n        Common.SuccessNotify();\r\n    }\r\n    if (UpdateResult == 1) {\r\n        Common.UpdateNotify();\r\n    }\r\n    if (DeleteResult == 1) {\r\n        Common.DeleteNotify();\r\n    }\r\n</script>\r\n\r\n\r\n\r\n");
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
