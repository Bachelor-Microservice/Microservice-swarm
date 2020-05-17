FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app
COPY BookingService.sln ./BookingService.sln
COPY BookingService/BookingService.csproj ./BookingService/BookingService.csproj
COPY NUnitTestBookingService/NUnitTestBookingService.csproj ./NUnitTestBookingService/NUnitTestBookingService.csproj
RUN dotnet restore

COPY . ./
RUN dotnet test . -c Release
RUN dotnet publish BookingService -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out ./
ENTRYPOINT [ "dotnet", "BookingService.dll" ]