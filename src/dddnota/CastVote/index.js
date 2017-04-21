var CastVote = require("../Shared/Commands/CastVote.js"); 
var errors = require("../Shared/Domain/Errors.js");


module.exports = function (context, req) {
    var validationErrors = [];
    if(!req.params.referendumId) {
      validationErrors.push({"field": "referendumId", "msg": "Referendum id is a required field."});
    }
    if(!req.params.voterId) {
      validationErrors.push({"field": "voterId", "msg": "Voter id is a required field."});
    }
    if(!req.body) {
      validationErrors.push({"field": "vote", "msg": "Vote is a required field."});
    }
    /*if(context.bindings.events && context.bindings.events.length == 1){
      validationErrors.push({"field": "voterId", "msg": "Already voted on this referendum."});
    }*/
    if(validationErrors.length > 0) {
      throw new errors.ValidationFailed(validationErrors);
    }

    context.bindings.outputEvents = new CastVote(req.params.referendumId, req.params.voterId, req.body);
    context.done();
};