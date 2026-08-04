FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY . .

WORKDIR /app/app-financeiro-flow

RUN dotnet restore

RUN dotnet publish -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS runtime

WORKDIR /app

COPY --from=build app/out .

ENTRYPOINT ["dotnet", "app-financeiro-flow.dll"]