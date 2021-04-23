# The Last Hotel - API

"The Last Hotel" is an example project for managing bookings, rooms and hotel customers.
<br />
This project was designed to be scalable and multiplatform,
therefore, .Net Core and Docker have been used since conception.


## Development requirements:
* [Visual Studio >= 2019](https://visualstudio.microsoft.com/pt-br/downloads/)
* [.NET Core 3.1](https://dotnet.microsoft.com/download/dotnet/3.1)
* [Docker for Desktop](https://www.docker.com/products/docker-desktop)


## API Routes:

ATTENTION:
* All route documentation can be accessed through the endpoint: **/swagger**
* All routes are versioned with the prefix **"v1"**
  
| Method | URL | Description 
| --- | --- | --- |
|GET |/v1/Booking/client/{id} |List all bookings of client|
|POST |/v1/Booking |Includes a Booking|
|PATCH |/v1/Booking/{id} |Updates a specific Booking|
|POST |/v1/Client |Includes a Client|
|POST |/v1/Room |Includes a Room|
|GET |/v1​/Room​/{id}​/Availability |Checks availability by date|
|  |   |
## Architecture division:
The project is divided into 6 parts:

| Project | Description 
| --- | --- |
|**docker-compose**|Used to run the project locally (run the docker-compose.yml file that contains the API plus the database for testing)|
|**TheLastHotel.API**|API that contains all the configuration of routes and that executes the business logic|
|**TheLastHotel.Domain**|Contains all classes that represent the business, such as: 'Booking', 'Room' and 'Client'|
|**[TheLastHotel.Repository](./TheLastHotel.Repository.md)**|Contains the generic repository for accessing data and the respective repositories for working with domain classes.|
|**TheLastHotel.Service**|Contains all the services to persist the data and validate the business rules. Each business context contains the Queries and Commands division implementing the business logic.|
|**TheLastHotel.Tests**|It contains the unit tests that validate the code of all the previous layers.|
|  |   |
 
 ## Architecture details:
![alt text](./Doc/images/OnionArchitecture.png "Onion Architecture")
<br />
<p>

* The **"Domain entities"** is represented by the project **"TheLastHotel.Domain"**. Contains all the domain classes used in the project.
  

* The **"Repository"** is represented by the **"TheLastHotel.Repository"** project.
Contains all the repository classes used by the services and commands contained in the "TheLastHotel.Service" project.
In addition, contains a generic repository that can be used by MongoDB (>= 3.5) and Azure CosmosDB using MongoDB API *(where can scale more easily)* .
Inside "TheLastHotel.Repository" contains the Database folder. Contains the generic repository and additional settings for queries to be made with Linq and not text *(avoiding problems with misspellings)*.
* The **"Business Logic"** is represented by the **"TheLastHotel.Service"**. Contains all operations, logic and business validations
* The **"API Service"** is represented by the **"TheLastHotel.API"**. This is the user/system interaction layer. Contains the exposed endpoints for access and all API route logic.
It contains the dependency injection configuration and settings for the database that will be used in the previous layers.

* **(Extra - For development) Docker-compose**. It is an extra project in the solution to execute the project using Docker. It makes use of the "docker-compose.yml" file to run the API and MongoDB locally for development.
</p>
