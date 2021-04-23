# Scalability

One of the premises of the project is that it was scalable and that is why some decisions were made to make this possible.

## The use of container
Container possibility to package the application and all its dependencies and run it basically on any Linux or Windows server. <br>

The *TheLastHotel.API* container can be run in a Docker Swarm cluster or in a local Kubernetes cluster, if using the cloud is a problem, for example. <br>

## Cloud
Running this project in a cloud is recommended as there are several services that facilitate scalability and provide the necessary security for this.

> Used in the project:<br>
> The Web App (Container) service in Azure was chosen for the scalability scenario. <br>
> It provides multiple price layers and different configurations for vertical and horizontal scalability, in addition to extensive monitoring, the ability to easily configure alerts and SSL.

<br>
<img src="../Images/../Doc/Images/webapi_1.png" alt="Web App Configuration" width="650" height="350"/><br />
Web App Configuration for Container
<br>
<br>
<img src="../Images/../Doc/Images/webapi_2.png" alt="Web App Configuration" width="1000" height="500"/><br />
Possibility to change the price layer according to the desired features
<br>
<br>
<img src="../Images/../Doc/Images/webapi_scale.png" alt="Web App Configuration" width="1000" height="450"/><br />

There is the possibility to scale manually and automatically. <br>
**Manually**: the user adjusts the number of instances he wants.<br>
**Automatically**: the user creates a rule for monitoring, such as the required CPU limit in % that he wants to start the scale. 
<br>
<br>

## **Improvement suggestions for the project.**
Use of cache on query end-points. <Br>
This will decrease trips to the database and bring more performance.<br>
One possibility is to use Redis, as it is light, fast and easy to maintain. 