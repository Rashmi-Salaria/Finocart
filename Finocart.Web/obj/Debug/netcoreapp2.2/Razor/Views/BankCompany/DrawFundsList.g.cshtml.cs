#pragma checksum "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\BankCompany\DrawFundsList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "08f160ef83ba2a13eb04e5acf0b1d66695f9e74b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_BankCompany_DrawFundsList), @"mvc.1.0.view", @"/Views/BankCompany/DrawFundsList.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/BankCompany/DrawFundsList.cshtml", typeof(AspNetCore.Views_BankCompany_DrawFundsList))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"08f160ef83ba2a13eb04e5acf0b1d66695f9e74b", @"/Views/BankCompany/DrawFundsList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6b23c2871a53ca46bac6eeea28bffed3162ffb9c", @"/Views/_ViewImports.cshtml")]
    public class Views_BankCompany_DrawFundsList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/Content/images/searchicon.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("img-responsive"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("display: inline;"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 1 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\BankCompany\DrawFundsList.cshtml"
  
    ViewData["Title"] = "DrawFundsList";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(96, 141, true);
            WriteLiteral("\r\n<style>\r\n    .modal-dialog {\r\n        width: 600px;\r\n        margin: 30px auto;\r\n    }\r\n</style>\r\n<script>\r\n    var GetBankLimitLogList = \'");
            EndContext();
            BeginContext(238, 48, false);
#line 13 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\BankCompany\DrawFundsList.cshtml"
                          Write(Url.Action("GetBankLimitLogList", "BankCompany"));

#line default
#line hidden
            EndContext();
            BeginContext(286, 725, true);
            WriteLiteral(@"';
</script>
<div class=""main-inner-section"">
    <div class=""content-upper-section"" id=""AnchorCompany"">
        <div class=""tabsec-main"">
            <div class=""tab-content"">
                <div class=""row"" id="""">
                    <div class=""col-md-12 col-sm-12 col-xs-12"">
                        <div class=""main-table-box page-scroll"" style=""padding:15px 15px 0 15px;"">
                            <h4 style=""font-size:15px;font-weight:bold;margin-top:0;"">Upload KYC</h4>
                        </div>
                        <div class=""search-box-main clearfix"" style=""padding:0 15px 0;"">
                            <div class=""col-xs-12 col-sm-4 col-md-4 padding"">
                                ");
            EndContext();
            BeginContext(1012, 115, false);
#line 26 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\BankCompany\DrawFundsList.cshtml"
                           Write(Html.TextBox("txtSearchfield", null, new { placeholder = "Search by Bank Name,KYC status", @class = "searchTerm" }));

#line default
#line hidden
            EndContext();
            BeginContext(1127, 27, true);
            WriteLiteral("<span class=\"searchButton\">");
            EndContext();
            BeginContext(1154, 93, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "08f160ef83ba2a13eb04e5acf0b1d66695f9e74b6467", async() => {
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
            BeginContext(1247, 1255, true);
            WriteLiteral(@"</span>
                            </div>
                        </div>
                        </div>
                    <div class=""col-md-12 col-sm-12 col-xs-12"">
                        <div class=""main-table-box page-scroll"">
                            <div class=""table-responsive"">
                                <table id=""DrawFundsList_tb"" class=""table table-responsive table-hover table-bordered tbl_UserList dataTable no-footer"">
                                    <thead>
                                        <tr>
                                            <th hidden>ID</th>
                                            <th>Bank ID</th>
                                            <th>Name of Bank</th>
                                            <th>KYC Status</th>
                                            <th style=""width:10%"">Documentation</th>
                                        </tr>
                                    </thead>
                                    <tbody");
            WriteLiteral("></tbody>\r\n                                </table>\r\n                            </div>\r\n                        </div>\r\n\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n");
            EndContext();
            BeginContext(2514, 457, true);
            WriteLiteral(@"<div id=""frview"" class=""modal fade frview"" role=""dialog"">
    <div class=""modal-dialog"">
        <!-- Modal content-->
        <div class=""modal-content"">
            <div class=""modal-body"">
                <div class=""frview-tab"">
                    <div id=""dvFundingReqAmount"">
                        <table class=""table table-responsive"" id=""tblBankSetLimitLogView"">
                            <thead>
                                <tr>
");
            EndContext();
            BeginContext(3284, 4254, true);
            WriteLiteral(@"                                    <th>Available Limits (INR)</th>
                                    <th>Utilized Limits (INR)</th>
                                    <th>ODBD</th>
                                    <th>Interest Rate (% P.A.)</th>
                                    <th>Validity From Date</th>
                                    <th>Validity Upto</th>
                                    <th>Modified Date</th>
                                </tr>
                            </thead>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

<script>
    $(document).ready(function () {

        $('#DrawFundsList_tb').dataTable({

            ""processing"": true,
            ""serverSide"": true,

            ""dom"": '""ltipr""',
            ""scrollX"": false,

            ""ajax"": {
                ""url"": ""../BankCompany/FundsList"",
                ""type"": ""POST"",
                ""datatype""");
            WriteLiteral(@": ""json"",
            },
            ""columns"": [
                { ""data"": ""id"", ""name"": ""id"", ""autoWidth"": true, ""class"": ""hidden"", ""visible"": false, ""searchable"": false },
                { ""data"": ""bankID"", ""name"": ""bankID"", ""autoWidth"": true, ""visible"": false, ""searchable"": false},
                { ""data"": ""bankName"", ""name"": ""bankName"", ""autoWidth"": true }, 
                //{ ""data"": ""odbd"", ""name"": ""odbd"", ""autoWidth"": true }, 
                //{ ""data"": ""available_Limits"", ""name"": ""available_Limits"", ""autoWidth"": true, render: $.fn.dataTable.render.number(',', '.', 0)  }, 
                ////{ ""data"": ""utilized_Limits"", ""name"": ""utilized_Limits"", ""autoWidth"": true, render: $.fn.dataTable.render.number(',', '.', 0)  }, 
                //{ ""data"": ""interest_rate"", ""name"": ""Interest_rate"", ""autoWidth"": true }, 
                //{
                //    ""data"": ""validityFromto"", ""name"": ""ValidityFromto"", ""autoWidth"": true, ""format"": ""dd/MM/YYYY""
                //},
                
  ");
            WriteLiteral(@"              ////{ ""data"": ""interest_rate_month"", ""name"": ""Interest_rate_month"", ""autoWidth"": true }, 
                //{
                //    ""data"": ""validity_upto"", ""name"": ""validity_upto"", ""autoWidth"": true, ""format"": ""dd/MM/YYYY"" },
                //{ ""data"": ""draw_funds"", ""name"": ""Draw_funds"", ""autoWidth"": true, render: $.fn.dataTable.render.number(',', 0) }, 
                { ""data"": ""status"", ""name"": ""status"", ""autoWidth"": true }, 
                {
                    ""data"": function (data, type, row) {
                        return ""<a class='btnDocument' data-status="" + data.status + "" data-id="" + data.bankID + "" data-bank="" + data.bankName +"" ><img src='../Content/images/shape-4.png' title='View' class='img-responsive' /></a>"";
                            //+""<a href='#' onclick='GetLogView("" + data.id + "")' class='actions-ico' data-toggle='modal' data-target='#frview' ><img  src='../Content/images/shape-4.png' title='View' class='img-responsive' /></a>"";
                    }
   ");
            WriteLiteral(@"             }
            ],
           
        });


        //oTable1 = $('#txtSearchfield').DataTable();
        $('.searchTerm').on('keyup', function () {
            debugger;
            oTable1.columns(2).search($('#txtSearchfield').val().trim());
            oTable1.columns(3).search($('#txtSearchfield').val().trim());
            oTable1.draw();
        });
    });
    $(document).on(""click"", "".btnDocument"", function (e) {
        $('#divsuccess').html('');
        var $buttonClicked = $(this);
        var status = $buttonClicked.attr('data-status');
        var ID = $buttonClicked.attr('data-id');
        var BankName = $buttonClicked.attr('data-bank');
        //if (status == 'Approved') {
        //    //alert(status);
        //    //alert('KYC document approved by bank');
        //    //return false;
        //}
        url = ""../BankCompany/DrawFundsDocumentList/?ID="" + ID + ""&BankName="" + BankName + ""&Status="" + status;
        window.location.href = url;

      ");
            WriteLiteral("  //href = \'../BankCompany/DrawFundsDocumentList/?ID=\" + data.bankID + \"&BankName=\" + data.bankName + \"\' data - status=\" + data.status + \"\r\n    });\r\n</script>");
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
