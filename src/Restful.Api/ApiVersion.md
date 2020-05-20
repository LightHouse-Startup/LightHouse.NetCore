# NetCore中Api版本控制

## 添加nuget包
`dotnet add package Microsoft.AspNetCore.Mvc.Versioning`

## 添加版本服务
指定api版本的3重形式：

1. 使用路由约束中指定请求Api的版本
    ```csharp
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    ```
    请求api path：`api/v1/[controller]`

2. 在查询字符串(Query String)中指定Api版本
    ```csharp
    [ApiVersion("1")]
    [Route("api/[controller]")]
    ```
    请求api path：`api/[controller]?api-version=1`

3. 在请求头(HTTP Header)中指定Api版本
    ```csharp
    [ApiVersion("1")]
    [Route("api/[controller]")]
    ```
    ```
    Request Header:
        x-api-version:1
    ```
    请求api path：`api/[controller]`

ps:
* `ApiVersion`可以标注在类或方法上，多版本api使用不同命名空间或者`xxV1/xxV2`命名区分即可
* 路由约束方式不能同其他2种并存，其他两种可以使用`ApiVersionReader.Combine`提供并存支持。    

ConfigureServices：
```csharp
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
```
补充：
* `ReportApiVersions`为true时，api的response header里会多一个`api-supported-versions`，描述当前api支持的版本
* `AssumeDefaultVersionWhenUnspecified`，标记当客户端没有指定版本号的时候，是否使用默认版本号
* `DefaultApiVersion`，配置默认接口版本
* `ApiVersionReader`，设置api版本可以从哪里获取，可以从如下任一处获取：
    
    a) `url?api-version=xxx`

    b) `request header: x-api-version`

## 弃用旧版本
`[ApiVersion("1.0", Deprecated = true)]`

api response header 会出现过时的和支持的版本，分别为：`api-deprecated-versions`，`api-supported-versions`

## 标记勿需版本的Api
```csharp
[ApiVersionNeutral]
[Route("api/[controller]")]
[ApiController]
```
