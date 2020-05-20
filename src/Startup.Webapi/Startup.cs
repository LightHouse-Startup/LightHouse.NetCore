using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Startup.Webapi.Services;

namespace Startup.Webapi
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
            services
             .AddControllers()
             .AddJsonOptions(options =>
             {
                //netcore中默认采用，如果需要兼容PascalCase风格，可以重新设置默认值
                //去掉默认返回json时的camel case的转化
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
                /*
                //如下为netcore2.x下写法
                if (options.SerializerSettings.ContractResolver is DefaultContractResolver resolver)
                {
                    resolver.NamingStrategy = null;
                }*/
             })
             .AddMvcOptions(Options =>
             {
                //默认只支持json，这里添加xml返回
                Options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
             });
             
#if DEBUG
            services.AddTransient<IMailService, LocalMailService>();
#else
            services.AddTransient<IMailService, CloudMailService>();
#endif
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory log)
        {
            log.AddProvider(new NLogLoggerProvider());
            //log.AddNLog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStatusCodePages();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //     app.Map("/jump", HereIAm);

            //     app.Run(async context =>
            //    {
            //        await context.Response.WriteAsync("Hello world!");
            //    });
        }

        private static void HereIAm(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Here I Am");
            });
        }
    }
}
