using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PUC.LDSI.Identity.Context;
using PUC.LDSI.Identity.Entities;

[assembly: HostingStartup(typeof(PUC.LDSI.MVC.Areas.Identity.IdentityHostingStartup))]
namespace PUC.LDSI.MVC.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<SecurityContext>(
                    opc => opc.UseSqlServer(context.Configuration.GetConnectionString("ConexaoIdentity"),
                    prj => prj.MigrationsAssembly("PUC.LDSI.Identity")));

                services.AddDefaultIdentity<Usuario>().AddEntityFrameworkStores<SecurityContext>();

                services.Configure<IdentityOptions>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequiredUniqueChars = 0;
                });
            });
        }
    }
}