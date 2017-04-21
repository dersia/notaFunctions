var CommandBase = require("./CommandBase.js");

module.exports = class RegisterVoter extends CommandBase {
  constructor(voterId, firstname, lastname, address) {
    super();
    this.voterId = voterId; // mandatory
    this.firstname = firstname; //mandatory
    this.lastname = lastname; //mandatory
    this.address = address; // mandatory. MUST contain zip code.
  }
};