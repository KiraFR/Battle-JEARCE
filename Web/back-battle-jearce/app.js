//import
const Express = require("express");
const BodyParser = require("body-parser");
const app = Express();
var mongoose = require("mongoose");

const uri = "mongodb+srv://AdminDB:AdminDB@cluster0-0nx9f.mongodb.net/Jearce?retryWrites=true"

//parametrage

app.use(BodyParser.json());
app.use(BodyParser.urlencoded({ extended: true }));

//connection
mongoose.connect(uri, { useNewUrlParser: true });

//models

var userModel = mongoose.model('user', {
    pseudo: String,
    password: String
}, "User");

//routage

app.post("/AddUser", async (request, response) => {
    try {
        var user = new userModel(request.headers);//headers pour POST surement params pour get(pas encore testé)
        var result = await user.save();
        response.send(result);
    } catch (error) {
        response.status(500).send(error);
    }
});

// PAS ENCORE MIS A JOUR

/*app.get("/GetUser", (request, response) => {
  collection.find({}).toArray((error, result) => {
    if (error) {
      return response.status(500).send(error);
    }
    response.send(result);
  });
});*/

/*app.get("/person/:id", (request, response) => {
  collection.findOne({ "_id": new ObjectId(request.params.id) }, (error, result) => {
    if (error) {
      return response.status(500).send(error);
    }
    response.send(result);
  });
});*/


app.listen(5000, () => {
    console.log("Listenig at :5000")
});

