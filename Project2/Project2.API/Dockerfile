
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Project2.API/Project2.API.csproj", "Project2.API/"]
COPY ["Project2.Domain/Project2.Domain.csproj", "Project2.Domain/"]
COPY ["Project2.Data/Project2.Data.csproj", "Project2.Data/"]
RUN dotnet restore "Project2.API/Project2.API.csproj"
COPY . .
WORKDIR "/src/Project2.API"
RUN dotnet build "Project2.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Project2.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Project2.API.dll"]