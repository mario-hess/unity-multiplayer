const mongoose = require("mongoose");

const Schema = mongoose.Schema;

const characterSchema = new Schema({
  characterName: {
    type: String,
    required: true,
  },
  characterClass: {
    type: Number,
    required: true,
  },
});

module.exports = characterSchema;
