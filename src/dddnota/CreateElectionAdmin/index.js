var CreateElectionAdmin = require("../Shared/Commands/CreateElectionAdmin.js");
var errors = require("../Shared/Domain/Errors.js");
var isValidPostalCode = require("../Shared/Domain/PostalCode.Utils.js");

module.exports = function (context, req) {
    var validationErrors = [];
    if(!req.body.electionAdminId) {
      validationErrors.push({"field": "electionAdminId", "msg": "ElectionAdmin id is a required field."});
    }
    if(!req.body.firstname) {
      validationErrors.push({"field": "firstname", "msg": "ElectionAdmin firstname is a required field."});
    }   
    if(!req.body.lastname) {
      validationErrors.push({"field": "lastname", "msg": "ElectionAdmin lastname is a required field."});
    }
    if(!isValidPostalCode(req.body.address.postalCode, req.body.address.addressCountry)) {
      validationErrors.push({"field": "postalCode", "msg": "Zip / Postal Code is invalid overall."});
    }

    if(req.body.address && !req.body.address.postalCode) {
      validationErrors.push({"field": "postalCode", "msg": "Zip / Postal Code is a required field."});
    }

    if(req.body.address && !req.body.address.addressRegion) {
      validationErrors.push({"field": "addressRegion", "msg": "Address Region is a required field."});
    }    
    if(validationErrors.length > 0) {
      context.log(validationErrors);
      throw new errors.ValidationFailed(validationErrors);
    } 
        
    context.bindings.outputEvents = new CreateElectionAdmin(req.body.electionAdminId, req.body.firstname, req.body.lastname, req.body.address);
    context.done();
};