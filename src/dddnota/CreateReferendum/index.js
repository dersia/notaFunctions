var CreateReferendum = require("../Shared/Commands/CreateReferendum.js"); 
var errors = require("../Shared/Domain/Errors.js");

module.exports = function (context, req) {
    var validationErrors = [];
    if(!req.body.referendumId) {
      validationErrors.push({"field": "referendumId", "msg": "Referendum id is a required field."});
    }
    if(!req.body.proposal) {
      validationErrors.push({"field": "proposal", "msg": "Referendum proposal is a required field."});
    }   
    if(!req.body.name) {
      validationErrors.push({"field": "name", "msg": "Referendum name is a required field."});
    }   
    if(!req.body.options) {
      validationErrors.push({"field": "options", "msg": "Referendum options are required."});
    }
    if(req.body.options && req.body.options.length < 2) {
      validationErrors.push({"field": "optinos", "msg": "At least two options are required."});
    }   
    if(validationErrors.length > 0) {
      throw new errors.ValidationFailed(validationErrors);
    }  

    context.bindings.outputEvents = new CreateReferendum(req.body.referendumId, req.body.name, req.body.proposal, req.body.options);
    context.done();
};