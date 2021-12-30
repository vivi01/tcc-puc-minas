#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/ModuloInformacaoCadastral/GISA.Prestador/GISA.Prestador.csproj", "src/ModuloInformacaoCadastral/GISA.Prestador/"]
RUN dotnet restore "src/ModuloInformacaoCadastral/GISA.Prestador/GISA.Prestador.csproj"
COPY . .
WORKDIR "/src/src/ModuloInformacaoCadastral/GISA.Prestador"
RUN dotnet build "GISA.Prestador.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GISA.Prestador.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GISA.Prestador.dll"]