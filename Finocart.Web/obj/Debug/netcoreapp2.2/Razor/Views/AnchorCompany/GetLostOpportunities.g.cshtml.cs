#pragma checksum "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\AnchorCompany\GetLostOpportunities.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c91342b0059c5b4887e6993557f8c27c05f8534e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_AnchorCompany_GetLostOpportunities), @"mvc.1.0.view", @"/Views/AnchorCompany/GetLostOpportunities.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/AnchorCompany/GetLostOpportunities.cshtml", typeof(AspNetCore.Views_AnchorCompany_GetLostOpportunities))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c91342b0059c5b4887e6993557f8c27c05f8534e", @"/Views/AnchorCompany/GetLostOpportunities.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6b23c2871a53ca46bac6eeea28bffed3162ffb9c", @"/Views/_ViewImports.cshtml")]
    public class Views_AnchorCompany_GetLostOpportunities : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Finocart.CustomModel.AnchorCompDropDownModel>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/AnchorCompany/LostOpportunities.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "0", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/Content/images/filtericon.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("title", new global::Microsoft.AspNetCore.Html.HtmlString("Filter"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("img-responsive"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/Content/images/mrge.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("data-toggle", new global::Microsoft.AspNetCore.Html.HtmlString("modal"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("data-target", new global::Microsoft.AspNetCore.Html.HtmlString("#mergeline"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("BtnMerge"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_9 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("data-toggle", new global::Microsoft.AspNetCore.Html.HtmlString("tooltip"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_10 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("title", new global::Microsoft.AspNetCore.Html.HtmlString("Merge Tool"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_11 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/Content/images/split.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_12 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("data-target", new global::Microsoft.AspNetCore.Html.HtmlString("#sliptrows"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_13 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("BtnSplitNew"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_14 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("title", new global::Microsoft.AspNetCore.Html.HtmlString("Split Tool"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(66, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\AnchorCompany\GetLostOpportunities.cshtml"
  
    ViewData["Title"] = "User";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(155, 275, true);
            WriteLiteral(@"<link rel=""stylesheet"" href=""https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/css/bootstrap-datepicker3.css"" />
<script type=""text/javascript"" src=""https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/js/bootstrap-datepicker.min.js""></script>
");
            EndContext();
            BeginContext(430, 60, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c91342b0059c5b4887e6993557f8c27c05f8534e9479", async() => {
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
            BeginContext(490, 50, true);
            WriteLiteral("\r\n<script>\r\n    var GetAnchorLostOpportunities = \'");
            EndContext();
            BeginContext(541, 59, false);
#line 11 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\AnchorCompany\GetLostOpportunities.cshtml"
                                 Write(Url.Action("GetAnchorLostOpportunities", "AnchorAnalytics"));

#line default
#line hidden
            EndContext();
            BeginContext(600, 759, true);
            WriteLiteral(@"';   
</script>
<style>
    .hide-column {
        display: none;
    }

    .main-table-box .table-bordered > tbody > tr > td, .main-table-box .table-bordered > tbody > tr > th, .main-table-box .table-bordered > tfoot > tr > td, .main-table-box .table-bordered > tfoot > tr > th, .main-table-box .table-bordered > thead > tr > td, .main-table-box .table-bordered > thead > tr > th {
        border: 1px solid #d8d8d8;
        border-bottom: 1px solid #d8d8d8;
        border-right: 0;
    }
</style>
<div class=""main-inner-section"">
    <div class=""content-upper-section"">
        <div class=""row"">
            
            <div class=""col-md-12 col-sm-12 col-xs-12"">
              
                <div class=""search-box-main clearfix"">
");
            EndContext();
            BeginContext(1499, 190, true);
            WriteLiteral("                    <h4 style=\"font-size:15px;font-weight:bold;\">Lost Opportunities</h4>\r\n                    <div class=\"col-md-12 col-sm-12 col-xs-12 padding\" style=\"display: inline;\">\r\n\r\n");
            EndContext();
            BeginContext(1809, 378, true);
            WriteLiteral(@"
                        <div class=""col-xs-12 col-sm-2 ana-lps"" style=""float: none;display: inline-table;padding:0;vertical-align: top;"">
                            <div class=""input-group"">
                                <select id=""ddl_Vendors"" class=""selectstatus"" type=""button"" data-toggle=""dropdown"" style=""padding: 6px 10px;"">

                                    ");
            EndContext();
            BeginContext(2187, 46, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c91342b0059c5b4887e6993557f8c27c05f8534e12709", async() => {
                BeginContext(2203, 21, true);
                WriteLiteral("Select Anchor Company");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2233, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 42 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\AnchorCompany\GetLostOpportunities.cshtml"
                                     foreach (var item in Model)
                                    {

#line default
#line hidden
            BeginContext(2340, 40, true);
            WriteLiteral("                                        ");
            EndContext();
            BeginContext(2380, 58, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c91342b0059c5b4887e6993557f8c27c05f8534e14504", async() => {
                BeginContext(2413, 16, false);
#line 44 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\AnchorCompany\GetLostOpportunities.cshtml"
                                                                   Write(item.CompanyName);

#line default
#line hidden
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            BeginWriteTagHelperAttribute();
#line 44 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\AnchorCompany\GetLostOpportunities.cshtml"
                                           WriteLiteral(item.CompanyID);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.SingleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2438, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 45 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\AnchorCompany\GetLostOpportunities.cshtml"
                                    }

#line default
#line hidden
            BeginContext(2479, 773, true);
            WriteLiteral(@"                                </select>
                            </div>
                            <label id=""lblAchrCmpny"" style=""display:none;"">Kindly Select Company</label>
                        </div>

                        <div class=""col-xs-12 col-sm-3 ana-lp"" style=""float: none;display: inline-table;"">
                            <span class=""input-group-addon datepic"">
                                <label class=""ana-lab"">From:</label>
                            </span>
                            <div class='input-group date' id=""lstOprtFrmdat"">
                                <input placeholder=""Select from date"" style=""border-right: 1px solid #dadbdc;min-width: 100%;margin-bottom:0;"" type=""text"" class=""searchTerm"" id=""FromDate"" />");
            EndContext();
            BeginContext(3383, 970, true);
            WriteLiteral(@"
                                <span class=""input-group-addon"" style=""border: 1px solid #dadbdc;padding:5px 10px;border-left: 0;"">
                                    <i class=""fas fa-calendar-alt""></i>
                                </span>
                            </div>
                            <label id=""lblErrMsg"" style=""display:none;"">Kindly Select from Date</label>
                        </div>
                        <div class=""col-xs-12 col-sm-3 ana-lp"" style=""float: none;display: inline-table;"">
                            <span class=""input-group-addon datepic"">
                                <label class=""ana-lab"">To:</label>
                            </span>
                            <div class='input-group date' id=""lstOprtTodat"">
                                <input placeholder=""Select to date"" style=""border-right: 1px solid #dadbdc;min-width: 100%;;margin-bottom:0;"" type=""text"" class=""searchTerm"" id=""ToDate"" />");
            EndContext();
            BeginContext(4484, 430, true);
            WriteLiteral(@"
                                <span class=""input-group-addon"" style=""border: 1px solid #dadbdc;padding:5px 10px;border-left: 0;"">
                                    <i class=""fas fa-calendar-alt""></i>
                                </span>
                            </div>
                            <label id=""lblErrMsgToDate"" style=""display:none;"">Kindly Select To Date</label>
                        </div>


");
            EndContext();
            BeginContext(5172, 122, true);
            WriteLiteral("\r\n                        <button id=\"btnInvoiceFilter\" class=\"filterbtn\" style=\"margin:0;\">\r\n                            ");
            EndContext();
            BeginContext(5294, 83, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "c91342b0059c5b4887e6993557f8c27c05f8534e19537", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(5377, 289, true);
            WriteLiteral(@"
                        </button>
                    </div>
                </div>
            </div>

            <div class=""col-md-12 col-sm-12 col-xs-12"">
                <div class=""table-icons"" style=""display:none;"">
                    <ul>
                        <li><a");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 5666, "\"", 5711, 1);
#line 91 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\AnchorCompany\GetLostOpportunities.cshtml"
WriteAttributeValue("", 5673, Url.Action("ExportUsersData", "User"), 5673, 38, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(5712, 147, true);
            WriteLiteral("><i class=\"fa fa-download\" aria-hidden=\"true\" data-toggle=\"tooltip\" title=\"Export To Excel\"></i></a></li>\r\n                        <li><a href=\"#\">");
            EndContext();
            BeginContext(5859, 162, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "c91342b0059c5b4887e6993557f8c27c05f8534e21783", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_8);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_9);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_10);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(6021, 51, true);
            WriteLiteral("</a></li>\r\n                        <li><a href=\"#\">");
            EndContext();
            BeginContext(6072, 166, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "c91342b0059c5b4887e6993557f8c27c05f8534e23506", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_11);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_12);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_13);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_9);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_14);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(6238, 2113, true);
            WriteLiteral(@"</a></li>
                    </ul>
                </div>
                <div class=""main-table-box page-scroll"" id=""dvLostOpportunities"" style=""display:none"">
                    <div class=""table-responsive"">
                        <table id=""tbl_LostOpportunitiesList"" class=""table table-responsive table-hover table-bordered tbl_UserList"">
                            <thead>
                                <tr class=""first-tr"">
                                    <th colspan=""2"">
                                        Total Number of Discounted
                                        Invoices Received from Vendor
                                    </th>
                                    <th colspan=""2"">
                                        Number of Invoices Approved for
                                        Discount
                                    </th>
                                    <th colspan=""1"">% of Discounted Invoice Approved</th>
                                ");
            WriteLiteral(@"    <th colspan=""4"">Opportunities</th>
                                </tr>
                                <tr class=""sec-tr"">

                                    <th style=""display:none""></th>
                                    <th>No. of Invoices</th>
                                    <th>Approved Amt</th>
                                    <th>Number of Inv</th>
                                    <th>Amount Approved</th>
                                    <th></th>
                                    <th>Amt Paid</th>
                                    <th>Amt Earned</th>
                                    <th>Opportunities Lost on Amount </th>
                                    <th>% Of Opportunities</th>
                                    <th style=""display:none""></th>
                                </tr>
                            </thead>
                            <tbody></tbody>
                        </table>
                    </div>
                </div>
   ");
            WriteLiteral("         </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n\r\n\r\n\r\n\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Finocart.CustomModel.AnchorCompDropDownModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
