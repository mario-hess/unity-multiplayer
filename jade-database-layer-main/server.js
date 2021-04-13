const path = require("path");
const express = require("express");
const bodyParser = require("body-parser");
const mongoose = require("mongoose");

const config = require("./config/config");

const authRoutes = require("./routes/auth");
const characterRoutes = require("./routes/character");

const server = express();

const port = 3000;
// || process.env.PORT
server.use(bodyParser.urlencoded({ extended: true }));
server.use(bodyParser.json());

server.use((req, res, next) => {
  res.setHeader("Access-Control-Allow-Origin", "*");
  res.setHeader(
    "Access-Control-Allow-Methods",
    "OPTIONS, GET, POST, PUT, PATCH, DELETE"
  );
  res.setHeader("Access-Control-Allow-Headers", "Content-Type, Authorization");
  next();
});

// Routes
server.use("/auth", authRoutes);
server.use("/character", characterRoutes);

// Error route
server.use((error, req, res, next) => {
  console.log(error);
  const status = error.statusCode || 500;
  const message = error.message;
  const data = error.data;
  res.status(status).json({ message: message, data: data });
});

// Connect to database & start server
const connect = async () => {
  try {
    await mongoose.connect(config.mongoURI, {
      useCreateIndex: true,
      useNewUrlParser: true,
      useUnifiedTopology: true,
    });
    console.log("Connected to database...");
    server.listen(port, () => {
      console.log(`Server started on port ${port}...`);
    });
  } catch (error) {
    console.log(`Connection failed with error: ${error}`);
  }
};

connect();
