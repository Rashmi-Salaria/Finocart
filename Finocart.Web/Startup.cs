using System;
using System.IO;
using System.Net;
using System.Text;
using Finocart.DBContext;
using Finocart.Interface;
using Finocart.Services;
using Finocart.Web.Controllers;
using Finocart.Web.Views.Common;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Finocart.Web
{
    public class Startup
    {
        /// <summary>
        /// Startup Configuration
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;

               
            });

            services.AddOptions();

            ////Register Appsetting---------------------------------------------------------- 
            services.AddSingleton<IConfiguration>(Configuration);
            ////-----------------------------------------------------------------------------

            ////Register DB Context
            services.AddDbContext<VMSContext>(options => options.UseSqlServer(Configuration.GetConnectionString("EmployeeDatabase")));

            // services.AddIdentity<IdentityUser, IdentityRole>(options => options.Stores.MaxLengthForKeys = 128)
            //.AddEntityFrameworkStores<VMSContext>()
            //.AddDefaultUI()
            //  .AddDefaultTokenProviders();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //JWT Configuration
            var SecretKey = Encoding.ASCII.GetBytes(Configuration["Auth0:ClientSecret"]);

            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(token =>
            {
                token.RequireHttpsMetadata = false;
                token.SaveToken = true;
                token.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(SecretKey),
                    ValidateIssuer = true,
                    ValidIssuer = Configuration["URL:ApplicationUrl"],
                    ValidateAudience = true,
                    ValidAudience = Configuration["URL:ApplicationUrl"],
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            ////----------Register Interface-------------------------------------------------


            services.AddScoped<IVendor, VendorService>();
            services.AddScoped<IEmployee, EmployeeService>();
            services.AddScoped<ILookupDetails, LookupDetailsService>();
            services.AddScoped<IUser, UserService>();
            services.AddScoped<IInvoice, InvoiceService>();
            services.AddScoped<IPurchaseOrder, PurchaseOrderService>();
            services.AddScoped<IAdmin, AdminService>();
            services.AddScoped<IFinoCartMaster, FinocartMasterService>();
            services.AddScoped<ICommon, CommonService>();
            services.AddScoped<IAnchorCompany, AnchorCompanyService>();
            services.AddScoped<IVendor, VendorService>();
            services.AddScoped<ICompany, CompanyService>();
            services.AddScoped<INotification, NotificationService>();
            services.AddScoped<IBucketManagement, BucketManagementService>();
            services.AddScoped<IVendorAnalytics, VendorAnalyticsService>();
            services.AddScoped<IBank, BankService>();
            services.AddScoped<CustomFilter>();

            ////-----------------------------------------------------------------------------
            //services.AddMvc(options =>
            //{
            //    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            //});

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);//You can set Time   
                options.Cookie.HttpOnly = true;
               
            });
            services.AddMvc();

          
                //services.AddAuthorization(options =>
                //{
                //    options.AddPolicy("RequireAdministratorRole",
                //         policy => policy.RequireRole("SuperAdmin", "MasterAdmin", "Vendor", "Anchor Company", "InternalUser"));
                //});

        
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Account/UserLogin";
                //options.LogoutPath = $"/Account/Logout";
                //options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSession();
            app.UseCookiePolicy();

            app.Use(async (context, next) =>
            {
                var JWToken = context.Session.GetString("JWToken");
                if (!string.IsNullOrEmpty(JWToken))
                {
                    context.Request.Headers.Add("Authorization", "Bearer " + JWToken);
                }
                await next();
            });
            app.UseAuthentication();

            string userName = string.Empty;
            string roleName = string.Empty;
            app.Use((context, next) =>
            {
               
                roleName = context.Session.GetString("Role");
                userName = context.Session.GetString("userName");
                

                return next();
            });

           


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                app.UseExceptionHandler(appError =>
                {
                    appError.Run(async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        context.Response.ContentType = "application/json";

                        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                        if (contextFeature != null)
                        {
                            context.Response.ContentType = "text/html";

                            string errorMessage = contextFeature.Error.Message;
                            string stackTrace = contextFeature.Error.StackTrace;

                            WritErrorLog(errorMessage, stackTrace, roleName, userName);

                            var exceptionHandlerPathFeature =
                                context.Features.Get<IExceptionHandlerPathFeature>();

                            if (exceptionHandlerPathFeature?.Error is FileNotFoundException)
                            {
                                await context.Response.WriteAsync("File error thrown!<br><br>\r\n");
                            }

                            try
                            {
                                app.UseExceptionHandler("~/Home/Error");
                            }
                            catch (Exception)
                            {

                                throw;
                            }

                        }
                    });
                });
                //app.UseExceptionHandler("/Home/Error");
                //app.UseExceptionHandler(errorApp =>
                //{
                //    errorApp.Run(async context =>
                //    {
                //        //context.Response.StatusCode = 500;
                //        context.Response.ContentType = "text/html";

                //        await context.Response.WriteAsync("<html lang=\"en\"><body>\r\n");
                //        await context.Response.WriteAsync("ERROR!<br><br>\r\n");

                //        var exceptionHandlerPathFeature =
                //            context.Features.Get<IExceptionHandlerPathFeature>();

                //        // Use exceptionHandlerPathFeature to process the exception (for example, 
                //        // logging), but do NOT expose sensitive error information directly to 
                //        // the client.

                //        if (exceptionHandlerPathFeature?.Error is FileNotFoundException)
                //        {
                //            await context.Response.WriteAsync("File error thrown!<br><br>\r\n");
                //        }

                //        await context.Response.WriteAsync("<a href=\"/\">Home</a><br>\r\n");
                //        await context.Response.WriteAsync("</body></html>\r\n");
                //        await context.Response.WriteAsync(new string(' ', 512)); // IE padding
                //    });
                //});
            }
            app.UseStaticFiles();
           // app.UseSession();
           // app.UseStaticFiles();
           
           // app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    //template: "{controller=Home}/{action=Index}/{id?}");
                    //template: "{controller=Employee}/{action=Index}/{id?}");
                    //template: "{controller=Employee}/{action=Users}/{id?}");
                    template: "{controller=Account}/{action=SuperAdminLogin}/{id?}");
                //template: "{controller=Account}/{action=ValidateVendorLogOn}/{id?}");
            });
            //

            //CreateRoles(serviceProvider);



            ////Handler for the Vendor Document Folder 
            app.MapWhen(
                context =>
                (
                    context.Request.Path.ToString().Contains(Configuration["VendorDocumentsFolder"])
                    || context.Request.Path.ToString().Contains(Configuration["AnchorDocumentsFolder"])
                ),
                appBuilder =>
                {
                    appBuilder.RequestHanlderMiddleware();
                });
        }

        #region WriteErrorLog Method          
        /// <summary>
        /// Write Error Log
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <param name="stackTrace"></param>
        /// <param name="roleName"></param>
        /// <param name="userName"></param>
        private void WritErrorLog(string errorMessage, string stackTrace, string roleName, string userName)
        {

            string fileDirectory = Configuration["ErrorLogPath"] + roleName + "\\" + userName;
            string fileName = fileDirectory + "\\" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";
            if (!Directory.Exists(fileDirectory))
            {
                Directory.CreateDirectory(fileDirectory);
            }
            if (!File.Exists(fileName))
            {
                try
                {
                    StreamWriter sw1 = File.CreateText(fileName);
                    sw1.Close();
                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }
            try
            {

                StreamWriter sw = File.AppendText(fileName);
                sw.WriteLine("**************************" + DateTime.Now.ToString() + "*****************************");
                sw.WriteLine();
                sw.WriteLine("-------------------ErrorMessage---------------------");
                sw.WriteLine();
                sw.WriteLine(errorMessage);
                sw.WriteLine();
                sw.WriteLine("-------------------Error Details--------------------");
                sw.WriteLine();
                sw.WriteLine(stackTrace);
                sw.WriteLine();
                sw.WriteLine("**********************************End Here******************************************");
                sw.WriteLine();
                //sw.Flush();
                sw.Close();


            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        #endregion 

        //private void CreateRoles(IServiceProvider serviceProvider)
        //{
        //    //var scopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();
        //    //var scope = scopeFactory.CreateScope();

        //    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        //    var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        //    Task<IdentityResult> roleResult;
        //    string email = "rashmi.salaria@brainvire.com";

        //    //Check that there is an Administrator role and create if not
        //    Task<bool> hasAdminRole = roleManager.RoleExistsAsync("Administrator");
        //    hasAdminRole.Wait();

        //    if (!hasAdminRole.Result)
        //    {
        //        roleResult = roleManager.CreateAsync(new IdentityRole("Administrator"));
        //        roleResult.Wait();
        //    }

        //    //Check if the admin user exists and create it if not
        //    //Add to the Administrator role

        //    Task<ApplicationUser> testUser = userManager.FindByEmailAsync(email);
        //    testUser.Wait();

        //    if (testUser.Result == null)
        //    {
        //        ApplicationUser administrator = new ApplicationUser();
        //        administrator.Email = email;
        //        administrator.UserName = email;
        //        var Password = SecurityHelperService.Encrypt("P@ssw0rd");
        //        Task<IdentityResult> newUser = userManager.CreateAsync(administrator, "P@ssw0rd");
        //        newUser.Wait();

        //        if (newUser.Result.Succeeded)
        //        {
        //            Task<IdentityResult> newUserRole = userManager.AddToRoleAsync(administrator, "Administrator");
        //            newUserRole.Wait();
        //        }
        //    }

        //}
    }


}