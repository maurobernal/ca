##########################################
#Build
FROM maurobernal/net8-build AS build

##Pass Solution
WORKDIR /
COPY . .

WORKDIR /src/
RUN dotnet build Web/Web.csproj -c Release -o /app/build

##########################################
#Runtime
FROM maurobernal/net8-base as base
USER root
WORKDIR /app
EXPOSE 	80
EXPOSE 443
ENV ASPNETCORE_HTTP_PORTS=80;
ENV ASPNETCORE_HTTPS_PORTS=443;
ENV ASPNETCORE_URLS=http://*:80/;https://*:443/;
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/root/.aspnet/https/CertificadoCA.pfx
ENV ASPNETCORE_Kestrel__Certificates__Default__Password=Pass2024!
WORKDIR /root/
COPY https .aspnet/https/


##########################################
FROM build as publish
USER root
RUN dotnet publish Web/Web.csproj -c Release -o /app/publish
FROM base as final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet" , "ca.Web.dll"]