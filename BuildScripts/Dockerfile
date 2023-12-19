FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
RUN apt-get -y update
RUN apt-get install -y software-properties-common
RUN apt-get install -y gnupg2
RUN add-apt-repository -y ppa:nginx/stable
RUN apt-get -y update; exit 0
RUN apt-get install -y nginx

RUN apt-get install -y supervisor
RUN mkdir -p /var/log/microservice
