#pragma checksum "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\Account\ChangePassword.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "edac85e2dc1e1e7ac42e73884f117b7f3e624923"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Account_ChangePassword), @"mvc.1.0.view", @"/Views/Account/ChangePassword.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Account/ChangePassword.cshtml", typeof(AspNetCore.Views_Account_ChangePassword))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"edac85e2dc1e1e7ac42e73884f117b7f3e624923", @"/Views/Account/ChangePassword.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6b23c2871a53ca46bac6eeea28bffed3162ffb9c", @"/Views/_ViewImports.cshtml")]
    public class Views_Account_ChangePassword : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Finocart.CustomModel.ChangePasswordModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/Content/images/login-logo.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("img-responsive"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 2 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\Account\ChangePassword.cshtml"
  
    ViewData["Title"] = "Change Password";
    Layout = "~/Views/Shared/_LoginLayout.cshtml";

#line default
#line hidden
            BeginContext(152, 4, true);
            WriteLiteral("\r\n\r\n");
            EndContext();
#line 8 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\Account\ChangePassword.cshtml"
 using (Html.BeginForm("setChangePassword", "Common", FormMethod.Post, new { enctype = "multipart/form-data", id = "FormSubmitLogin" }))
{
    

#line default
#line hidden
            BeginContext(302, 23, false);
#line 10 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\Account\ChangePassword.cshtml"
Write(Html.AntiForgeryToken());

#line default
#line hidden
            EndContext();
            BeginContext(331, 369, true);
            WriteLiteral(@"    <div id=""login-box"">
        <div class=""col-xs-12 logbox log-big"">
            <div class=""login-box clearfix"">
                <div style="" display:inline-flex;position: relative;"">
                    <div class=""col-xs-12 col-sm-6 col-md-6 col-lg-6 padding hidden-xs"">
                        <div class=""left-box clearfix"">

                            ");
            EndContext();
            BeginContext(700, 68, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "edac85e2dc1e1e7ac42e73884f117b7f3e6249235338", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(768, 217, true);
            WriteLiteral("\r\n                        </div>\r\n                    </div>\r\n                    <div class=\"col-xs-12 col-sm-6 col-md-6 col-lg-6 padding\" id=\"logsec\">\r\n                        <div class=\"right-box\" id=\"rightbox\">\r\n");
            EndContext();
            BeginContext(1025, 257, true);
            WriteLiteral(@"                                <div class=""row"">
                                    <div class=""col-xs-12"">
                                        <h3>Change Password</h3>
                                        <label class=""log-valid"" id=""lblerror"">");
            EndContext();
            BeginContext(1283, 25, false);
#line 29 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\Account\ChangePassword.cshtml"
                                                                          Write(ViewBag.InvalidCredential);

#line default
#line hidden
            EndContext();
            BeginContext(1308, 246, true);
            WriteLiteral("</label>\r\n                                    </div>\r\n                                </div>\r\n                                <div class=\"row\">\r\n                                    <div class=\"col-xs-12\">\r\n                                        ");
            EndContext();
            BeginContext(1555, 27, false);
#line 34 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\Account\ChangePassword.cshtml"
                                   Write(Html.HiddenFor(m => m.Role));

#line default
#line hidden
            EndContext();
            BeginContext(1582, 42, true);
            WriteLiteral("\r\n                                        ");
            EndContext();
            BeginContext(1625, 29, false);
#line 35 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\Account\ChangePassword.cshtml"
                                   Write(Html.HiddenFor(m => m.UserId));

#line default
#line hidden
            EndContext();
            BeginContext(1654, 42, true);
            WriteLiteral("\r\n                                        ");
            EndContext();
            BeginContext(1697, 28, false);
#line 36 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\Account\ChangePassword.cshtml"
                                   Write(Html.HiddenFor(m => m.Email));

#line default
#line hidden
            EndContext();
            BeginContext(1725, 42, true);
            WriteLiteral("\r\n                                        ");
            EndContext();
            BeginContext(1768, 99, false);
#line 37 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\Account\ChangePassword.cshtml"
                                   Write(Html.PasswordFor(m => m.OldPassword, new { @class = "form-control", placeholder = "Old Password" }));

#line default
#line hidden
            EndContext();
            BeginContext(1867, 265, true);
            WriteLiteral(@"
                                        <label id=""lblOldPasswordError"" class=""error"" style=""color:red;display: none;"">Old Password cannot be empty</label>
                                        <label id=""lblWrongPasswordError"" class=""error"" style=""color:red"">");
            EndContext();
            BeginContext(2133, 25, false);
#line 39 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\Account\ChangePassword.cshtml"
                                                                                                     Write(TempData["WrongPassword"]);

#line default
#line hidden
            EndContext();
            BeginContext(2158, 155, true);
            WriteLiteral("</label>\r\n                                    </div>\r\n                                    <div class=\"col-xs-12\">\r\n                                        ");
            EndContext();
            BeginContext(2314, 99, false);
#line 42 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\Account\ChangePassword.cshtml"
                                   Write(Html.PasswordFor(m => m.NewPassword, new { @class = "form-control", placeholder = "New Password" }));

#line default
#line hidden
            EndContext();
            BeginContext(2413, 2054, true);
            WriteLiteral(@"
                                        <label id=""lblNewPasswordError"" class=""error"" style=""color:red;display: none;"">New Password cannot be empty</label>
                                        <label id=""NewlblNewPasswordError"" class=""error"" style=""color:red;display: none;"">Password should be contain atlest 1 capital , 1 small letters ,1 number and 1 special characters</label>

                                    </div>
                                    <div class=""col-xs-12"">
                                        <input type=""password"" placeholder=""Confirm Password"" id=""ConfirmPassword"" class=""form-control"" />
                                        <label id=""lblConfirmPasswordError"" class=""error"" style=""color:red;display: none;"">Confirm Password cannot be empty</label>
                                        <label id=""lblUnMatchError"" class=""error"" style=""color:red;display: none;"">Confirm Password should be same as new password</label>
                                        <label id=""Ne");
            WriteLiteral(@"wlblConfirmPasswordError"" class=""error"" style=""color:red;display: none;"">Confirm Password should be contain atlest 1 capital , 1 small letters ,1 number and 1 special characters</label>

                                    </div>
                                    <div class=""col-xs-12"">
                                        <div class=""row"">
                                            <div class=""col-xs-12 col-sm-6"">
                                                <button class=""login-btn"" type=""submit"" id=""btnSave"">Submit</button>
                                            </div>
                                            <div class=""col-xs-12 col-sm-6"">
                                                <button class=""clear-btn"" type=""button"" id=""btnCancel"" @*onclick=""ClearAllControls(FormSubmitLogin)""*@>Clear</button>
                                            </div>
                                        </div>
                                    </div>
                                </");
            WriteLiteral("div>\r\n");
            EndContext();
            BeginContext(4508, 132, true);
            WriteLiteral("                        </div>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n");
            EndContext();
            BeginContext(4646, 829, true);
            WriteLiteral(@"    <script>
        $(function () {
            $(""#btnSave"").click(function (e) {

                debugger;
                var errormsg = false;

                var OldPassword = $(""#OldPassword"").val();
                var NewPassword = $(""#NewPassword"").val();
                var ConfirmPassword = $(""#ConfirmPassword"").val();

                if (OldPassword == """") {
                    $(""#lblOldPasswordError"").show();
                    errormsg = true;
                }

                if (NewPassword == """") {
                    $(""#lblNewPasswordError"").show();
                    errormsg = true;
                }
                //password validation
                if (NewPassword != """") {
                    var strongRegex = new RegExp(""^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!");
            EndContext();
            BeginContext(5476, 367, true);
            WriteLiteral(@"@#\$%\^&\*])(?=.{6,})"");
                    if (!strongRegex.test(NewPassword)) {
                        $(""#NewlblNewPasswordError"").show();
                        return false;
                    }

                }
                if (ConfirmPassword != """") {
                    var strongRegex = new RegExp(""^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!");
            EndContext();
            BeginContext(5844, 1481, true);
            WriteLiteral(@"@#\$%\^&\*])(?=.{6,})"");
                    if (!strongRegex.test(ConfirmPassword)) {
                        $(""#NewlblConfirmPasswordError"").show();
                        return false;
                    }
                }

                if (ConfirmPassword == """") {
                    $(""#lblConfirmPasswordError"").show();
                    errormsg = true;
                }

                if (NewPassword != ConfirmPassword) {
                    $(""#lblUnMatchError"").show();
                    errormsg = true;
                }

                //////

                if (errormsg == true) {
                    return false;
                }
                // Validation End
            });
            $('#btnCancel').click(function () {
                debugger;
                $(""#OldPassword"").val('');
                $(""#NewPassword"").val('');
                $(""#ConfirmPassword"").val('');

            });
            $(""#OldPassword"").keypress(function () {");
            WriteLiteral(@"
                $('#lblOldPasswordError').hide();
                $(""#lblWrongPasswordError"").hide();
            });

            $(""#NewPassword"").keypress(function () {
                $('#lblNewPasswordError').hide();
            });

            $(""#ConfirmPassword"").keypress(function () {
                $('#lblConfirmPasswordError').hide();
                $('#lblUnMatchError').hide();
            });
        });


    </script>
");
            EndContext();
#line 153 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\Account\ChangePassword.cshtml"
}

#line default
#line hidden
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Finocart.CustomModel.ChangePasswordModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
