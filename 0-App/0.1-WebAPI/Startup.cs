using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using WishList.Domain;
using WishList.Domain.Services;
using WishList.Repository;

namespace WishList.WebApi
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
            services.AddDbContext<DataContext>(x =>
                x.UseSqlite(Configuration.GetConnectionString("DefaultConn"), b => b.MigrationsAssembly("WishList.WebApi"))
            );
            services.AddSwaggerGen(c =>
                c.SwaggerDoc("v1", new Info { Title = "Wishes", Version = "v1"})
            );
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(opcoes =>
                {
                    opcoes.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore; // Remove valores nulos das respostas
                    opcoes.SerializerSettings.MaxDepth = int.MaxValue;
                    opcoes.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                }); ;

            services.AddScoped<IProductServices, ProductServices>();
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IWishServices, WishServices>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Wishes"));

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
