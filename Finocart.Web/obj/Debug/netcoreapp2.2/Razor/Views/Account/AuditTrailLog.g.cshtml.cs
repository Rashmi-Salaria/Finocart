#pragma checksum "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\Account\AuditTrailLog.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e7378a05bd239d34fc2a69b5c50db82c9d8696ad"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Account_AuditTrailLog), @"mvc.1.0.view", @"/Views/Account/AuditTrailLog.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Account/AuditTrailLog.cshtml", typeof(AspNetCore.Views_Account_AuditTrailLog))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e7378a05bd239d34fc2a69b5c50db82c9d8696ad", @"/Views/Account/AuditTrailLog.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6b23c2871a53ca46bac6eeea28bffed3162ffb9c", @"/Views/_ViewImports.cshtml")]
    public class Views_Account_AuditTrailLog : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/User/AuditTrailLog.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/Content/images/filtericon.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("title", new global::Microsoft.AspNetCore.Html.HtmlString("Filter"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("img-responsive"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 1 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\Account\AuditTrailLog.cshtml"
  
    ViewData["Title"] = "User";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(87, 345, true);
            WriteLiteral(@"<style>
    .hide_column {
        display: none;
    }
</style>
<link rel=""stylesheet"" href=""https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/css/bootstrap-datepicker3.css"" />
<script type=""text/javascript"" src=""https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/js/bootstrap-datepicker.min.js""></script>
");
            EndContext();
            BeginContext(432, 47, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e7378a05bd239d34fc2a69b5c50db82c9d8696ad5315", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(479, 44, true);
            WriteLiteral("\r\n<script>\r\n    var GetAuditTrailLogData = \'");
            EndContext();
            BeginContext(524, 48, false);
#line 14 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\Account\AuditTrailLog.cshtml"
                           Write(Url.Action("GetAuditTrailList", "AnchorCompany"));

#line default
#line hidden
            EndContext();
            BeginContext(572, 1053, true);
            WriteLiteral(@"';
</script>
<div class=""main-inner-section"">
    <div class=""content-upper-section"">
        <div class=""tabsec-main"">
            <div class=""tab-content"">
                <div id=""eInvoices"" class=""tab-pane fade in active"">
                    <div class=""row"" id=""eAnchorCompInvoices"">
                        <div class=""col-md-12 col-sm-12 col-xs-12"">
                            <div class=""search-box-main clearfix"">
                                <div class=""col-xs-12 col-sm-4 col-md-3 ana-fr"" style=""float: none;display: inline-table;"">
                                    <span class=""input-group-addon datepic"">
                                        <label class=""ana-lab"">From:</label>
                                    </span>
                                    <div class='input-group date' id=""FromClender"">
                                        <input placeholder=""Select from date"" style=""border-right: 1px solid #dadbdc;min-width: 100%;margin: 0;"" type=""text"" class=""searchTerm"" id");
            WriteLiteral("=\"FundReqFromDate\" readonly> ");
            EndContext();
            BeginContext(1784, 1092, true);
            WriteLiteral(@"
                                        <span class=""input-group-addon"" style=""border: 1px solid #dadbdc;padding: 5px 10px;border-left: 0;"">
                                            <i class=""fas fa-calendar-alt""></i>
                                        </span>
                                    </div>
                                    <label id=""lblErrMsg"" style=""display:none;padding:0;"">Kindly Select From Date</label>
                                </div>
                                <div class=""col-xs-12 col-sm-4 col-md-3 ana-fr"" style=""float: none;display:inline-table;"">
                                    <span class=""input-group-addon datepic"">
                                        <label class=""ana-lab"">To:</label>
                                    </span>
                                    <div class=""input-group date"" id=""Toclender"">

                                        <input placeholder=""Select to date"" style=""border-right: 1px solid #dadbdc;min-width: 100%;marg");
            WriteLiteral("in:0;\" type=\"text\" class=\"searchTerm\" id=\"FundReqToDate\" readonly />");
            EndContext();
            BeginContext(3035, 609, true);
            WriteLiteral(@"
                                        <span class=""input-group-addon"" id=""ToClnderDate"" style=""border: 1px solid #dadbdc;padding:5px 10px;border-left: 0;"">
                                            <i class=""fas fa-calendar-alt""></i>
                                        </span>
                                    </div>
                                    <label id=""lblErrMsgToDate"" style=""display:none;padding:0;"">Kindly Select To Date</label>
                                </div>

                                <div class=""col-xs-12 col-sm-2 col-md-4 padding"" style=""float: right;"">
");
            EndContext();
            BeginContext(3978, 157, true);
            WriteLiteral("                                    <button id=\"btnComapnyFilter\" class=\"filterbtn\" style=\"float: right;margin:0;\">\r\n                                        ");
            EndContext();
            BeginContext(4135, 83, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "e7378a05bd239d34fc2a69b5c50db82c9d8696ad10318", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(4218, 1355, true);
            WriteLiteral(@"
                                    </button>
                                </div>
                            </div>
                        </div>

                        <div class=""col-md-12 col-sm-12 col-xs-12"">
                            <div class=""main-table-box page-scroll"">
                                <div class=""table-responsive"">
                                    <table id=""tbl_AuditTrailList"" class=""table table-responsive table-hover table-bordered"">
                                        <thead>
                                            <tr>
                                                <th style=""width:10%"">Module Name</th>
                                                <th style=""width:10%"">Page Name</th>
                                                <th style=""width:13%"">Log Date</th>
                                                <th style=""width:12%"">User Name</th>
                                            </tr>
                                        ");
            WriteLiteral(@"</thead>
                                        <tbody></tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

");
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
