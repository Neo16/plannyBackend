
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PlannyBackend.Services;
using PlannyBackend.Interfaces;
using PlannyBackend.Models.Identity;
using Swashbuckle.AspNetCore.Swagger;
using AutoMapper;
using System.Reflection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using PlannyBackend.Bll.Interfaces;
using PlannyBackend.Bll.Services;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Http;
using PlannyBackend.Common.Configurations;
using PlannyBackend.DAL;
using PlannyBackend.Web.WebServices;
using PlannyBackend.Web.Middlewares;

namespace PlannyBackend.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private IConfigurationSection ConfigurationSectionAzureBlob { get; }
        private IConfigurationSection ConfigurationSectionToken { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ConfigurationSectionAzureBlob = Configuration.GetSection("AzureBlob");
            ConfigurationSectionToken = configuration.GetSection("Token");
        }      

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AzureBlobConfiguration>(ConfigurationSectionAzureBlob);
            services.Configure<TokenConfiguration>(ConfigurationSectionToken);

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication()
              .AddCookie()
              .AddJwtBearer(options =>
              {
                  options.RequireHttpsMetadata = false;
                  options.SaveToken = true;
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      IssuerSigningKey = new SymmetricSecurityKey(
                         Encoding.UTF8.GetBytes(ConfigurationSectionToken.Get<TokenConfiguration>().SigningKey)
                      ),
                      ValidateAudience = false,
                      ValidIssuer = "https://localhost:44381/"
                  };
              });
                      
            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IPlannyService, PlannyService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<TokenService>();
            services.AddTransient<CurrentUserService>();
            services.AddTransient<IActionContextAccessor, ActionContextAccessor>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IFileService, FileService>();
            services.AddCors();
            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Planny API", Version = "v1" });

                // Swagger 2.+ support
                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                };
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                c.AddSecurityRequirement(security);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env,
            ApplicationDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseCors(builder =>
               builder.AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials()
                      .WithOrigins("http://localhost:3000")
            );

            app.UseStaticFiles();
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseAuthentication();
            app.UseSwagger();


            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Planny API");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            Mapper.Initialize(cfg =>
            {
                cfg.AddProfiles(GetType().GetTypeInfo().Assembly);
            });

            context.Seed();

        }
    }
}
