FROM alpine:latest

LABEL maintainer="entreur@gmail.com"

RUN apk update
RUN apk add --no-cache git
RUN apk add --no-cache openrc
RUN apk add --no-cache openssh
RUN apk add --no-cache python3

EXPOSE 22