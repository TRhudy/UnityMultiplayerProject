#Build node image from Node Docker Hub
FROM node:14

#Identify working directory
WORKDIR /app

#Copy Json package to working directory
COPY package*.json ./

#Install npm package from package.json
RUN npm install

#Copy server.js to working directory

#Expose server at port, accessible outside of container
EXPOSE 8080

#Command to start app
CMD ["npm", "server.js"]
