FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app
COPY CustomerManagerService.sln ./CustomerManagerService.sln
COPY CustomerManagerService/CustomerManagerService.csproj ./CustomerManagerService/CustomerManagerService.csproj
COPY NUnitTestCustomerManager/NUnitTestCustomerManager.csproj ./NUnitTestCustomerManager/NUnitTestCustomerManager.csproj
RUN dotnet restore

COPY . ./
#RUN dotnet test . -c Release
RUN dotnet publish CustomerManagerService -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out ./
ENTRYPOINT [ "dotnet", "CustomerManagerService.dll" ]