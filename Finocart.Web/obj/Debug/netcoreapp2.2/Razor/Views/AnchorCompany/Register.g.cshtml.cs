#pragma checksum "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\AnchorCompany\Register.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f1569c4f1af6eb292a17a68fff66b37d6f018191"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_AnchorCompany_Register), @"mvc.1.0.view", @"/Views/AnchorCompany/Register.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/AnchorCompany/Register.cshtml", typeof(AspNetCore.Views_AnchorCompany_Register))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f1569c4f1af6eb292a17a68fff66b37d6f018191", @"/Views/AnchorCompany/Register.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6b23c2871a53ca46bac6eeea28bffed3162ffb9c", @"/Views/_ViewImports.cshtml")]
    public class Views_AnchorCompany_Register : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Finocart.Model.Company>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("text/javascript"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/AnchorCompany/AnchorCompany.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 2 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\AnchorCompany\Register.cshtml"
  
    ViewData["Title"] = "Register";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(122, 186, true);
            WriteLiteral("<style type=\"text/css\">\r\n    .cls-error {\r\n        display: block;\r\n        color: red;\r\n    }\r\n\r\n    .cls-none {\r\n        display: none;\r\n    }\r\n</style>\r\n<script>\r\n    var CheckPan = \'");
            EndContext();
            BeginContext(309, 39, false);
#line 17 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\AnchorCompany\Register.cshtml"
               Write(Url.Action("CheckPan", "AnchorCompany"));

#line default
#line hidden
            EndContext();
            BeginContext(348, 26, true);
            WriteLiteral("\';\r\n    var CheckEmail = \'");
            EndContext();
            BeginContext(375, 41, false);
#line 18 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\AnchorCompany\Register.cshtml"
                 Write(Url.Action("CheckEmail", "AnchorCompany"));

#line default
#line hidden
            EndContext();
            BeginContext(416, 253, true);
            WriteLiteral("\';\r\n\r\n    $(function () {\r\n        $(\"form\").submit(function () {\r\n            var selTypeText = $(\"#InterestedAs option:selected\").text();\r\n            $(\"#hidText\").val(selTypeText);\r\n        });\r\n    });\r\n</script>\r\n<div class=\"main-inner-section\">\r\n");
            EndContext();
#line 28 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\AnchorCompany\Register.cshtml"
     using (Html.BeginForm("Register", "AnchorCompany", FormMethod.Post, new { enctype = "multipart/form-data", id = "FormSubmitRegister" }))
    {
        

#line default
#line hidden
            BeginContext(828, 23, false);
#line 30 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\AnchorCompany\Register.cshtml"
   Write(Html.AntiForgeryToken());

#line default
#line hidden
            EndContext();
            BeginContext(853, 489, true);
            WriteLiteral(@"        <div class=""content-upper-section"">
            <div class=""row"">
                <div class=""col-xs-12 col-sm-12"">
                    <div class=""main-head clearfix"" style=""background: #fff;border-bottom: solid 1px #e3e3e3;padding: 15px 0;margin: 0 0 15px 0;border-radius: 0;"">
                        <div class=""col-xs-12 col-sm-4"">
                            <h2>Registration</h2>
                        </div>
                        
                    </div>

");
            EndContext();
            BeginContext(1784, 65, true);
            WriteLiteral("\r\n                    <div style=\"display:none;\" id=\"divsuccess\">");
            EndContext();
            BeginContext(1850, 19, false);
#line 49 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\AnchorCompany\Register.cshtml"
                                                          Write(TempData["success"]);

#line default
#line hidden
            EndContext();
            BeginContext(1869, 297, true);
            WriteLiteral(@"</div>

                </div>
                <div class=""col-md-12 col-sm-12 col-xs-12"">
                    <div class=""main-content-box page-scroll"">

                        <div class=""registration-form"">
                            <div class=""row"">
                                ");
            EndContext();
            BeginContext(2167, 32, false);
#line 57 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\AnchorCompany\Register.cshtml"
                           Write(Html.HiddenFor(m => m.CompanyID));

#line default
#line hidden
            EndContext();
            BeginContext(2199, 225, true);
            WriteLiteral("\r\n                                <div class=\"col-xs-12 col-sm-6\">\r\n                                    <label class=\"control-label\">Company Name:<span style=\"color:red;\">*</span></label>\r\n                                    ");
            EndContext();
            BeginContext(2425, 149, false);
#line 60 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\AnchorCompany\Register.cshtml"
                               Write(Html.TextBoxFor(m => m.Company_name, new { @class = "form-control", @placeholder = "Enter Company Name*", @maxLength = "250", autocomplete = "off" }));

#line default
#line hidden
            EndContext();
            BeginContext(2574, 354, true);
            WriteLiteral(@"
                                    <label id=""lblCompanyName"" class=""cls-error""></label>
                                </div>
                                <div class=""col-xs-12 col-sm-6"">
                                    <label class=""control-label"">PAN Number:<span style=""color:red;"">*</span></label>
                                    ");
            EndContext();
            BeginContext(2929, 144, false);
#line 65 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\AnchorCompany\Register.cshtml"
                               Write(Html.TextBoxFor(m => m.Pan_number, new { @class = "form-control", @placeholder = "Enter PAN Number*", @maxLength = "10", autocomplete = "off" }));

#line default
#line hidden
            EndContext();
            BeginContext(3073, 397, true);
            WriteLiteral(@"
                                    <label id=""lblPan"" class=""cls-error""></label>
                                </div>
                            </div>
                            <div class=""row"">
                                <div class=""col-xs-12 col-sm-6"">
                                    <label class=""control-label"">Select Type:</label>
                                    ");
            EndContext();
            BeginContext(3471, 151, false);
#line 72 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\AnchorCompany\Register.cshtml"
                               Write(Html.DropDownListFor(m => m.FactoryOrReverseFactory, new SelectList(ViewBag.look, "ID", "LookUpValue"), "Select Type", new { @class = "form-control" }));

#line default
#line hidden
            EndContext();
            BeginContext(3622, 442, true);
            WriteLiteral(@"
                                    <input type=""hidden"" id=""hidText"" name=""hidText"" />
                                    <label id=""lblfactory"" class=""cls-error""></label>
                                </div>
                                <div class=""col-xs-12 col-sm-6"">
                                    <label class=""control-label"">Interested As:<span style=""color:red;"">*</span></label>
                                    ");
            EndContext();
            BeginContext(4065, 144, false);
#line 78 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\AnchorCompany\Register.cshtml"
                               Write(Html.DropDownListFor(m => m.InterestedAs, new SelectList(ViewBag.LookUp, "ID", "LookUpValue"), "Interested As", new { @class = "form-control" }));

#line default
#line hidden
            EndContext();
            BeginContext(4209, 531, true);
            WriteLiteral(@"
                                    <input type=""hidden"" id=""hidText"" name=""hidText"" />
                                    <label id=""lblInterestedas"" class=""cls-error""></label>
                                </div>

                            </div>
                            <div class=""row"">

                                <div class=""col-xs-12 col-sm-6"">
                                    <label class=""control-label"">CIN Number:<span style=""color:red;"">*</span></label>
                                    ");
            EndContext();
            BeginContext(4741, 145, false);
#line 88 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\AnchorCompany\Register.cshtml"
                               Write(Html.TextBoxFor(m => m.Contact_CIN, new { @class = "form-control", @placeholder = "Enter CIN Number*", @maxLength = "50", autocomplete = "off" }));

#line default
#line hidden
            EndContext();
            BeginContext(4886, 316, true);
            WriteLiteral(@"
                                    <label id=""lblCIN"" class=""cls-error""></label>
                                </div>
                                <div class=""col-xs-12 col-sm-6"">
                                    <label class=""control-label"">Select Status:</label>
                                    ");
            EndContext();
            BeginContext(5203, 133, false);
#line 93 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\AnchorCompany\Register.cshtml"
                               Write(Html.DropDownListFor(m => m.status, new SelectList(ViewBag.Details, "ID", "Status"), "Selectstatus", new { @class = "form-control" }));

#line default
#line hidden
            EndContext();
            BeginContext(5336, 688, true);
            WriteLiteral(@"
                                    <input type=""hidden"" id=""hidText"" name=""hidText"" />
                                    <label id=""lblstatus"" class=""cls-error""></label>
                                </div>
                            </div>
                            <div class=""row"">

                                <div class=""col-xs-12 col-sm-6"">
                                    <label class=""control-label"">Contact Person:<span style=""color:red;"">*</span></label>
                                    <div class=""row"">
                                        <div class=""col-xs-4 col-sm-3"" style=""padding-right:0;"">
                                            ");
            EndContext();
            BeginContext(6025, 445, false);
#line 104 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\AnchorCompany\Register.cshtml"
                                       Write(Html.DropDownListFor(m=>m.Contact_Title, new List<SelectListItem>
                                   {
                                       new SelectListItem{ Text="Mr.", Value = "Mr." },
                                       new SelectListItem{ Text="Ms.", Value = "Ms." },
                                       new SelectListItem{ Text="Mrs.", Value = "Mrs." },

                                   }, new { @class = "form-control"}));

#line default
#line hidden
            EndContext();
            BeginContext(6470, 197, true);
            WriteLiteral("\r\n\r\n\r\n                                        </div>\r\n                                        <div class=\"col-xs-8 col-sm-9\" style=\"padding-left:5px;\">\r\n                                            ");
            EndContext();
            BeginContext(6668, 152, false);
#line 115 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\AnchorCompany\Register.cshtml"
                                       Write(Html.TextBoxFor(m => m.Contact_Name, new { @class = "form-control", @placeholder = "Name of Contact Person*", autocomplete = "off", @maxLength = "50" }));

#line default
#line hidden
            EndContext();
            BeginContext(6820, 457, true);
            WriteLiteral(@"
                                            <label id=""lblContactPerson"" class=""cls-error""></label>
                                        </div>
                                    </div>
                                </div>
                                <div class=""col-xs-12 col-sm-6"">
                                    <label class=""control-label"">Designation:<span style=""color:red;"">*</span></label>
                                    ");
            EndContext();
            BeginContext(7278, 154, false);
#line 122 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\AnchorCompany\Register.cshtml"
                               Write(Html.TextBoxFor(m => m.Contact_Designation, new { @class = "form-control", @placeholder = "Enter Designation*", autocomplete = "off", @maxLength = "50" }));

#line default
#line hidden
            EndContext();
            BeginContext(7432, 407, true);
            WriteLiteral(@"
                                    <label id=""lblDesignation"" class=""cls-error""></label>
                                </div>
                            </div>



                            <div class=""row"">
                                <div class=""col-xs-12 col-sm-6"">
                                    <label class=""control-label"">Address:</label>
                                    ");
            EndContext();
            BeginContext(7840, 118, false);
#line 132 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\AnchorCompany\Register.cshtml"
                               Write(Html.TextBoxFor(m => m.Address, new { @class = "form-control", @placeholder = "Enter Address", autocomplete = "off" }));

#line default
#line hidden
            EndContext();
            BeginContext(7958, 263, true);
            WriteLiteral(@"

                                </div>
                                <div class=""col-xs-12 col-sm-6"">
                                    <label class=""control-label"">Email ID:<span style=""color:red;"">*</span></label>
                                    ");
            EndContext();
            BeginContext(8222, 146, false);
#line 137 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\AnchorCompany\Register.cshtml"
                               Write(Html.TextBoxFor(m => m.Contact_email, new { @class = "form-control", @placeholder = "Enter Email ID*", @maxLength = "150", autocomplete = "off" }));

#line default
#line hidden
            EndContext();
            BeginContext(8368, 407, true);
            WriteLiteral(@"
                                    <label id=""lblEmail"" class=""cls-error""></label>
                                </div>
                            </div>
                            <div class=""row form-group"">
                                <div class=""col-xs-12 col-sm-6"">
                                    <label class=""control-label"">Comments:</label>
                                    ");
            EndContext();
            BeginContext(8776, 152, false);
#line 144 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\AnchorCompany\Register.cshtml"
                               Write(Html.TextAreaFor(m => m.Contact_Comments, new { @class = "form-control", @placeholder = "Comments (If Any)", @maxLength = "500", autocomplete = "off" }));

#line default
#line hidden
            EndContext();
            BeginContext(8928, 266, true);
            WriteLiteral(@"
                                </div>
                                <div class=""col-xs-12 col-sm-6"">
                                    <label class=""control-label"">Mobile Number:<span style=""color:red;"">*</span></label>
                                    ");
            EndContext();
            BeginContext(9195, 151, false);
#line 148 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\AnchorCompany\Register.cshtml"
                               Write(Html.TextBoxFor(m => m.Contact_mobile, new { @class = "form-control", @placeholder = "Enter Mobile Number*", @maxLength = "10", autocomplete = "off" }));

#line default
#line hidden
            EndContext();
            BeginContext(9346, 1401, true);
            WriteLiteral(@"
                                    <label id=""lblMobileNo"" class=""cls-error""></label>
                                </div>
                            </div>
                            <div class=""row"">
                               
                                    <button class=""submit-btn m-right"" type=""button"" id=""btnedit"">Edit</button>
                                    <button class=""cancel-btn"" type=""button"" onclick=""BackBtn()"" style=""margin-left: 1px;"">Back</button>
                             
                                
                                    <div style=""display:none;float:left;"" id=""dvSubmit"">
                                        <button class=""submit-btn"" type=""submit"" id=""btnSubmit"" style=""margin-right: 17px;margin-left: 13px;"">Submit</button>
                                        <button class=""submit-btn m-right"" type=""button"" id=""btnDelete"">Delete</button>
                                        <button class=""cancel-btn"" type=""button"" onclick=""Cl");
            WriteLiteral(@"earAllControls(FormSubmitRegister)"" style=""margin-right: 17px;margin-left: 275px;"">Clear</button>

                                    </div>
                                
                                
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        ");
            EndContext();
            BeginContext(10747, 79, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f1569c4f1af6eb292a17a68fff66b37d6f01819121464", async() => {
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
            BeginContext(10826, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(10830, 737, true);
            WriteLiteral(@"        <script>
            $(document).ready(function () {
                if ($('#Company_name').val() == '')
                {
                    $(""#btnedit"").hide();
                    $(""#btnDelete"").hide();
                    $(""#dvSubmit"").show();


                }
                $(function () {

                    if ($('#Company_name').val() != '') {

                        $('input[type=""text""]').attr(""disabled"", ""disabled"");
                    $("".form-control"").prop(""disabled"", true);
                    $('input[type=""text""]').attr(""disabled"", """");
                        $("".form-control"").prop(""disabled"", true);
                    }
                });

                var url = '");
            EndContext();
            BeginContext(11568, 42, false);
#line 195 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\AnchorCompany\Register.cshtml"
                      Write(Url.Action("CompanyList", "AnchorCompany"));

#line default
#line hidden
            EndContext();
            BeginContext(11610, 5330, true);
            WriteLiteral(@"';

                if ($('#divsuccess').html() == 'success') {
                    $('input[type=""text""]').val('');
                    $('#Contact_Comments').val('');
                    //  Common.CustomSuccessNotify(""Registered and email sent"");
                    alert(""You have successfully registered. An email with credentials to login has been sent at your mailid."");
                    location.href = url;
                    $('#divsuccess').html('');

                }

            });
            $(function () {
                $(""#btnSubmit"").click(function (e) {

                    var companyName = $('#Company_name').val();
                    var CIN = $('#Contact_CIN').val();
                    var panNumber = $('#Pan_number').val();
                    var regpan = /^([a-zA-Z]){5}([0-9]){4}([a-zA-Z]){1}?$/;
                    var password = $('#Password').val();
                    var confirmPassword = $('#Confirm_Password').val();
                    var contactPe");
            WriteLiteral(@"rson = $('#Contact_Name').val();
                    var confirmPassword = $('#Confirm_Password').val();
                    var designation = $('#Contact_Designation').val();
                    var email = $('#Contact_email').val();
                    var mobile = $('#Contact_mobile').val();
                    var SelInterestedAs = $(""#InterestedAs"").val();
                    var selecttype = $(""FactoryOrReverseFactory"").val();
                    var status = $(""Selectstatus"").val();
                    var count = 0;
                    $('.cls-error').text('');
                    if (companyName == '') {
                        $('#lblCompanyName').text('Enter the Company name');
                        count++;
                    }
                    if (CIN == '') {
                        $('#lblCIN').text('Enter the CIN No');
                        count++;
                    }

                    if (panNumber == '') {
                        $('#lblPan').show();
      ");
            WriteLiteral(@"                  $('#lblPan').text('Enter the Pan number');
                        count++;
                    }
                    if (regpan.test(panNumber)) {

                    } else {
                        $('#lblPan').show();
                        $('#lblPan').text('Please enter valid Pan number');
                    }
                    if (contactPerson == '') {
                        $('#lblContactPerson').text('Enter the Contact person');
                        count++;
                    }
                    if (designation == '') {
                        $('#lblDesignation').text('Enter the Designation');
                        count++;
                    }
                    if (email == '') {

                        $('#lblEmail').show();
                        $('#lblEmail').text('Enter the Email Id');
                        count++;
                    }
                    if (mobile == '') {
                        $('#lblMobileNo').text('Enter");
            WriteLiteral(@" the Mobile No');
                        count++;
                    }

                    if (email != '') {
                        if (!IsEmail(email)) {
                            $('#lblEmail').text('Please enter valid Email Id.');
                            count++;
                        }
                        else {
                            $('#lblEmail').text('');
                        }
                    }
                    if (password != confirmPassword) {
                        $('#lblconfirmPassword').text('Confirm password should match with password')
                        count++;
                    }
                    if (SelInterestedAs == 0) {
                        $(""#lblInterestedas"").text('Select at least one type');
                        count++;
                    }
                    if (selecttype == 0)
                    {
                        $(""#lblfactory"").text('Select at least one type');
                        count++;");
            WriteLiteral(@"
                    }

                    if (count == 0 && $('.cls-error').text() == '') {
                        return true;
                    }
                    else {
                        return false;
                    }
                });

            });


            $(function () {
                $('#btnedit').click(function (e) {
                    debugger
                    $('input[type=""text""]').removeAttr('disabled');
                    $("".form-control"").prop(""disabled"", false);

                }, function () {

                    $('input[type=""text""]').removeAttr('disabled');
                    $("".form-control"").prop(""disabled"", false);


                    //disable edit button on click
                    $(""#btnedit"").hide();
                    $(""#dvSubmit"").show();
                    $(""#dvClear"").show();

                });

            });

            $('#btnDelete').click(function () {
                debugger;
        ");
            WriteLiteral("        var companyName = $(\'#Company_name\').val();\r\n                var CIN = $(\'#Contact_CIN\').val();\r\n                var panNumber = $(\'#Pan_number\').val();\r\n             $.ajax({\r\n\r\n                 url: \'");
            EndContext();
            BeginContext(16941, 27, false);
#line 328 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\AnchorCompany\Register.cshtml"
                  Write(Url.Action("DeleteCompany"));

#line default
#line hidden
            EndContext();
            BeginContext(16968, 679, true);
            WriteLiteral(@"',
                 data: { companyName: $('#Company_name').val(), panNumber: $('#Pan_number').val(), id: $('.xyz').val() },
                 type: ""POST"",//use id here
              dataType: ""json"",
             success: function () {
            alert(""Data has been deleted."");
            LoadData();
        },
        //error: function () {
        //    alert(""Error while deleting data"");
        //}
                });

            });
            function ClearAllControls(formid) {
                Common.ClearAllControls(formid)
            }

            function BackBtn() {
                history.back();
            }

        </script>
");
            EndContext();
#line 351 "D:\Rashmi\Projects\Finocart\Development_V1\Finocart\Finocart.Web\Views\AnchorCompany\Register.cshtml"
    }

#line default
#line hidden
            BeginContext(17654, 16, true);
            WriteLiteral("\r\n</div>\r\n\r\n\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Finocart.Model.Company> Html { get; private set; }
    }
}
#pragma warning restore 1591
