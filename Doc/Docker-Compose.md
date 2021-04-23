# Docker-compose
Project created to run the API and MongoDB development.   
It was created for Visual Studio attache and enable debugging.   
   
The main file is called "docker-compose.yml".   
It is a file that contains all the configuration so that it is possible to execute the containers.
   
```cpp
version: '3.4'

services:
  thelasthotel.api:
    image: ${DOCKER_REGISTRY-}thelasthotelapi
    build:
      context: .
      dockerfile: TheLastHotel.API/Dockerfile
    environment:
      DatabaseName: TheLastHotelDB
      ConnectionString: mongodb://root:MongoDB2021@mongo:27017
    networks:
      - alten-compose-network

  mongo:
    image: mongo
    environment:
      MONGO_INITDB_ROOT_USERNAME: root
      MONGO_INITDB_ROOT_PASSWORD: MongoDB2021
    ports:
      - "27017:27017"
    volumes:
      - /Docker/Volumes/MongoDB:/data/db
    networks:
      - alten-compose-network

networks: 
    alten-compose-network:
```
   
### Details:
In the *docker-compose.yml* file, two containers were declared, *thelasthotel.api* and *mongo*.

O container *mongo* tem duas variaveis de ambiente que devem ser informadas e os valores serão usados na API.   
* MONGO_INITDB_ROOT_USERNAME: nome de usuário administrador do banco local.
* MONGO_INITDB_ROOT_PASSWORD: senha de usuário administrador do banco local.   
  
In the settings of *thelasthotel.api* there are two main points:   

* build-> dockerfile: it maps the Dockerfile file into the TheLastHotel.API folder. Dockerfile is the file responsible for packaging the projects and transforming them into a docker image.
* environment-> DatabaseName: variable with the name of the database that will be created in MongoDB/CosmosDB.
* environment-> ConnectionString: Connection string with the path to the database.


