# TheLastHotel.Service
Contains all classes responsible for business operations and validations.
A pattern called "CQRS" was used that separates **query** and **command**. [Reference about the CQRS pattern](https://martinfowler.com/bliki/CQRS.html).
* Query: Are all types of queries, whether in database or other type of repository
* Command: They are commands that change state or generate an action, such as *insert, update, delete, sending email, generating files, etc*.   

There is a division of folders by business context:  
-Booking  
--*Command*  
----**AddBookingCommand**  
--*Query*  
----**CheckIfRoomIsAvailabilityQuery**  
  
-Room  
--*Command*  
----**AddRoomCommand**  
--*Query*  
----**FindRoomByIdQuery**   
    
    
Another pattern adopted is that of notifications [Reference about the Notification pattern](https://www.martinfowler.com/eaaDev/Notification.html)..    
When a business rule is breached, a notification is added to the command with the description of the rule.   
*These messages are used to return to the api.*

  

