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
var personnage = new mongoose.Schema({
    type : String
    });
var formation = new mongoose.Schema({
    nom : String,
    personnage : [personnage]});
var joueur = new mongoose.Schema({
    pseudo : String,
    password : String,
    email : String,
    personnage : [personnage],
    formation : [formation],
    score : { type : Number, min : 0},
    argent : {type : Number, min : 0}
});
var userModel = mongoose.model('user',joueur, "User");

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

app.get("/GetUser", async (request, response) => {
  try {
        var result = await userModel.find().exec();
        response.send(result);

} catch (error) {
    response.statut(500).send(error);
}
});

app.get("/GetUser/:id", async (request, response) => {
   try {
        var info = request.params.id;
        var pseudoMDP = info.split(',');
        var result = await userModel.find().exec();
        var string = JSON.stringify(result);
        var obj = JSON.parse(string);
        for(var i in obj){
        if(obj[i].pseudo == pseudoMDP[0] && obj[i].password == pseudoMDP[1]){
        var id = obj[i]._id;
        }
        }
        var resultUser = await userModel.findById(id).exec();
        response.send(resultUser);

} catch (error) {
    response.statut(500).send(error);
}
});


app.listen(5000, () => {
    console.log("Listenig at :5000")
});

