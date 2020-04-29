### STAGE 1: Build ###
FROM johnpapa/angular-cli AS build
WORKDIR /usr/src/app
COPY package.json ./
RUN npm install
COPY . .
RUN ng build --configuration=staging


FROM nginx:1.16

## Remove default nginx website
RUN rm -rf /usr/share/nginx/html/*

# copy artifact build from the 'build environment'
COPY  nginx.conf /etc/nginx/conf.d/default.conf
COPY --from=build /usr/src/app/dist/SPA /usr/share/nginx/html
# expose port 80
EXPOSE 80


# run nginx
CMD ["nginx", "-g", "daemon off;"]