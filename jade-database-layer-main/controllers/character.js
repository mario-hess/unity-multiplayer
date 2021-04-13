const User = require("../models/user");

exports.create = async (req, res, next) => {
  try {
    const objectId = req.body._id;
    const characters = req.body.characters;

    

    const loadedUser = await User.findOne({ _id: objectId });

    if (!loadedUser) {
      const error = new Error("Could not find a user with this ObjectId.");
      error.statusCode = 401;
      throw error;
    }

    loadedUser.characters.push({
      characterName: characters[0].characterName,
      characterClass: characters[0].characterClass,
    });

    const updatedUser = await loadedUser.save();


    res.status(201).json(updatedUser);
      
      
  } catch (error) {
    if (!error.statusCode) {
      error.statusCode = 500;
    }
    next(error);
  }
};

exports.fetchCharacterData = async (req, res, next) => {

  try {
    const objectId = req.body._id;
    const characterIndex = req.body.characterIndex;

    const loadedUser = await User.findOne({ _id: objectId });

    if (!loadedUser) {
      const error = new Error("Could not find a user with this ObjectId.");
      error.statusCode = 401;
      throw error;
    }

    const loadedCharacter = loadedUser.characters[characterIndex];

    res.status(201).json(loadedCharacter);
    
  } catch (error) {
    if (!error.statusCode) {
      error.statusCode = 500;
    }
    next(error);
  }

};
