#pragma checksum "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\Vendor\AwaitedInvoicesApprovals.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8647b2f650d16f9516c80c4d479ad9a474a15aa6"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Vendor_AwaitedInvoicesApprovals), @"mvc.1.0.view", @"/Views/Vendor/AwaitedInvoicesApprovals.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Vendor/AwaitedInvoicesApprovals.cshtml", typeof(AspNetCore.Views_Vendor_AwaitedInvoicesApprovals))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8647b2f650d16f9516c80c4d479ad9a474a15aa6", @"/Views/Vendor/AwaitedInvoicesApprovals.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6b23c2871a53ca46bac6eeea28bffed3162ffb9c", @"/Views/_ViewImports.cshtml")]
    public class Views_Vendor_AwaitedInvoicesApprovals : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Tuple<IEnumerable<Finocart.Model.ModuleMaster>, IEnumerable<Finocart.Model.SearchHistory>>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/AwaitedAnchCompList/AwaitedAnchrCompList.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/Content/images/searchicon.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("img-responsive"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("display: inline;"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/Content/images/excelicon.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("title", new global::Microsoft.AspNetCore.Html.HtmlString("Download Excel File"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 2 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\Vendor\AwaitedInvoicesApprovals.cshtml"
  
    ViewData["Title"] = "AwaitedInvoicesApprovals";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(206, 51, true);
            WriteLiteral("\r\n<script>\r\n    var GetAnchorCompListByVendorID = \'");
            EndContext();
            BeginContext(258, 54, false);
#line 8 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\Vendor\AwaitedInvoicesApprovals.cshtml"
                                  Write(Url.Action("getAwaitedInvoiceList", "VendorDashboard"));

#line default
#line hidden
            EndContext();
            BeginContext(312, 20, true);
            WriteLiteral("\';\r\n   \r\n</script>\r\n");
            EndContext();
            BeginContext(332, 69, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8647b2f650d16f9516c80c4d479ad9a474a15aa66406", async() => {
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
            BeginContext(401, 439, true);
            WriteLiteral(@"
<div class=""main-inner-section"">
    <div class=""content-upper-section"">
        <div class=""row"">
            <div class=""col-md-12 col-sm-12 col-xs-12"">
                <div class=""search-box-main clearfix"">
                    <h4 style=""font-size:15px;font-weight:bold;"">Awaited Approval</h4>
                    <div class=""row"">
                        <div class=""col-xs-12 col-sm-3 col-md-3"">
                            ");
            EndContext();
            BeginContext(841, 718, false);
#line 20 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\Vendor\AwaitedInvoicesApprovals.cshtml"
                       Write(Html.DropDownList("pagechange",new List<SelectListItem>
                                {
                                new SelectListItem{ Text="Awaited Approval in INR",Value="1"},
                                 new SelectListItem{ Text="Receivables Due Today in INR",Value="2"},
                                 new SelectListItem{ Text="Amount Approved Today in INR",Value="3"},
                                 new SelectListItem{ Text="Available for Funding Today in INR",Value="4"},
                                 new SelectListItem{ Text="Invoices Status",Value="5"}
                            }, new { @class = "searchTerm ext-box", @style = "border-right: 1px solid #dadbdc;max-width: 100%;" }));

#line default
#line hidden
            EndContext();
            BeginContext(1559, 269, true);
            WriteLiteral(@"
                        </div>

                        <div class=""col-xs-12 col-sm-3 col-md-3"">
                            <input type=""text"" class=""searchTerm"" id=""txt_AnchorCompanyName"" placeholder=""Search by Anchor Company Name"" /><span class=""searchButton"">");
            EndContext();
            BeginContext(1828, 93, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "8647b2f650d16f9516c80c4d479ad9a474a15aa69397", async() => {
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
            BeginContext(1921, 272, true);
            WriteLiteral(@"</span>
                        </div>
                        <div class=""col-xs-12 col-sm-3 col-md-3"">
                            <input type=""text"" class=""searchTerm"" id=""txt_AppInvoiceAmnt"" placeholder=""Search by Total Invoice Amount"" /><span class=""searchButton"">");
            EndContext();
            BeginContext(2193, 93, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "8647b2f650d16f9516c80c4d479ad9a474a15aa611016", async() => {
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
            BeginContext(2286, 252, true);
            WriteLiteral("</span>\r\n                        </div>\r\n                        <div class=\"col-xs-12 col-sm-3 col-md-3\">\r\n                            <button id=\"ExportAwtedAppInvoiceCSV\" class=\"excelbutton\" style=\"margin-right:0;\">\r\n                                ");
            EndContext();
            BeginContext(2538, 120, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "8647b2f650d16f9516c80c4d479ad9a474a15aa612620", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
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
            BeginContext(2658, 75, true);
            WriteLiteral("\r\n                            </button>\r\n\r\n                        </div>\r\n");
            EndContext();
            BeginContext(2936, 2368, true);
            WriteLiteral(@"
                    </div>
                </div>
                </div>

                        <div class=""col-md-12 col-sm-12 col-xs-12"">
                            <div class=""main-table-box page-scroll"">
                                <div class=""table-responsive"">
                                    <table id=""tbl_AwaitedInvoice"" class=""table table-responsive table-bordered table-hover tbl_UserList"">
                                        <thead>
                                            <tr>
                                                <th hidden>Anchor Comp ID</th>
                                                <th>Anchor Comp Name</th>
                                                <th>No. of Disc. Offered Inv</th>
                                                <th>Total Inv. Amt(INR)</th>
                                                <th>Total Approved Amt(INR)</th>
                                                <th>Action</th>
                                       ");
            WriteLiteral(@"     </tr>
                                        </thead>
                                    </table>
                                </div>
                            </div>
                            <div class=""col-xs-12 footer-btn"">
                                <div class=""row"">
                                   
                                    <div class=""col-xs-12 col-sm-12"">
                                        <div class=""row"">
                                            <div class=""col-xs-12 col-sm-4 padding"">
                                                <label class=""footer-label"">Total Invoices: <span id=""lbAwaitTotalInvoices""></span></label>
                                            </div>
                                            <div class=""col-xs-12 col-sm-4 padding"">
                                                <label class=""footer-label"">Total Invoice Amount (INR): <span id=""lbAwaitTotalInvoicesAmt""></span></label>
                                     ");
            WriteLiteral(@"       </div>
                                           
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Tuple<IEnumerable<Finocart.Model.ModuleMaster>, IEnumerable<Finocart.Model.SearchHistory>>> Html { get; private set; }
    }
}
#pragma warning restore 1591
