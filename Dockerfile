FROM mcr.microsoft.com/dotnet/sdk:3.1 AS runtime
COPY . /app
WORKDIR /app
 
EXPOSE 5000/tcp
CMD ASPNETCORE_URLS=http://*:5000 dotnet stu.hubs.vn
ENV ASPNETCORE_ENVIRONMENT docker
