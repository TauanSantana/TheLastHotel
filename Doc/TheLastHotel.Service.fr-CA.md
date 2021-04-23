# TheLastHotel.Service
Contient toutes les classes responsables des opérations commerciales et des validations.
Un modèle appelé "CQRS" a été utilisé pour séparer **query** et **command**. [Reference about the CQRS pattern](https://martinfowler.com/bliki/CQRS.html).
* Query: Tous les types de requêtes, que ce soit dans la base de données ou dans un autre type de référentiel
* Command: Ce sont des commandes qui changent d'état ou génèrent une action, telles que *insérer, mettre à jour, supprimer, envoyer un e-mail, générer des fichiers, etc.*.   

Il existe une division des dossiers par contexte commercial:  
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
    
    
Un autre modèle adopté est celui des notifications [Reference about the Notification pattern](https://www.martinfowler.com/eaaDev/Notification.html).    
Lorsqu'une règle métier est violée, une notification est ajoutée à la commande avec la description de la règle.   
*Ces messages sont utilisés pour revenir à l'API.*