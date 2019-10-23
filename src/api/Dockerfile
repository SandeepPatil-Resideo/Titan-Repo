FROM ceresacr.azurecr.io/dotnet/core/aspnetcore-runtime-2.2:2.2.6 AS base
WORKDIR /app  
USER root
COPY --chown=titan:titan published .
USER titan
ENTRYPOINT ["dotnet", "Titan.Ufc.Addresses.API.dll"]
