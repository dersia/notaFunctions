#load "../Shared/TableStorageBase.csx"
#load "../Shared/Events/ElectionAdminCreated.csx"
#load "../Shared/Events/ReferendumCreated.csx"
#load "../Shared/Events/VoteCast.csx"
#load "../Shared/Events/VoterRegistered.csx"
#load "../Shared/Domain/PostalAddress.csx"
#load "../Shared/Commands/CommandBase.csx"
#load "../Shared/Commands/CastVote.csx"
#load "../Shared/Commands/CreateElectionAdmin.csx"
#load "../Shared/Commands/CreateReferendum.csx"
#load "../Shared/Commands/RegisterVoter.csx"
#load "../Shared/Helper/EventCreatedQueueItem.csx"
#r "Newtonsoft.Json"

using Newtonsoft.Json;

public static void Run(string eventHubMessage, IQueryable<TableStorageBase> events, ICollector<TableStorageBase> outEvents, out EventCreatedQueueItem outputQueueItem, TraceWriter log) {
	log.Info(eventHubMessage);
    var Command = JsonConvert.DeserializeObject<CommandBase>(eventHubMessage);
    var latestEvent = events.Where(e => e.PartitionKey == Command.CommandName).ToList()?.OrderByDescending(e => e.Version).FirstOrDefault();
    var latestVersion = latestEvent != null ? latestEvent.Version : -1;
    switch(Command.CommandName) {
        case "CastVote": 
            var CastVoteCommand = JsonConvert.DeserializeObject<CastVote>(eventHubMessage);
            var VoteCastEvent = new VoteCast 
            { 
                PartitionKey = CastVoteCommand.CommandName,
                RowKey = Guid.NewGuid().ToString(),
                Version = latestVersion + 1,
                ReferendumId = CastVoteCommand.ReferendumId,
                VoterId = CastVoteCommand.VoterId,
                Vote = CastVoteCommand.Vote
            };
            outEvents.Add(VoteCastEvent);
            break;
        case "CreateElectionAdmin": 
            var CreateElectionAdminCommand = JsonConvert.DeserializeObject<CreateElectionAdmin>(eventHubMessage);
            var ElectionAdminCreatedEvent = new ElectionAdminCreated 
            {
                PartitionKey = CreateElectionAdminCommand.CommandName,
                RowKey = Guid.NewGuid().ToString(),
                Version = latestVersion + 1,
                ElectionAdminId = CreateElectionAdminCommand.ElectionAdminId,
                Firstname = CreateElectionAdminCommand.Firstname,
                Lastname = CreateElectionAdminCommand.Lastname,
                Address = CreateElectionAdminCommand.Address
            };
            outEvents.Add(ElectionAdminCreatedEvent);
            break;
        case "CreateReferendum": 
            var CreateReferendumCommand = JsonConvert.DeserializeObject<CreateReferendum>(eventHubMessage);
            var ReferendumCreatedEvent = new ReferendumCreated 
            {
                PartitionKey = CreateReferendumCommand.CommandName,
                RowKey = Guid.NewGuid().ToString(),
                Version = latestVersion + 1,
                ReferendumId = CreateReferendumCommand.ReferendumId,
                Name = CreateReferendumCommand.Name,
                Proposal = CreateReferendumCommand.Proposal,
                Options = CreateReferendumCommand.Options
            };
            outEvents.Add(ReferendumCreatedEvent);
            break;
        case "RegisterVoter":
            var RegisterVoterCommand = JsonConvert.DeserializeObject<RegisterVoter>(eventHubMessage);
            var VoterRegisteredEvent = new VoterRegistered 
            {
                PartitionKey = RegisterVoterCommand.CommandName,
                RowKey = Guid.NewGuid().ToString(),
                Version = latestVersion + 1,
                VoterId = RegisterVoterCommand.VoterId,
                Firstname = RegisterVoterCommand.Firstname,
                Lastname = RegisterVoterCommand.Lastname,
                Address = RegisterVoterCommand.Address
            };
            outEvents.Add(VoterRegisteredEvent);
            break;
    }
    outputQueueItem = new EventCreatedQueueItem { EventName = Command.CommandName };
    log.Info(outputQueueItem.ToString());
}

