//import
const Express = require("express");
var session = require('express-session');
const BodyParser = require("body-parser");
const app = Express();
var mongoose = require("mongoose");

const uri = "mongodb+srv://AdminDB:AdminDB@cluster0-0nx9f.mongodb.net/Jearce?retryWrites=true"

//parametrage


app.use(session({
    secret: 'omegalul',
    name: 'salut',
    proxy: true,
    resave: true,
    saveUninitialized: true
}));
app.use(BodyParser.json());
app.use(BodyParser.urlencoded({ extended: true }));

//connection
mongoose.connect(uri, { useNewUrlParser: true });

//models
var sess;
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
var userModel = mongoose.model('user', joueur, "User");

//paramettrage proxy
app.use(function (req, res, next) {
    res.header("Access-Control-Allow-Origin", "*");
    res.header("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");
    next();
});


//routage
app.post("/AddUser", async (request, response) => {
    try {
        sess = request.session;
        var user = new userModel(request.body);
        user.score = 0;
        user.argent = 0;
        var result = await user.save();
        sess = result;
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

app.get("/GetSession", async (request, response) => {
    try {
        if (sess == null) {
            var vide = "la variable session est vide";
            reponse.send(vide);
        } else {
            response.send(sess);
        }
    } catch (error) {
        response.statut(500).send(error);
    }
});

app.delete("/DeleteSession", async (request, response) => {
    try {
        sess.destroy();
        var result = "session supprimer";
        response.send(result);

    } catch (error) {
        response.statut(500).send(error);
    }
});

app.get("/GetUser/:id", async (request, response) => {
    try {
        sess = request.session;
        var info = request.params.id;
        var pseudoMDP = info.split(',');
        var result = await userModel.find().exec();
        var string = JSON.stringify(result);
        var obj = JSON.parse(string);
        for(var i in obj){
        if(obj[i].email == pseudoMDP[0] && obj[i].password == pseudoMDP[1]){
        var id = obj[i]._id;
        }
        }
        var resultUser = await userModel.findById(id).exec();
        sess = resultUser;
        response.send(resultUser);

} catch (error) {
    response.statut(500).send(error);
}
});


app.listen(5000, () => {
    console.log("Listenig at :5000")
});

