const mongoose = require("mongoose");

const characterSchema = require("./character");

const Schema = mongoose.Schema;

const userSchema = new Schema({
  email: {
    type: String,
    required: true,
  },
  password: {
    type: String,
    required: true,
  },
  characters: [characterSchema],
});

module.exports = mongoose.model("User", userSchema);
