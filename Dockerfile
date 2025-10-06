FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app
COPY src .
RUN dotnet restore "BlazorShWebsite.Server/BlazorShWebsite.Server.csproj"
WORKDIR /app/BlazorShWebsite.Server
RUN dotnet publish "BlazorShWebsite.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0
EXPOSE 8080
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "BlazorShWebsite.Server.dll"]