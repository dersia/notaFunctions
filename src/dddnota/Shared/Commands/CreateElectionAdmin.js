var CommandBase = require("./CommandBase.js");

module.exports = class CreateElectionAdmin extends CommandBase {
  constructor(electionAdminId, firstname, lastname, address) {
    super();
    this.electionAdminId = electionAdminId; // mandatory
    this.firstname = firstname; //mandatory
    this.lastname = lastname; //mandatory
    this.address = address; // mandatory. MUST contain zip code.
  }
};