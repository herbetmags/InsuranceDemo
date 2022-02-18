using Insurance.Business.Managers;
using Insurance.Common.Interfaces.Business;
using Insurance.Common.Interfaces.Repositories;
using Insurance.Common.Interfaces.Services;
using Insurance.Common.Models.AppSettings;
using Insurance.Common.Models.Bases;
using Insurance.Data.Repositories;
using Insurance.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Insurance.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Insurance API - DEMO", Version = "v1" });
                //c.AddSecurityDefinition("", new OpenApiSecurityScheme
                //{
                //TODO    
                //});
            });
            //services.AddDbContext<InsuranceDbContext>(opt =>
            //            opt.UseSqlServer(Configuration["ConnectionStrings:InsuranceDB"]));
            ConfigureTransientRepositories(services);
            ConfigureSingletonServices(services);
            ConfigureSingletonDataManagers(services);
            ConfigureAppSettingsTypes(services);
            ConfigureAuthentication(services);
            ConfigureAuthorization(services);
            services.AddMvcCore()
                    .AddMvcOptions(c => c.EnableEndpointRouting = false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Insurance API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void ConfigureAuthentication(IServiceCollection services)
        {
            var tokenSettings = Configuration.GetSection("TokenKeys").Get<TokenSettings>();
            services
                .AddAuthentication(opt =>
                {
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(opt =>
                {
                    opt.SaveToken = true;
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenSettings.JwtTokenKey)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }

        public void ConfigureAuthorization(IServiceCollection services)
        {
            services.AddAuthorizationCore(opt =>
            {
                opt.AddPolicy("AdminOnly", policy =>
                    policy.RequireRole("Admin"));
                opt.AddPolicy("AdminAndAgent", policy =>
                    policy.RequireRole("Admin", "Agent"));
                opt.AddPolicy("AdminAndClient", policy =>
                    policy.RequireRole("Admin", "Client"));
                opt.AddPolicy("AgentOnly", policy =>
                    policy.RequireRole("Agent"));
                opt.AddPolicy("AgentAndClient", policy =>
                    policy.RequireRole("Agent", "Client"));
                opt.AddPolicy("ClientOnly", policy =>
                    policy.RequireRole("Client"));
            });
        }

        public void ConfigureTransientRepositories(IServiceCollection services)
        {
            services.AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
        }

        public void ConfigureSingletonServices(IServiceCollection services)
        {
            services.AddSingleton<IAccountsService, AccountsService>();
            services.AddSingleton<IJwtAuthenticationService, AuthenticationService>();

            services.AddSingleton<IPoliciesService, PoliciesService>();
            services.AddSingleton<IRolesService, RolesService>();
            services.AddSingleton<IStatusService, StatusService>();
            services.AddSingleton<IUserPoliciesService, UserPoliciesService>();
            services.AddSingleton<IUsersService, UsersService>();
        }


        public void ConfigureSingletonDataManagers(IServiceCollection services)
        {
            services.AddSingleton<IAccountsManager, AccountsManager>();
            services.AddSingleton<IJwtAuthenticationManager, JwtAuthenticationManager>();

            services.AddSingleton<IPoliciesDataManager, PoliciesDataManager>();
            services.AddSingleton<IRolesDataManager, RolesDataManager>();
            services.AddSingleton<IStatusDataManager, StatusDataManager>();
            services.AddSingleton<IUserPoliciesDataManager, UserPoliciesDataManager>();
            services.AddSingleton<IUsersDataManager, UsersDataManager>();
        }

        public void ConfigureAppSettingsTypes(IServiceCollection services)
        {
            var tokenSection = Configuration.GetSection("TokenKeys");
            var connectionStrings = Configuration.GetSection("ConnectionStrings");
            var pageSettings = Configuration.GetSection("PageSettings");
            services.Configure<TokenSettings>(tokenSection);
            services.Configure<ConnectionStrings>(connectionStrings);
            services.Configure<PageSettings>(pageSettings);
        }
    }
}