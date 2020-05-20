# NetCore中Api整合swagger

## 添加nuget包
`dotnet add package Swashbuckle.AspNetCore`

## swagger服务注册
```csharp
// Register the Swagger generator, defining 1 or more Swagger documents
services.AddSwaggerGen(options =>
{     
    //分别注册v1和v2
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
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

    /*
    //api多版本时可放开
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
    */

    //加载xml文档
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{typeof(Startup).Assembly.GetName().Name}.xml"), true);
    //如果需要包含额外的xml需要放开如下注释
    //options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"*.xml"), true);
});
```
上面加载的是当前项目对应的xml文件
* 在vs里面，可以右键项目属性——输出，勾选“ XML文档文件”，项目编译时会自动生成；
* 如果用的vscode，可以直接修改.csproj文件，添加如下
```xml
<PropertyGroup Condition="'$(Configuration)|$(Platform)'=='Debug|AnyCPU'">
<DocumentationFile>..\Restful.Api.xml</DocumentationFile>
</PropertyGroup>
```

## swagger配置中间件
```csharp
// Enable middleware to serve generated Swagger as a JSON endpoint.
app.UseSwagger();

// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    //options.SwaggerEndpoint("/swagger/v2/swagger.json", "My API V2");
    
    options.RoutePrefix = string.Empty;
    options.DocumentTitle = "Restful API";
});

app.UseMvc();
```

## 添加Bearer Token 认证
```csharp
services.AddSwaggerGen(option =>
{
    // ...

    // Add security definitions
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Description = "Please enter into field the word 'Bearer' followed by a space and the JWT value",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference()
            {
                Id = "Bearer",
                Type = ReferenceType.SecurityScheme
            }
        }, Array.Empty<string>() }
    });
});
```

## 参考
* [asp.net core 3.0 中使用 swagger](https://www.cnblogs.com/weihanli/p/ues-swagger-in-aspnetcore3_0.html)
