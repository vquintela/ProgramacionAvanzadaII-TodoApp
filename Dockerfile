FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80



FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /app
COPY TodoApp/TodoApp.csproj .
RUN dotnet restore
COPY . .
RUN dotnet build

FROM build AS publish
RUN dotnet publish -o /publish



FROM base AS final
WORKDIR /app
COPY --from=publish /publish .
ENTRYPOINT ["dotnet", "TodoApp.dll"]
