FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ExampleBlazorServerApp/ExampleBlazorServerApp.csproj", "ExampleBlazorServerApp/"]
RUN dotnet restore "ExampleBlazorServerApp/ExampleBlazorServerApp.csproj"
COPY . .
WORKDIR "/src/ExampleBlazorServerApp"
RUN dotnet build "ExampleBlazorServerApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ExampleBlazorServerApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ExampleBlazorServerApp.dll"]
