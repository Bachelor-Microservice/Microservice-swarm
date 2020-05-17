# Stage 1
#FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
#WORKDIR /build
#COPY . .
#RUN dotnet restore
#RUN dotnet test PriceCalendarService.NUnitTestPriceCalendarService
#RUN dotnet publish -c Release -o /app
# Stage 2
#FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS final
#WORKDIR /app
#COPY --from=build /app .
#ENTRYPOINT ["dotnet", "PriceCalendarService.dll"]
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app
COPY PriceCalendarService.sln ./PriceCalendarService.sln
COPY PriceCalendarService/PriceCalendarService.csproj ./PriceCalendarService/PriceCalendarService.csproj
COPY NUnitTestPriceCalendar/NUnitTestPriceCalendar.csproj ./NUnitTestPriceCalendar/NUnitTestPriceCalendar.csproj
RUN dotnet restore

COPY . ./
#RUN dotnet test . -c Release
RUN dotnet publish PriceCalendarService -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out ./
ENTRYPOINT [ "dotnet", "PriceCalendarService.dll" ]