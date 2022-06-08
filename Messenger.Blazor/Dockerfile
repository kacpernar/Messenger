FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Messenger.Blazor/Messenger.Blazor.csproj", "Messenger.Blazor/"]
RUN dotnet restore "Messenger.Blazor/Messenger.Blazor.csproj"
COPY . .
WORKDIR "/src/Messenger.Blazor"
RUN dotnet build "Messenger.Blazor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Messenger.Blazor.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Messenger.Blazor.dll"]
