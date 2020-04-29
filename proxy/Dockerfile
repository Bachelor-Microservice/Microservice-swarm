FROM nginx

WORKDIR /
RUN rm -rf /usr/share/nginx/html/*
# copy artifact build from the 'build environment'
COPY nginx.conf /etc/nginx/nginx.conf
# expose port 80
