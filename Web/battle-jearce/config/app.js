var MongoClient = require('mongodb').MongoClient;

// replace the uri string with your connection string.
var uri = "mongodb+srv://AdminDB:AdminDB@cluster0-0nx9f.mongodb.net/test?retryWrites=true"
MongoClient.connect(uri, { useNewUrlParser: true }, function (err, client) {
  if (err) {
    console.log('Error occurred while connecting to MongoDB Atlas...\n', err);
  }
  console.log('Connected...');
  const collection = client.db("Jearce").collection("User");
  // perform actions on the collection object
  client.close();
});
