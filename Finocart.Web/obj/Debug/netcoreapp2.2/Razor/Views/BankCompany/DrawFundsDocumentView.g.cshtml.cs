#pragma checksum "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\BankCompany\DrawFundsDocumentView.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e04235098232f8293b4bfeb208bbd4ce4077a7c2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_BankCompany_DrawFundsDocumentView), @"mvc.1.0.view", @"/Views/BankCompany/DrawFundsDocumentView.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/BankCompany/DrawFundsDocumentView.cshtml", typeof(AspNetCore.Views_BankCompany_DrawFundsDocumentView))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e04235098232f8293b4bfeb208bbd4ce4077a7c2", @"/Views/BankCompany/DrawFundsDocumentView.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6b23c2871a53ca46bac6eeea28bffed3162ffb9c", @"/Views/_ViewImports.cshtml")]
    public class Views_BankCompany_DrawFundsDocumentView : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Finocart.CustomModel.GetDocument_Download>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/BankCompany/DrawFundsDocumentView.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 2 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\BankCompany\DrawFundsDocumentView.cshtml"
  
    ViewData["Title"] = "DrawFundsHistoryList";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(153, 267, true);
            WriteLiteral(@"<style>
    .modal-dialog {
        width: 600px;
        margin: 30px auto;
    }

    .hide_column {
        display: none;
    }
</style>
<a class=""backbtn"" href=""javascript:history.back()""><i class=""fas fa-long-arrow-alt-left"" title=""Go Back""></i></a>
");
            EndContext();
            BeginContext(420, 62, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e04235098232f8293b4bfeb208bbd4ce4077a7c24330", async() => {
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
            BeginContext(482, 45, true);
            WriteLiteral("\r\n<script>\r\n    var DrawFundsDocumentView = \'");
            EndContext();
            BeginContext(528, 54, false);
#line 19 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\BankCompany\DrawFundsDocumentView.cshtml"
                            Write(Url.Action("DrawFundsDocumentViewList", "BankCompany"));

#line default
#line hidden
            EndContext();
            BeginContext(582, 32, true);
            WriteLiteral("\'\r\n    var Document_download = \'");
            EndContext();
            BeginContext(615, 46, false);
#line 20 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\BankCompany\DrawFundsDocumentView.cshtml"
                        Write(Url.Action("Document_download", "BankCompany"));

#line default
#line hidden
            EndContext();
            BeginContext(661, 756, true);
            WriteLiteral(@"'
</script>
<div class=""main-inner-section"">
    <div class=""content-upper-section"" id=""AnchorCompany"">
        <div class=""tabsec-main"">
            <div class=""tab-content"">
                <div class=""row"" id="""">
                    <div class=""col-md-12 col-sm-12 col-xs-12"">
                        <div class=""main-table-box page-scroll"" style=""padding:15px;"">
                            <h4 style=""font-size:15px;font-weight:bold;"">Document Download</h4>
                        </div>
                    </div>
                    <div class=""col-md-12 col-sm-12 col-xs-12"">
                        <div class=""main-table-box page-scroll"">
                            <div class=""table-responsive"">
                                ");
            EndContext();
            BeginContext(1418, 25, false);
#line 35 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\BankCompany\DrawFundsDocumentView.cshtml"
                           Write(Html.HiddenFor(m => m.ID));

#line default
#line hidden
            EndContext();
            BeginContext(1443, 814, true);
            WriteLiteral(@"
                                <table id=""DrawFundsDocumentView"" class=""table table-responsive table-hover table-bordered tbl_UserList dataTable no-footer"">
                                    <thead>
                                        <tr>
                                            <th></th>
                                            <th>File Name</th>
                                            <th style=""width:10%"">Action</th>
                                        </tr>
                                    </thead>
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Finocart.CustomModel.GetDocument_Download> Html { get; private set; }
    }
}
#pragma warning restore 1591
