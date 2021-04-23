# Docker-compose
Projet créé pour exécuter le développement de l'API et de MongoDB.
Il a été créé pour Visual Studio attaché et permet le débogage.
   
Le fichier principal s'appelle "docker-compose.yml".   
C'est un fichier qui contient toute la configuration pour qu'il soit possible d'exécuter les conteneurs.
   
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
   
### Des détails:
Dans le fichier *docker-compose.yml*, deux conteneurs ont été déclarés, *thelasthotel.api* et *mongo*.

Le conteneur *mongo* a deux variables d'environnement qui doivent être renseignées et les valeurs seront utilisées dans l'API.   
* MONGO_INITDB_ROOT_USERNAME: nom d'utilisateur de l'administrateur de la banque locale.
* MONGO_INITDB_ROOT_PASSWORD: mot de passe utilisateur de l'administrateur de la banque locale.   
  
Dans les paramètres de *thelasthotel.api*, il y a deux points principaux:   

* build-> dockerfile: il mappe le fichier Dockerfile dans le dossier TheLastHotel.API. Dockerfile est le fichier responsable de l'empaquetage des projets et de leur transformation en une image docker.
* environment-> DatabaseName: variable avec le nom de la base de données qui sera créée dans MongoDB/CosmosDB.
* environment-> ConnectionString: Connection string avec le chemin vers la base de données.


