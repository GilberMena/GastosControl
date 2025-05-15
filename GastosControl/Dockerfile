# Dockerfile para GastosControl (.NET) + SQL Server en Railway
# ===========================================
# Este Dockerfile construye y ejecuta tu app ASP.NET Core
# Railway no permite ejecutar múltiples contenedores, así que la BD debe ir por separado

# ----------- Etapa 1: build --------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar csprojs y restaurar paquetes
COPY GastosControl.sln ./
COPY GastosControl.Application/*.csproj ./GastosControl.Application/
COPY GastosControl.Domain/*.csproj ./GastosControl.Domain/
COPY GastosControl.Infrastructure/*.csproj ./GastosControl.Infrastructure/
COPY ./*.csproj ./GastosControl/

RUN dotnet restore ./GastosControl/GastosControl.csproj

# Copiar todo y compilar
COPY . ./
WORKDIR /app/GastosControl
RUN dotnet publish -c Release -o /out

# ----------- Etapa 2: runtime -------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /out .

# Puerto por defecto (ajustar si usas otro)
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "GastosControl.dll"]
