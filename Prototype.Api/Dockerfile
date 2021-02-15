#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
#USER ContainerAdministrator
#RUN net localgroup administrators /add "User Manager\ContainerUser"
#USER ContainerUser
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
USER ContainerAdministrator
RUN net localgroup administrators /add "User Manager\ContainerUser"
USER ContainerUser
WORKDIR /src
ENV HTTP_PROXY=http://192.200.200.250:3128
ENV HTTPS_PROXY=http://192.200.200.250:3128
COPY Prototype.Shared.1.0.0.nupkg /src
COPY nuget.config /src
COPY ["Prototype.Api/Prototype.Api.csproj", "Prototype.Api/"] .
RUN dotnet restore "Prototype.Api/Prototype.Api.csproj" --configfile c:/src/nuget.config
COPY . .
WORKDIR "/src/Prototype.Api"
RUN dotnet build "Prototype.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Prototype.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Prototype.Api.dll"]


#  docker build -t [your_image_name] .
#  docker build --force-rm -t falbarn/prototypeapi:dev --target base  --label "com.microsoft.created-by=visual-studio" --label "com.microsoft.visual-studio.project-name=Prototype.Api" ".\Prototype.Api" 

# docker run -p 8080:5000 -v $(pwd):/app [your_image_name]
#  docker run -dt -v $(pwd):/app" -v "$:c:\src" -v "..\..\..\Appdata\Roaming\Microsoft\UserSecrets:C:\Users\ContainerUser\AppData\Roaming\Microsoft\UserSecrets:ro" -v "..\..\..\AppData\Roaming\ASP.NET\Https:C:\Users\ContainerUser\AppData\Roaming\ASP.NET\Https:ro" -v "..\..\..\.nuget\packages\:c:\.nuget\fallbackpackages" -e "DOTNET_USE_POLLING_FILE_WATCHER=1" -e "ASPNETCORE_LOGGING__CONSOLE__DISABLECOLORS=true" -e "ASPNETCORE_ENVIRONMENT=Development" -e "ASPNETCORE_URLS=http://+:80" -e "NUGET_PACKAGES=c:\.nuget\fallbackpackages" -e "NUGET_FALLBACK_PACKAGES=c:\.nuget\fallbackpackages" -P --name Prototype.Api --entrypoint C:\remote_debugger\x64\msvsmon.exe prototypeapi:dev /noauth /anyuser /silent /nostatus /noclrwarn /nosecuritywarn /nofirewallwarn /nowowwarn /fallbackloadremotemanagedpdbs /timeout:2147483646 /LogDebuggeeOutputToStdOut 
