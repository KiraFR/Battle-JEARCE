//import
const Express = require("express");
var session = require('express-session');
const BodyParser = require("body-parser");
const app = Express();
var mongoose = require("mongoose");

const uri = "mongodb+srv://AdminDB:AdminDB@cluster0-0nx9f.mongodb.net/Jearce?retryWrites=true"

//parametrage

function tri(a, b) {
    return b.score - a.score;
}
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
var objet = new mongoose.Schema({
    nom: String,
    description: String,
    boost: Number
});
var personnage = new mongoose.Schema({
    type : String
    });
var joueur = new mongoose.Schema({
    pseudo : String,
    password : String,
    email : String,
    personnage : [personnage],
    formation: [JSON],
    amie: [JSON],
    score : { type : Number, min : 0},
    argent : {type : Number, min : 0}
});
var itemModel = mongoose.model('Item', objet, "Item");
var characterModel = mongoose.model('Character', personnage, "Character");
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
        console.log(user);
        user.score = 0;
        user.argent = 0;
        var result = await user.save();
        sess = result;
        response.send(result);
    } catch (error) {
        response.status(500).send(error);
    }
});

app.post("/AddCharacter", async (request, response) => {
    try {
        var character = new characterModel(request.body);
        var result = await character.save();
        response.send(result);
    } catch (error) {
        response.status(500).send(error);
    }
});

app.post("/AddItem", async (request, response) => {
    try {
        var item = new itemModel(request.body);
        var result = await item.save();
        response.send(result);
    } catch (error) {
        response.status(500).send(error);
    }
});

app.post("/AddFormation", async (request, response) => {
    try {
        sess.formation.push(request.body);
        console.log(sess);
        var update = new userModel(sess);
        var result = await update.save();
        sess = null;
        sess = result;
        console.log(result);
        response.send(result);
    } catch (error) {
        response.status(500).send(error);
    }
});

app.get("/GetClassement", async (request, response) => {
    try {
        if (sess == null) {
            var result = await userModel.find().exec();
            result.sort(tri);
            response.send(result);
        } else {
            var result = await userModel.find().exec();
            result.sort(tri);

            for (var i in result) {
                if (result[i]._id == sess._id) {
                    var position = i;
                    var integer = parseInt(position, 10)
                    integer += 1;
                }
            }

            var resultuser = { "position": integer, "users": result };
            response.send(resultuser);
        }
        
    }

     catch (error) {
        response.statut(500).send(error);
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
            var resultUser = obj[i];
        }
        }
        
        sess = resultUser;
        response.send(sess);

} catch (error) {
    response.statut(500).send(error);
}
});


app.listen(5000, () => {
    console.log("Listenig at :5000")
});
