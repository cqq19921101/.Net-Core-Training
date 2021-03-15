#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["HangFire.Job/HangFire.Job.csproj", "HangFire.Job/"]
COPY ["HangFire.HttpApi/HangFire.HttpApi.csproj", "HangFire.HttpApi/"]
COPY ["HangFire.Application/HangFire.Application.csproj", "HangFire.Application/"]
COPY ["HangFire.Application.Caching/HangFire.Application.Caching.csproj", "HangFire.Application.Caching/"]
COPY ["HangFire.Application.EventBus/HangFire.Application.EventBus.csproj", "HangFire.Application.EventBus/"]
COPY ["HangFire.Domain/HangFire.Domain.csproj", "HangFire.Domain/"]
COPY ["HangFire.Domain.Shared/HangFire.Domain.Shared.csproj", "HangFire.Domain.Shared/"]
COPY ["HangFire.Common/HangFire.Common.csproj", "HangFire.Common/"]
COPY ["HangFire.Application.Contracts/HangFire.Application.Contracts.csproj", "HangFire.Application.Contracts/"]
COPY ["HangFire.Swagger/HangFire.Swagger.csproj", "HangFire.Swagger/"]
COPY ["HangFire.EntityFrameworkCore/HangFire.EntityFrameworkCore.csproj", "HangFire.EntityFrameworkCore/"]
COPY ["HangFire.BackgroundJobs/HangFire.BackgroundJobs.csproj", "HangFire.BackgroundJobs/"]
RUN dotnet restore "HangFire.Job/HangFire.Job.csproj"
COPY . .
WORKDIR "/src/HangFire.Job"
RUN dotnet build "HangFire.Job.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HangFire.Job.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HangFire.Job.dll"]