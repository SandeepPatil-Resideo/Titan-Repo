FROM microsoft/dotnet:2.2-sdk AS publish

WORKDIR /src
COPY . .
COPY nuget.config /

RUN dotnet nuget locals all --clear    
RUN dotnet restore --no-cache
RUN dotnet build -c Release
RUN dotnet publish 'titan-address-api.csproj' -c Release -o /app

FROM microsoft/dotnet:2.2-aspnetcore-runtime AS final

EXPOSE 5000/tcp

WORKDIR /app
COPY --from=publish /app .
COPY *.json /app/

CMD ["dotnet", "titan-address-api.dll", "--server.urls", "http://*:5000"]