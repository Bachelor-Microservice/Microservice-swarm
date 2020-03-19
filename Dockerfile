FROM alpine:3.8 AS generate
WORKDIR /certificates
RUN apk update && \
    apk add --no-cache openssl && \
    rm -rf /var/cache/apk/*
RUN openssl req -x509 -nodes -days 365 -newkey rsa:2048 -keyout microservices.key -out microservices.crt -subj "/C=GB"

FROM nginx:1.15.8-alpine
RUN mkdir -p /var/www/cache
COPY nginx.conf /etc/nginx/nginx.conf
COPY --from=generate /certificates/microservices.key /certificates/microservices.crt /etc/nginx/ssl/