#pragma checksum "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\BankCompany\Dashboard.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "75bdc33e7c2463925beffaf1e3c972f987b29fb4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_BankCompany_Dashboard), @"mvc.1.0.view", @"/Views/BankCompany/Dashboard.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/BankCompany/Dashboard.cshtml", typeof(AspNetCore.Views_BankCompany_Dashboard))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"75bdc33e7c2463925beffaf1e3c972f987b29fb4", @"/Views/BankCompany/Dashboard.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6b23c2871a53ca46bac6eeea28bffed3162ffb9c", @"/Views/_ViewImports.cshtml")]
    public class Views_BankCompany_Dashboard : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/Scripts/fusioncharts.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/Scripts/fusioncharts.theme.fusion.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/BankCompany/BankSetLimitgraph.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/BankCompany/BankAnchorList"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("btnSetLimitList"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("dowte"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/BankCompany/BankDashboard.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\BankCompany\Dashboard.cshtml"
  
    ViewData["Title"] = "Dashboard";

#line default
#line hidden
            BeginContext(45, 49, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "75bdc33e7c2463925beffaf1e3c972f987b29fb46213", async() => {
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
            BeginContext(94, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(96, 62, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "75bdc33e7c2463925beffaf1e3c972f987b29fb47388", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(158, 1538, true);
            WriteLiteral(@"
<script type=""text/javascript""
        src=""https://rawgit.com/fusioncharts/fusioncharts-jquery-plugin/develop/dist/fusioncharts.jqueryplugin.min.js""></script>

<style>


    .charts-section .charts-title {
        position: relative;
        overflow: hidden;
        background-color: #454545;
        padding: 13px 10px 10px 10px;
        border: 1px solid #ececec;
        border-bottom: 1px solid #a2a2a2;
        width: 100%;
    }

    .charts-title .form-control-graph {
        /*width: 130px;*/
        padding: 4px;
        font-size: 14px;
        color: #ffffff;
        margin-left: 5px;
        background-color: #a2a2a2;
        outline: none;
        border: 1px solid #fff;
        height: 30px;
        line-height: 36px;
        -webkit-border-radius: 0;
        -moz-border-radius: 0;
        border-radius: 0;
    }

    .charts-section .charts-title h5 {
        display: table;
        float: left;
        margin: 0;
        line-height: 2;
        color: #fff");
            WriteLiteral(@"fff;
        text-transform: capitalize;
        font-weight: 600;
    }

    .charts-section .chart-div {
        height: 315px;
        text-align: center;
        border: 1px solid #ececec;
        font-weight: bold;
        background-color: #454545;
        color: #ffffff;
        padding-top: 15px;
        border-top: 0;
        width: 100%;
        margin-bottom: 20px;
        overflow: hidden;
    }

    .cls-error {
        display: block;
        color: red;
    }
</style>

");
            EndContext();
            BeginContext(1696, 58, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "75bdc33e7c2463925beffaf1e3c972f987b29fb410140", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1754, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(1869, 103, true);
            WriteLiteral("\r\n<div class=\"row\">\r\n    <h4 style=\"margin-left:13px;font-size:15px;font-weight:bold;\">Dashboard</h4>\r\n");
            EndContext();
            BeginContext(2010, 131, true);
            WriteLiteral("        <div class=\"top-boxes clearfix anchor\">\r\n            <div class=\"col-xs-12 col-sm-6 col-md-2\">\r\n                <a href=\"#\"");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 2141, "\"", 2210, 3);
            WriteAttributeValue("", 2151, "location.href=\'", 2151, 15, true);
#line 78 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\BankCompany\Dashboard.cshtml"
WriteAttributeValue("", 2166, Url.Action("BankAnchorList","BankCompany"), 2166, 43, false);

#line default
#line hidden
            WriteAttributeValue("", 2209, "\'", 2209, 1, true);
            EndWriteAttribute();
            BeginContext(2211, 119, true);
            WriteLiteral(">\r\n                <div class=\"box1\">\r\n                    <h4 style=\"font-size:20px !important;margin-bottom: 5px;\">\r\n");
            EndContext();
            BeginContext(2423, 95, true);
            WriteLiteral("                        <p>New Anchors</p>\r\n                    </h4>\r\n                    <h4>");
            EndContext();
            BeginContext(2519, 26, false);
#line 84 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\BankCompany\Dashboard.cshtml"
                   Write(ViewData["TOTALNEWANCHOR"]);

#line default
#line hidden
            EndContext();
            BeginContext(2545, 157, true);
            WriteLiteral("</h4>\r\n                </div>\r\n                </a>\r\n\r\n            </div>\r\n            <div class=\"col-xs-12 col-sm-6 col-md-2\">\r\n                <a href=\"#\"");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 2702, "\"", 2787, 3);
            WriteAttributeValue("", 2712, "location.href=\'", 2712, 15, true);
#line 90 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\BankCompany\Dashboard.cshtml"
WriteAttributeValue("", 2727, Url.Action("AwaitedInvoiceApproval","AnchorCompDashboard"), 2727, 59, false);

#line default
#line hidden
            WriteAttributeValue("", 2786, "\'", 2786, 1, true);
            EndWriteAttribute();
            BeginContext(2788, 127, true);
            WriteLiteral(">\r\n                    <div class=\"box2\">\r\n                        <h4 style=\"font-size:20px !important;margin-bottom: 5px;\">\r\n");
            EndContext();
            BeginContext(3012, 114, true);
            WriteLiteral("                            <p>Existing Anchors</p>\r\n\r\n                        </h4>\r\n                        <h4>");
            EndContext();
            BeginContext(3127, 31, false);
#line 97 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\BankCompany\Dashboard.cshtml"
                       Write(ViewData["TOTALEXISTINGANCHOR"]);

#line default
#line hidden
            EndContext();
            BeginContext(3158, 161, true);
            WriteLiteral("</h4>\r\n                    </div>\r\n                </a>\r\n            </div>\r\n            <div class=\"col-xs-12 col-sm-6 col-md-2\">\r\n\r\n                <a href=\"#\"");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 3319, "\"", 3396, 3);
            WriteAttributeValue("", 3329, "location.href=\'", 3329, 15, true);
#line 103 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\BankCompany\Dashboard.cshtml"
WriteAttributeValue("", 3344, Url.Action("BankLimitAnchorListing","BankCompany"), 3344, 51, false);

#line default
#line hidden
            WriteAttributeValue("", 3395, "\'", 3395, 1, true);
            EndWriteAttribute();
            BeginContext(3397, 127, true);
            WriteLiteral(">\r\n                    <div class=\"box3\">\r\n                        <h4 style=\"font-size:20px !important;margin-bottom: 5px;\">\r\n");
            EndContext();
            BeginContext(3621, 111, true);
            WriteLiteral("                            <p>Total Set limit</p>\r\n                        </h4>\r\n                        <h4>");
            EndContext();
            BeginContext(3733, 36, false);
#line 109 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\BankCompany\Dashboard.cshtml"
                       Write(ViewData["TOTALBANKLIMITANCHORLIST"]);

#line default
#line hidden
            EndContext();
            BeginContext(3769, 161, true);
            WriteLiteral("</h4>\r\n                    </div>\r\n                </a>\r\n            </div>\r\n            <div class=\"col-xs-12 col-sm-6 col-md-2\">\r\n\r\n                <a href=\"#\"");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 3930, "\"", 4005, 3);
            WriteAttributeValue("", 3940, "location.href=\'", 3940, 15, true);
#line 115 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\BankCompany\Dashboard.cshtml"
WriteAttributeValue("", 3955, Url.Action("DisbursementsListing","BankCompany"), 3955, 49, false);

#line default
#line hidden
            WriteAttributeValue("", 4004, "\'", 4004, 1, true);
            EndWriteAttribute();
            BeginContext(4006, 127, true);
            WriteLiteral(">\r\n                    <div class=\"box4\">\r\n                        <h4 style=\"font-size:20px !important;margin-bottom: 5px;\">\r\n");
            EndContext();
            BeginContext(4230, 120, true);
            WriteLiteral("                            <p>Total Disbersement limit</p>\r\n                        </h4>\r\n                        <h4>");
            EndContext();
            BeginContext(4351, 34, false);
#line 121 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\BankCompany\Dashboard.cshtml"
                       Write(ViewData["TOTALUTILIZEDBANKLIMIT"]);

#line default
#line hidden
            EndContext();
            BeginContext(4385, 161, true);
            WriteLiteral("</h4>\r\n                    </div>\r\n                </a>\r\n            </div>\r\n            <div class=\"col-xs-12 col-sm-6 col-md-2\">\r\n\r\n                <a href=\"#\"");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 4546, "\"", 4618, 3);
            WriteAttributeValue("", 4556, "location.href=\'", 4556, 15, true);
#line 127 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\BankCompany\Dashboard.cshtml"
WriteAttributeValue("", 4571, Url.Action("KycUploadListdata","BankCompany"), 4571, 46, false);

#line default
#line hidden
            WriteAttributeValue("", 4617, "\'", 4617, 1, true);
            EndWriteAttribute();
            BeginContext(4619, 127, true);
            WriteLiteral(">\r\n                    <div class=\"box3\">\r\n                        <h4 style=\"font-size:20px !important;margin-bottom: 5px;\">\r\n");
            EndContext();
            BeginContext(4843, 113, true);
            WriteLiteral("                            <p>Total Pending KYC</p>\r\n                        </h4>\r\n                        <h4>");
            EndContext();
            BeginContext(4957, 27, false);
#line 133 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\BankCompany\Dashboard.cshtml"
                       Write(ViewData["TOTALPENDINGKYC"]);

#line default
#line hidden
            EndContext();
            BeginContext(4984, 93, true);
            WriteLiteral("</h4>\r\n                    </div>\r\n                </a>\r\n            </div>\r\n        </div>\r\n");
            EndContext();
            BeginContext(5144, 132, true);
            WriteLiteral("        <label style=\"margin: 0 0 7px 0; font-weight: 600;color: #222;\">\r\n            Disbursement Request Received Today INR <span>");
            EndContext();
            BeginContext(5277, 33, false);
#line 141 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\BankCompany\Dashboard.cshtml"
                                                     Write(ViewBag.requestAmountReceiveToday);

#line default
#line hidden
            EndContext();
            BeginContext(5310, 62, true);
            WriteLiteral("</span>\r\n        </label>\r\n        <span style=\"float:right;\">");
            EndContext();
            BeginContext(5372, 87, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "75bdc33e7c2463925beffaf1e3c972f987b29fb419987", async() => {
                BeginContext(5446, 9, true);
                WriteLiteral("Set Limit");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(5459, 908, true);
            WriteLiteral(@"</span>
        <div class=""col-xs-12 col-sm-6 col-md-6 charts-section"">
            <div class=""charts-title"">
                <!-- Select Div Start -->
                <div class=""row"">
                    <div class=""col-md-3"">
                        <h5>Clash Flow</h5>
                    </div>
                    <div class=""col-md-9 zindex5"">
                        <div class=""form-inline rightAlign"">
                            <div class=""form-group"" style=""float:right;"">

                                <select id=""SetLimitforAnchor"" class=""form-control-graph""></select>
                            </div>
                        </div>
                    </div>
                    <div class=""clearfix""></div>
                </div>
                <!-- Select Div End -->
            </div>
            <div id=""chart-container""></div>
        </div>
           
");
            EndContext();
            BeginContext(6387, 1147, true);
            WriteLiteral(@"    <div class=""col-xs-12 col-sm-6 col-md-6"" style=""padding-right:0;"">
        <h4 style=""margin-top: 0;font-size: 12px;"">Top Customers (Anchor Companies) <span style=""float:right;"">3 Months</span></h4>
        <div class=""bottom-listing"">
            <div class=""main-table-box page-scroll ten-tab-scroll"">
                <div class=""table-responsive"">
                    <table id=""tbl_TopAnchorCompany"" class=""table table-responsive table-bordered table-hover internalusermtable"">
                        <thead>
                            <tr>
                                <th>Anchor Name</th>
                                <th>Anchor ID</th>
                                <th>Limit Available (INR)</th>
                                <th>Limit Utilize (INR)</th>
                            </tr>

                        </thead>
                    </table>
                </div>

            </div>
        </div>
    </div>
</div>

<!--Rule of Engine Model Popup-->
<div class=""m");
            WriteLiteral("odal fade\" id=\"myModal\" role=\"dialog\">\r\n    <div class=\"modal-dialog addengine\">\r\n\r\n        <!-- Modal content-->\r\n        ");
            EndContext();
            BeginContext(7534, 1978, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "75bdc33e7c2463925beffaf1e3c972f987b29fb423698", async() => {
                BeginContext(7540, 1965, true);
                WriteLiteral(@"
            <div class=""modal-content"">
                <div class=""modal-header"">
                    <button type=""button"" class=""close new-close"" data-dismiss=""modal"">&times;</button>
                    <h4 class=""modal-title"">Rule of Engine</h4>
                </div>
                <div class=""modal-body engine-box"">
                    <div class=""form-group"">
                        <input type=""text"" id=""rule_percentage"" name=""model_popup"" placeholder=""Enter Percentage"" onkeypress=""return isNumberKey(event)"" class=""form-control"" />
                        <label id=""lblrule_percentageError"" class=""error"" style=""color:red;display: none;"">Please Enter Value</label>
                    </div>
                    <div class=""form-group"">
                        <input type=""text"" id=""rule_limit"" name=""model_popup"" placeholder=""Enter Limit/Day"" onkeypress=""return isNumberKey(event)"" class=""form-control"" />
                        <label id=""lblrule_limitError"" class=""error"" style=""color:red");
                WriteLiteral(@";display: none;"">Please Enter Value</label>
                        <label id=""lblrule_limitError_radio"" class=""error"" style=""color:red;display: none;"">Please Select Value</label>
                    </div>
                    <div class=""form-group"">
                        <label><input style=""vertical-align: bottom;margin-right: 5px;"" type=""radio"" name=""status"" value=""yes"" id=""status_yes"">Yes</label>
                        <label><input style=""vertical-align: bottom;margin-right: 5px;"" type=""radio"" name=""status"" value=""no"" id=""status_no"">No</label>
                    </div>
                </div>
                <div class=""modal-footer"">
                    <button type=""button"" class=""btn btn-close"" data-dismiss=""modal"" style=""margin:0;"">Close</button>
                    <button type=""button"" class=""btn confirmbutton"" id=""submit_rule_engine"">Submit</button>
                </div>
            </div>
        ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(9512, 22, true);
            WriteLiteral("\r\n    </div>\r\n</div>\r\n");
            EndContext();
            BeginContext(9534, 54, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "75bdc33e7c2463925beffaf1e3c972f987b29fb427271", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(9588, 981, true);
            WriteLiteral(@"

<script>
    $(document).ready(function () {

        $('#tbl_TopAnchorCompany').dataTable({

            ""processing"": true,
            ""serverSide"": true,

            ""dom"": '""ltipr""',
            ""scrollX"": false,

            ""ajax"": {
                ""url"": ""../BankCompany/GetTopAnchorData"",
                ""type"": ""POST"",
                ""datatype"": ""json"",
            },
            ""columns"": [
                { ""data"": ""anchor_Name"", ""name"": ""anchor_Name"", ""autoWidth"": true }, //index 1
                { ""data"": ""anchor_id"", ""name"": ""anchor_id"", ""autoWidth"": true }, //index 2
                { ""data"": ""available_Limits"", ""name"": ""available_Limits"", ""autoWidth"": true, render: $.fn.dataTable.render.number(',', '.', 0) }, //index 3
                { ""data"": ""utilized_Limits"", ""name"": ""utilized_Limits"", ""autoWidth"": true, render: $.fn.dataTable.render.number(',', '.', 0) } //index 4


            ],
        });
    });
</script>
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
