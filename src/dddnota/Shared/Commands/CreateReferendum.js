var CommandBase = require("./CommandBase.js");

module.exports = class CreateReferendum extends CommandBase {
  constructor(referendumId, name, proposal, options) {
    super();
    this.referendumId = referendumId; 
    this.name = name; // mandatory
    this.proposal = proposal; // mandatory
    this.options = options; // mandatory
  }
};