# Acesse https://aka.ms/customizecontainer para saber como personalizar seu contêiner de depuração e como o Visual Studio usa este Dockerfile para criar suas imagens para uma depuração mais rápida.

# Esta fase é usada durante a execução no VS no modo rápido (Padrão para a configuração de Depuração)
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
RUN addgroup -S nonroot \
    && adduser -S nonroot -G nonroot \
    && apk add --no-cache ffmpeg
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# Esta fase é usada para compilar o projeto de serviço
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["libs/Fiap.FileCut/Fiap.FileCut.Infra.Api", "../../libs/Fiap.FileCut/Fiap.FileCut.Infra.Api"]
COPY ["libs/Fiap.FileCut/Fiap.FileCut.Core", "../../libs/Fiap.FileCut/Fiap.FileCut.Core"]
COPY ["libs/Fiap.FileCut/Fiap.FileCut.Infra.RabbitMq", "../../libs/Fiap.FileCut/Fiap.FileCut.Infra.RabbitMq"]
COPY ["libs/Fiap.FileCut/Fiap.FileCut.Infra.Storage", "../../libs/Fiap.FileCut/Fiap.FileCut.Infra.Storage"]
COPY ["libs/Fiap.FileCut/Fiap.FileCut.Infra.IdentityProvider", "../../libs/Fiap.FileCut/Fiap.FileCut.Infra.IdentityProvider"]
COPY ["libs/Fiap.FileCut/Fiap.FileCut.Processing", "../../libs/Fiap.FileCut/Fiap.FileCut.Processing"]
COPY ["src/Fiap.FileCut.Processamento.Api", "Fiap.FileCut.Processamento.Api"]
RUN dotnet restore "./Fiap.FileCut.Processamento.Api/Fiap.FileCut.Processamento.Api.csproj"
WORKDIR "/src/Fiap.FileCut.Processamento.Api"
RUN dotnet build "./Fiap.FileCut.Processamento.Api.csproj" -c "$BUILD_CONFIGURATION" -o /app/build

# Esta fase é usada para publicar o projeto de serviço a ser copiado para a fase final
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Fiap.FileCut.Processamento.Api.csproj" -c "$BUILD_CONFIGURATION" \
  -o /app/publish /p:UseAppHost=false

# Esta fase é usada na produção ou quando executada no VS no modo normal (padrão quando não está usando a configuração de Depuração)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
USER nonroot
ENTRYPOINT ["dotnet", "Fiap.FileCut.Processamento.Api.dll"]
