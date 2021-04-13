const express = require("express");
const { body } = require("express-validator");

const User = require("../models/user");

const characterController = require("../controllers/character");

const router = express.Router();

router.put("/create", characterController.create);
router.post("/fetchCharacterData", characterController.fetchCharacterData);

module.exports = router;
