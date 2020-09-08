using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using OdeToFood.Data;
using System;
using System.IO;

namespace OdeToFood
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
            services.AddDbContextPool<OdeToFoodDbContext>(option =>
            {
                option.UseSqlServer(Configuration.GetConnectionString("OdeToFoodDb"));
            });

             services.AddScoped<IRestaurantData, SqlRestaurantData>();
            //services.AddScoped<IRestaurantData, InMemoryRestaurantData>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddRazorPages();
            services.AddControllers();
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
       // public void Configure(IApplicationBuilder app, IHostingEnvironment env)
       public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();  //HSTS instructs the browser to only access information across a secure connection
            }

            //app.Use(SayHelloMiddleware);

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseNodeModules();
            app.UseCookiePolicy();

           // app.UseMvc();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });

        }

        private RequestDelegate SayHelloMiddleware(RequestDelegate arg)
        {
            return async ctx =>
            {
                if (ctx.Request.Path.StartsWithSegments("/hello"))
                {
                    await ctx.Response.WriteAsync("Hello World");
                }
                else
                {
                    //await next(ctx);
                }
                
            };
        }
    }
}
