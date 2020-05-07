FROM nginx

WORKDIR /
COPY ./silent-refresh.html /usr/share/nginx/html/

# copy artifact build from the 'build environment'
COPY nginx.conf /etc/nginx/nginx.conf
# expose port 80
