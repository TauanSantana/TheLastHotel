# TheLastHotel.Repository
Contient toutes les classes du référentiel utilisées par les services et commandes contenus dans le projet "TheLastHotel.Service".
En outre, contient un référentiel générique qui peut être utilisé par MongoDB (>=3.5) et Azure CosmosDB à l'aide de l'API MongoDB *(où peut évoluer plus facilement)*.
À l'intérieur de "TheLastHotel.Repository" contient le dossier Database. Contient le référentiel générique et des paramètres supplémentaires pour les requêtes à effectuer avec Linq et non avec du texte *(évitant les problèmes d'orthographe)*.

Description and relationship of files:
 Ficher | La description 
| --- | --- |
|Interfaces\IMongoContext| Interface implémentée par [IMongoContext] (#IMongoContext) et contient la signature de la méthode *GetCollection* qui sera utilisée par [BaseRepository](#BaseRepository) |
|Interfaces\IMongoDbSettings| Interface qui expose la signature des propriétés DatabaseName et ConnectionString. Implementé par [MongoDbSettings](#MongoDbSettings)|
|Interfaces\IRepository| Interface utilisée pour exposer toutes les méthodes implémentées dans [BaseRepository](#BaseRepository)|
|Database\BaseRepository| Contient toutes les méthodes d'accès à la base de données *(CRUD)*. Implémente [IRepository] (#IRepository) et reçoit comme paramètre la classe qui représente la collection.
|Database\EnumMongoErrorCode| Enum avec les codes d'erreur se référant à la connexion. Utilisé pour la gestion des exceptions dans [BaseRepository](#BaseRepository)|
|Database\MongoContext| Classe responsable de l'accès à MongoDB et contenant la méthode *GetCollection* qui sera utilisée par [BaseRepository](#BaseRepository) pour accéder aux collections |
|Database\MongoDbPersistence| Classe responsable de la création d'une instance de [RepositoryMapBase](#RepositoryMapBase). Il est exécuté dans la classe Startup de l'API|
|Database\MongoDbSettings| Classe qui implémente IMongoDbSettings et contient les propriétés *DatabaseName* et *ConnectionString* pour la connexion à la base de données. Il est utilisé par [MongoContext](#MongoContext)|
|Database\Paged| Classe de base utilisée par [BaseRepository](#BaseRepository) pour apporter des données paginées |
|Database\ParameterRebinder| Utilisé par la classe [Utility](#Utility) pour la composition de requête Linq | 
|Database\RepositoryMapBase| Classe abstraite avec signature de la méthode *'Configure'* à implémenter par la classe [ConfigurationDbMap](#ConfigurationDbMap) et appelée au démarrage|
|Database\Utility| Classe avec des méthodes auxiliaires pour composer des requêtes Linq pour MongoDB|
|BookingRepository| Implémente [IRepository](#IRepository) et [BaseRepository] (#BaseRepository) en passant la classe de domaine "Booking" comme paramètre pour identifier la collection correspondante dans la base de données.|
|ClientRepository| Implémente [IRepository](#IRepository) et [BaseRepository] (#BaseRepository) en passant la classe de domaine "Client" comme paramètre pour identifier la collection correspondante dans la base de données.|
|RoomRepository| Implémente [IRepository](#IRepository) et [BaseRepository] (#BaseRepository) en passant la classe de domaine "Room" comme paramètre pour identifier la collection correspondante dans la base de données.|
|ConfigurationDbMap| Mappage des classes de domaine vers la base de données |
