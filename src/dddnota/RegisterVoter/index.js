var RegisterVoter = require("../Shared/Commands/RegisterVoter.js"); 
var errors = require("../Shared/Domain/Errors.js");

module.exports = function (context, req) {
    var validationErrors = [];
    if(!req.params.voterId) {
      validationErrors.push({"field": "voterId", "msg": "Voter id is a required field."});
    }
    if(!req.body.firstname) {
      validationErrors.push({"field": "firstname", "msg": "Voter firstname is a required field."});
    }
    if(!req.body.lastname) {
      validationErrors.push({"field": "lastname", "msg": "Voter lastname is a required field."});
    }
    if(req.body.address && !req.body.address.postalCode) {
      validationErrors.push({"field": "postalCode", "msg": "Zip / Postal Code is a required field."});
    }
    if(req.body.address && !req.body.address.addressRegion) {
      validationErrors.push({"field": "addressRegion", "msg": "Address Region is a required field."});
    }
    if(validationErrors.length > 0) {
      throw new errors.ValidationFailed(validationErrors);
    }
    context.bindings.outputEvents = new RegisterVoter(req.params.voterId, req.body.firstname, req.body.lastname, req.body.address);
    context.done();
};