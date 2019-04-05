const Express = require("express");
const BodyParser = require("body-parser");
const MongoClient = require("mongodb").MongoClient;
const ObjectId = require("mongodb").ObjectID;
const DataBase_name = "exemple";
const uri = "mongodb+srv://AdminDB:AdminDB@cluster0-0nx9f.mongodb.net/test?retryWrites=true"
var app = Express();
app.use(BodyParser.json());
app.use(BodyParser.urlencoded({ extended: true }));

var database, collection;
app.listen(8080, () => {

  MongoClient.connect(uri, { useNewUrlParser: true }, function (err, client) {
    if (err) {
      console.log('Error occurred while connecting to MongoDB Atlas...\n', err);
      throw Error;
    }
    console.log('Connected...');
    database = client.db(DataBase_name);
    collection = database.collection("User");
    // perform actions on the collection object
    client.close();
  });

});

app.post("/AddUser", (request, response) => {
  collection.insert(request.body, (error, result) => {
    if (error) {
      return response.status(500).send(error);
    }
    response.send(result.result);
  });
});

app.get("/GetUser", (request, response) => {
  collection.find({}).toArray((error, result) => {
    if (error) {
      return response.status(500).send(error);
    }
    response.send(result);
  });
});

app.get("/person/:id", (request, response) => {
  collection.findOne({ "_id": new ObjectId(request.params.id) }, (error, result) => {
    if (error) {
      return response.status(500).send(error);
    }
    response.send(result);
  });
});
