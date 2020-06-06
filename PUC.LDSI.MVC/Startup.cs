using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PUC.LDSI.DataBase;
using PUC.LDSI.IoC;
using PUC.LDSI.MVC.AutoMapper;

namespace PUC.LDSI.MVC
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<AppDbContext>(
                o => o.UseSqlServer(Configuration.GetConnectionString("Conexao"),
                x => x.MigrationsAssembly("PUC.LDSI.DataBase")));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            NativeBootStrapperBase.RegisterServices(services);

            AutoMapperConfig.RegisterMappings();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Professor", policy => policy.RequireRole("Professor"));
                options.AddPolicy("Aluno", policy => policy.RequireRole("Aluno"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
