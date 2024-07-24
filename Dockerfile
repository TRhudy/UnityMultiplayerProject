#Build node image from Node Docker Hub
FROM node:14

#Identify working directory
WORKDIR /app

#Copy package
COPY package*.json /app/

#Install rpm package from package.json
RUN npm install

#Expose server at port, accessible outside of container
EXPOSE 8080

#Command to start app
CMD ["npm", "server.js"]
