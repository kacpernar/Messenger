FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Message_Service/Message_Service.csproj", "Message_Service/"]
RUN dotnet restore "Message_Service/Message_Service.csproj"
COPY . .
WORKDIR "/src/Message_Service"
RUN dotnet build "Message_Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Message_Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Message_Service.dll"]
