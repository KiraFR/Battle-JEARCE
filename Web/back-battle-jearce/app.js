//import
const Express = require("express");
var session = require('express-session');
const BodyParser = require("body-parser");
const app = Express();
var ObjectID = require('mongodb').ObjectID;
var mongoose = require("mongoose");

const uri = "mongodb+srv://AdminDB:AdminDB@cluster0-0nx9f.mongodb.net/Jearce?retryWrites=true"

//parametrage

function tri(a, b) {
    return b.score - a.score;
}
var initperso = [{ "personnage": "5cb59bbe1c9d4400009738d6", "nombre": 1 },
    { "personnage": "5cb59bdc1c9d4400009738d7", "nombre": 1 },
    { "personnage": "5cb59bef1c9d4400009738d8", "nombre": 1 },
    { "personnage": "5cb59bfd1c9d4400009738d9", "nombre": 1 },
    { "personnage": "5cb59c0c1c9d4400009738da", "nombre": 1 },]
var initFormation = [{
    p1: "5cb59bdc1c9d4400009738d7",
    p2: "5cb59bdc1c9d4400009738d7",
    p3: "5cb59bdc1c9d4400009738d7",
    p4: "5cb59bdc1c9d4400009738d7"
}]
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
    pseudo: String,
    password: String,
    email: String,
    objet: [String],
    personnage: [{ "personnage": String, "nombre": Number }] ,
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
    res.header("Access-Control-Allow-Methods", "GET, PUT, POST, DELETE");
    next();
});


//routage
app.post("/AddUser", async (request, response) => {
    try {
        sess = request.session;
        var crypto = require('crypto');
        var text = request.body.password;
        var algorithm = 'aes256';
        var cle = "fUjXn2r5u7x!A%D*";
        var cipher = crypto.createCipher(algorithm, cle);
        var crypted = cipher.update(text, 'utf8', 'hex');
        crypted += cipher.final('hex');
        var user = new userModel(request.body);
        user.score = 0;
        user.argent = 0;
        user.password = crypted;
        user.personnage = initperso;
        user.formation = initFormation;
        var result = await user.save();
        sess = result;
        sess.password = "";
        response.send(result);
    } catch (error) {
        response.status(500).send(error);
    }
});

app.post("/AddUserItem", async (request, response) => {
    try {
        sess.objet.push(request.body._id);
        var update = new userModel(sess);
        var result = await update.save();
        sess = null;
        sess = result;
        response.send(result);
    } catch (error) {
        response.status(500).send(error);
    }
});

app.post("/AddUserCharacter", async (request, response) => {
    try {
        for (var i in sess.personnage) {
            for (var j in request.body) {
                if (sess.personnage[i].personnage == request.body[j].personnage) {
                    sess.personnage[i].nombre += request.body[j].nombre;
                }
            }
        }
        var update = new userModel(sess);
        var result = await update.save();
        sess = null;
        sess = result;
        response.send(result);
    } catch (error) {
        response.status(500).send(error);
    }
});

app.post("/UpdatePassword", async (request, response) => {
    try {
        var crypto = require('crypto');
        var text = request.body.password;
        var algorithm = 'aes256';
        var cle = "fUjXn2r5u7x!A%D*";
        var cipher = crypto.createCipher(algorithm, cle);
        var crypted = cipher.update(text, 'utf8', 'hex');
        crypted += cipher.final('hex');
        var update = sess;
        update.password = crypted;
        userModel.findOneAndUpdate({ _id: sess._id }, update, { upsert: true }, (err, doc) => {
            if (err) console.log(err);
            else {
                console.log("yes");
            }
        });
        sess = null;
        sess = update;
        sess.password = "";
        response.send(sess);
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
        var password = await userModel.findById(sess._id).exec();
        sess.formation.push(request.body);
        sess.password = password.password;
        var update = sess;
        userModel.findOneAndUpdate ({ _id: sess._id }, update, { upsert: true }, (err, doc) => {
            if (err) console.log(err);
            else {
                console.log("yes");
            }
        });

        sess = null;
        sess = update;
        response.send(sess);
    } catch (error) {
        response.status(500).send(error);
    }
});

app.get("/GetClassement", async (request, response) => {
    try {
       var result = await userModel.find().exec();
       result.sort(tri);
       response.send(result);
 
    }catch (error) {
        response.statut(500).send(error);
    }
});

app.get("/GetUser", async (request, response) => {
    try {
        var result = await userModel.find().exec();
        for (var i in result) {
            result[i].password = "";
        }
        response.send(result);

} catch (error) {
    response.statut(500).send(error);
}
});

app.get("/GetCharacter", async (request, response) => {
    try {
        var result = await characterModel.find().exec();
        response.send(result);

    } catch (error) {
        response.statut(500).send(error);
    }
});

app.get("/GetItem", async (request, response) => {
    try {
        var result = await itemModel.find().exec();
        response.send(result);

    } catch (error) {
        response.statut(500).send(error);
    }
});

app.get("/GetSessionOuverte", async (request, response) => {
    try {
        if (sess == null) {
            reponse.send(false);
        } else {
            response.send(true);
        }
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

app.post("/DeleteSession", async (request, response) => {
    try {
        sess = null;
        var result = "session supprimer";
        response.send(result);

    } catch (error) {
        response.statut(500).send(error);
    }
});

app.get("/GetFormation/:id", async (request, response) => {
    try {
        console.log('pass');
        var id = request.params.id;
        var result = await userModel.findById(id);
        console.log(result);
        response.send(result.formation);
    } catch (error) {
        response.status(500).send(error);
    }
});

app.get("/GetUser/:id", async (request, response) => {
    try {
        sess = request.session;
        var info = request.params.id;
        var pseudoMDP = info.split(',');
        var crypto = require('crypto');
        var text = pseudoMDP[1];
        var algorithm = 'aes256';
        var cle = "fUjXn2r5u7x!A%D*";
        var cipher = crypto.createCipher(algorithm, cle);
        var crypted = cipher.update(text, 'utf8', 'hex');
        crypted += cipher.final('hex');
        pseudoMDP[1] = crypted;
        var result = await userModel.find().exec();
        var string = JSON.stringify(result);
        var obj = JSON.parse(string);
        for(var i in obj){
        if(obj[i].email == pseudoMDP[0] && obj[i].password == pseudoMDP[1]){
            var resultUser = obj[i];
        }
        }
        
        sess = resultUser;
        sess.password = "";
        response.send(sess);

} catch (error) {
    response.statut(500).send(error);
}
});


app.listen(5000, () => {
    console.log("Listenig at :5000")
});

