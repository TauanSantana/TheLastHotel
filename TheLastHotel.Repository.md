# TheLastHotel.Repository

Description and relationship of files:
 File | Description 
| --- | --- |
|Interfaces\IMongoContext| Interface implemented by [IMongoContext] (#IMongoContext) and contains the signature of the *GetCollection* method that will be used by [BaseRepository](#BaseRepository) |
|Interfaces\IMongoDbSettings|Interface that exposes the signature of the DatabaseName and ConnectionString properties. Implemented by [MongoDbSettings](#MongoDbSettings)|
|Interfaces\IRepository|Interface used to expose all the methods implemented in [BaseRepository](#BaseRepository)|
|Database\BaseRepository| Contains all methods of accessing the database *(CRUD)*. Implements [IRepository](#IRepository) and receives as a parameter the class that represents the collection.|
|Database\EnumMongoErrorCode| Enum with error codes referring to the connection. Used for exception handling in [BaseRepository](#BaseRepository)|
|Database\MongoContext| Class responsible for accessing MongoDB and containing the *GetCollection* method that will be used by [BaseRepository] (#BaseRepository) to access the collections |
|Database\MongoDbPersistence| Class responsible for creating an instance of [RepositoryMapBase] (#RepositoryMapBase). It is executed in the Startup class in the API|
|Database\MongoDbSettings| Class that implements IMongoDbSettings and contains the properties *DatabaseName* and *ConnectionString* for connection to the database. It is used by [MongoContext](#MongoContext)|
|Database\Paged| Base class used by [BaseRepository](#BaseRepository) to bring paginated data |
|Database\ParameterRebinder| Used by the class [Utility](#Utility) | for query composition Linq
|Database\RepositoryMapBase| Abstract class with signature of the *'Configure'* method to be implemented by the class [ConfigurationDbMap](#ConfigurationDbMap) and called on Startup|
|Database\Utility| Class with auxiliary methods for composing Linq queries for MongoDB|
|BookingRepository| Implements [IRepository](#IRepository) and [BaseRepository](#BaseRepository) by passing the domain class "Booking" as a parameter to identify the corresponding Collection in the database.|
|ClientRepository| Implements [IRepository](#IRepository) and [BaseRepository](#BaseRepository) by passing the domain class "Client" as a parameter to identify the corresponding Collection in the database.|
|RoomRepository| Implements [IRepository](#IRepository) and [BaseRepository](#BaseRepository) by passing the domain class "Room" as a parameter to identify the corresponding Collection in the database.|
|ConfigurationDbMap| Mapping of domain classes to the Database |
