#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
# docker build -t app-school . 
# docker run -dp 5055:5055 --name app-school app-school

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY *.sln .
COPY ["App.School.Api/App.School.Api.csproj", "App.School.Api/"]
COPY ["App.School.Infra.IoC/App.School.Infra.IoC.csproj", "App.School.Infra.IoC/"]
COPY ["App.School.Infra.Data/App.School.Infra.Data.csproj", "App.School.Infra.Data/"]
COPY ["App.School.Domain/App.School.Domain.csproj", "App.School.Domain/"]
COPY ["App.School.Application/App.School.Application.csproj", "App.School.Application/"]
COPY ["App.School.Core/App.School.Core.csproj", "App.School.Core/"]
RUN dotnet restore "App.School.Api/App.School.Api.csproj"
COPY . .
WORKDIR "/src/App.School.Api"
RUN dotnet build "App.School.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "App.School.Api.csproj" -c Release -o /app/publish

FROM base AS final
ENV ASPNETCORE_URLS http://+:5055
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "App.School.Api.dll"]
EXPOSE 5055

