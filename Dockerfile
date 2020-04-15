FROM nginx:1.15.8-alpine
RUN mkdir -p /var/www/cache
COPY nginx.conf /etc/nginx/nginx.conf