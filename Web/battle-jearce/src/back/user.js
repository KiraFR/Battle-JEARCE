var mongoose = require('mongoose')

// Define collection and schema for User item

var User = new mongoose.Schema({
  Pseudo: {
    type: String
  },

  Mail: {
    type: String
  },

  Password: {
    type: String
  },

  Classement: {
    type: Number
  }
},

  {
    collection: 'User'
  }
)

module.exports = mongoose.model('User', User)
