# Scalability

L'une des prémisses du projet est qu'il était évolutif et c'est pourquoi certaines décisions ont été prises pour rendre cela possible.

## L'utilisation du conteneur
Possibilité de conteneur pour empaqueter l'application et toutes ses dépendances et l'exécuter, essentiellement, sur n'importe quel serveur Linux ou Windows. <br>

Le conteneur *TheLastHotel.API* peut être exécuté dans un cluster Docker Swarm ou dans un cluster Kubernetes local, si l'utilisation du cloud pose un problème, par exemple. <br>

## Nuage
L'exécution de ce projet dans un cloud est recommandée car il existe plusieurs services qui facilitent l'évolutivité et fournissent la sécurité nécessaire à cet effet.

> Utilisé dans le projet:<br>
> Le service Web App (conteneur) dans Azure a été choisi pour le scénario d'évolutivité. <br>
> Il fournit plusieurs couches de prix et différentes configurations pour une évolutivité verticale et horizontale, en plus d'une surveillance étendue, la possibilité de configurer facilement des alertes et SSL.

<br>
<img src="../Images/../Doc/Images/webapi_1.png" alt="Web App Configuration" width="650" height="350"/><br />
Configuration de l'application Web pour le conteneur
<br>
<br>
<img src="../Images/../Doc/Images/webapi_2.png" alt="Web App Configuration" width="1000" height="500"/><br />
Possibilité de modifier la couche de prix en fonction des fonctionnalités souhaitées
<br>
<br>
<img src="../Images/../Doc/Images/webapi_scale.png" alt="Web App Configuration" width="1000" height="450"/><br />

Il y a la possibilité de mettre à l'échelle manuellement et automatiquement. <br>
**Manuellement**: l'utilisateur ajuste le nombre d'instances qu'il souhaite.<br>
**Automatiquement**: l'utilisateur crée une règle de surveillance, telle que la limite CPU requise en % qu'il souhaite démarrer la balance. 
<br>
<br>

## **Suggestions d'amélioration pour le projet.**
Utilisation du cache sur les points de terminaison de requête. <Br>
Cela réduira les déplacements vers la base de données et améliorera les performances.<br>
Une possibilité est d'utiliser Redis, car il est léger, rapide et facile à entretenir. 