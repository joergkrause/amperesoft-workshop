// Mongo DB setup
docker run -d --name Workshop -p 27017:27017 -e MONGO_INITDB_ROOT_USERNAME=db_user -e MONGO_INITDB_ROOT_PASSWORD=p@ssw0rd mongo
docker exec -it Workshop bash
mongosh --username db_user --password p@ssw0rd
show dbs  // show databases

// EF Core setup
mongosh
use Workshop
Install-Package MongoDB.Driver
