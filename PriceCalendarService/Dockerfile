# Stage 1
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /build
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /app
#Attempt at testing stage
FROM build AS tests
WORKDIR /build/PriceCalendarService/NUnitTestPriceCalendar
CMD ["dotnet", "test"]
# Stage 2
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "PriceCalendarService.dll"]