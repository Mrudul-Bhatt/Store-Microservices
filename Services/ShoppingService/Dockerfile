#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

# FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
# WORKDIR /app
# EXPOSE 80

# FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
# WORKDIR /src
# COPY ["Services/ShoppingService/ShoppingService.csproj", "Services/ShoppingService/"]
# RUN dotnet restore "Services/ShoppingService/ShoppingService.csproj"
# COPY . .
# WORKDIR "/src/Services/ShoppingService"
# RUN dotnet build "ShoppingService.csproj" -c Release -o /app/build

# FROM build AS publish
# RUN dotnet publish "ShoppingService.csproj" -c Release -o /app/publish /p:UseAppHost=false

# FROM base AS final
# WORKDIR /app
# COPY --from=publish /app/publish .
# ENTRYPOINT ["dotnet", "ShoppingService.dll"]

FROM mrudb/microservicebase:latest AS base
COPY nginx.conf /etc/nginx/sites-available/default
COPY cert.pem /etc/nginx/conf.d/cert.pem
COPY key.pem /etc/nginx/conf.d/key.pem
WORKDIR /app
ENV ASPNETCORE_URLS http://+:8000
EXPOSE 8000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src/ShoppingService
COPY ./ShoppingService.csproj .

RUN dotnet restore -nowarn:msb3202,nu1503
COPY . .
RUN pwd
RUN ls -la
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .

COPY supervisord.conf /etc/supervisor/conf.d/supervisord.conf
ENTRYPOINT ["/usr/bin/supervisord"]
