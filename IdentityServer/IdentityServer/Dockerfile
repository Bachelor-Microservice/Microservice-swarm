# Stage 1
FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /build
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /app
# Stage 2
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS final
WORKDIR /app
COPY --from=build /app .
ENV ASPNETCORE_URLS: http://+:80
EXPOSE 80
ENTRYPOINT ["dotnet", "IdentityServer.dll"]