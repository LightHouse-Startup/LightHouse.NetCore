using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Restful.Api.Filters;
using Restful.Api.Repositories;
using Restful.Api.Services;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;
using System.Linq;

namespace Restful.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
            .AddControllers()
            .AddNewtonsoftJson(options =>
            {
                //Json组件默认为System.Text.Json，这里添加Newtonsoft.Json
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            })
            .AddMvcOptions(options =>
            {
                //默认只支持json，这里添加xml返回
                options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
            });

            services.AddScoped<ICompanyService, CompanyService>();
            services.AddDbContext<SqliteDbContext>(options =>
            {
                options.UseSqlite("Data Source=lighthouse.db");
            });

            //添加Api版本控制服务——Microsoft.AspNetCore.Mvc.Versioning
            services.AddApiVersioning(options =>
            {
                //如果设置为true, 在Api的响应头部，会追加当前Api支持的版本：api-supported-versions
                options.ReportApiVersions = true;

                //配置默认接口版本为1.0
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);

                //在查询字符串(Query String)中指定Api版本
                //options.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
                //在查询字符串(Query String)或请求头(HTTP Header)中指定Api版本
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new QueryStringApiVersionReader(),
                    new HeaderApiVersionReader() { HeaderNames = { "x-api-version" } });
            });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(options =>
            {      //分别注册v1和v2
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Restful API",
                    //服务描述
                    Description = "A simple example ASP.NET Core Web Restful API",
                    //API服务条款的URL
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "xiecf",
                        Email = string.Empty,
                        Url = new Uri("https://github.com/xiecf007"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license"),
                    }
                });

                options.SwaggerDoc("v2", new OpenApiInfo
                {
                    Version = "v2",
                    Title = "Restful API",
                    Description = "A simple example ASP.NET Core Web Restful API",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "xiecf",
                        Email = string.Empty,
                        Url = new Uri("https://github.com/xiecf007"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license"),
                    }
                });

                options.DocInclusionPredicate((docName, apiDesc) =>
                {
                    var versions = apiDesc
                                    .CustomAttributes()
                                    .OfType<ApiVersionAttribute>()
                                    .SelectMany(attr => attr.Versions);

                    return versions.Any(v => $"v{v.ToString()}" == docName);
                });

                options.OperationFilter<RemoveVersionParameterOperationFilter>();
                options.DocumentFilter<SetVersionInPathDocumentFilter>();

                //加载xml文档
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{typeof(Startup).Assembly.GetName().Name}.xml"), true);
                //如果需要包含额外的xml需要放开如下注释
                //options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"*.xml"), true);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStatusCodePages();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v2/swagger.json", "My API V2");
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");

                options.RoutePrefix = string.Empty;
                options.DocumentTitle = "Restful API";
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
