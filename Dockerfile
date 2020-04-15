FROM nginx:1.16

## Remove default nginx website
RUN rm -rf /usr/share/nginx/html/*

# copy artifact build from the 'build environment'
COPY nginx.conf /etc/nginx/nginx.conf
# expose port 80
EXPOSE 80


# run nginx
CMD ["nginx", "-g", "daemon off;"]