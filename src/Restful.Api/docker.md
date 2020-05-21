# 容器化部署ASP.NET Core项目

假设已经有一个基于ASP.NET Core的WebApi项目Restful.Api，下面简单记录下容器化部署过程

## 创建Dockerfile
如果用的是VisualStudio，可以在项目右键，选择“添加”-“Docker支持”，会自动生成如下Dockerfile内容
```Dockerfile
#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["Restful.Api.csproj", ""]
RUN dotnet restore "./Restful.Api.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Restful.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Restful.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Restful.Api.dll"]
```

## 构建镜像

```shell
docker build -f "../Restful.Api/Dockerfile" --force-rm -t restfulapi:dev --target base  --label "com.microsoft.created-by=visual-studio" --label "com.microsoft.visual-studio.project-name=Restful.Api" "../Restful.Api" 
```
以上为VisualStudio编译镜像时用到的命令，可以简化为：

`docker build --force-rm -t restfulapi:dev .`

build 命令参数：（更多参数查看`docker build --help`）
* -f 
    >- 指定Dockerfile文件路径，也可以是绝对路径，如果不指定，就在当前目录下找
* --force-rm 
    >-  Always remove intermediate containers
* -t
    >- restfulapi 将映像命名为 restfulapi，
    >- dev 设置镜像的tag(一般为版本号)
    >- tag可以省略，默认为latest
* --target
    >- 设置要构建的目标构建阶段
* --label
    >- Set metadata for an image    
* .  
    >- 在当前文件夹中查找（Dockerfile文件）     

ps：
* dangling images（没有标签并且没有被容器使用的镜像）
    >- 表现为`docker images`列表中REPOSITORY和TAg都为<none>
    >- 清除命令：`docker image prune` 

## 运行容器

```shell
docker run -dt -v "C:\Users\dev\vsdbg\vs2017u5:/remote_debugger:rw" -v "D:\workspace-light-house\light-house-7\netcore-startup-github\Restful.Api:/app" -v "D:\workspace-light-house\light-house-7\netcore-startup-github\Restful.Api:/src/" -v "C:\Users\dev\.nuget\packages\:/root/.nuget/fallbackpackages2" -v "C:\Program Files\dotnet\sdk\NuGetFallbackFolder:/root/.nuget/fallbackpackages" -e "DOTNET_USE_POLLING_FILE_WATCHER=1" -e "ASPNETCORE_LOGGING__CONSOLE__DISABLECOLORS=true" -e "ASPNETCORE_ENVIRONMENT=Development" -e "NUGET_PACKAGES=/root/.nuget/fallbackpackages2" -e "NUGET_FALLBACK_PACKAGES=/root/.nuget/fallbackpackages;/root/.nuget/fallbackpackages2" -P --name Restful.Api --entrypoint tail restfulapi:dev -f /dev/null 
```
以上在VisualStudio中利用Docker调试时用到的命令，可以简化为：

`docker run -d --rm -p 5000:80 --name restfulapi restfulapi:dev`

或

`docker run -it --rm -p 5000:80 --name restfulapi restfulapi:dev`

run命令参数（更多参数查看`docker run --help`）
* -dt
    >- -d 在后台运行容器，并打印容器id
    >- -it 接管容器控制台
* -v
    >- 绑定挂载卷
* -e 
    >- 指定环境变量值        
* --rm
    >- 容器在退出时自动删除
* -p
    >- 将本地计算机上的端口 5000 映射到容器中的端口 80(`EXPOSE 80`)
* -P    
    >- 这里是大写，随机指定端口
* --name
    >- 指定容器名称
* `restfulapi:dev`
    >- 指定创建容器依赖的镜像（版本）    
* --entrypoint
    >- 
* -f         
    >- 
