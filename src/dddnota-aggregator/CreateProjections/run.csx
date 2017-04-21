#load "../Shared/TableStorageBase.csx"
#load "../Shared/Events/ElectionAdminCreated.csx"
#load "../Shared/Events/ReferendumCreated.csx"
#load "../Shared/Events/VoteCast.csx"
#load "../Shared/Events/VoterRegistered.csx"
#load "../Shared/Domain/PostalAddress.csx"
#load "../Shared/Projections/ElectionAdminProjection.csx"
#load "../Shared/Projections/ReferendumProjection.csx"
#load "../Shared/Projections/VoteProjection.csx"
#load "../Shared/Projections/VoterProjection.csx"
#load "../Shared/Helper/NotifyQueueItem.csx"
#load "../Shared/Helper/EventCreatedQueueItem.csx"
#r "Newtonsoft.Json"
using System;
using Newtonsoft.Json.Linq;

public static async Task Run(EventCreatedQueueItem myQueueItem, IQueryable<TableStorageBase> events, IAsyncCollector<JObject> outputProjections, IAsyncCollector<NotifyQueueItem> outputNotify,TraceWriter log)
{
    var notifyer = new NotifyQueueItem();
    switch(myQueueItem.EventName) {
        case "CastVote":
        await ProjectionCreator.CreateVoteProjection(events, outputProjections, notifyer);
        break;
        case "CreateElectionAdmin":
        await ProjectionCreator.CreateElectionAdminProjection(events, outputProjections, notifyer);
        break;
        case "CreateReferendum":
        await ProjectionCreator.CreateReferendumProjection(events, outputProjections, notifyer);
        break;
        case "RegisterVoter":
        await ProjectionCreator.CreateVoterProjection(events, outputProjections, notifyer);
        break;
    }
    await outputNotify.AddAsync(notifyer);
}

public static class ProjectionCreator {

    public static async Task CreateElectionAdminProjection(IQueryable<TableStorageBase> events, IAsyncCollector<JObject> outputProjections, NotifyQueueItem outputNotify) {
        var latestEvent = events.Where(e => e.PartitionKey == "ElectionAdminCreated").ToList()?.OrderBy(e => e.Version).LastOrDefault() as ElectionAdminCreated;
        if(latestEvent == null)
            return;

        await outputProjections.AddAsync(JObject.FromObject(new ElectionAdminProjection { ElectionAdminId = latestEvent.ElectionAdminId, Firstname = latestEvent.Firstname, Lastname = latestEvent.Lastname, Address = latestEvent.Address }));
        outputNotify.UpdatedProjections.Add("ElectionAdminProjection");
    }

    public static async Task CreateReferendumProjection(IQueryable<TableStorageBase> events, IAsyncCollector<JObject> outputProjections, NotifyQueueItem outputNotify) {
        var latestEvent = events.Where(e => e.PartitionKey == "ReferendumCreated").ToList()?.OrderBy(e => e.Version).LastOrDefault() as ReferendumCreated;
        if(latestEvent == null)
            return;

        await outputProjections.AddAsync(JObject.FromObject(new ReferendumProjection { Name = latestEvent.Name, ReferendumId = latestEvent.ReferendumId, Proposal = latestEvent.Proposal, Options = latestEvent.Options }));
        outputNotify.UpdatedProjections.Add("ReferendumProjection");
    }

    public static async Task CreateVoteProjection(IQueryable<TableStorageBase> events, IAsyncCollector<JObject> outputProjections, NotifyQueueItem outputNotify) {
        var latestEvent = events.Where(e => e.PartitionKey == "VoteCast").ToList()?.OrderBy(e => e.Version).LastOrDefault() as VoteCast;
        if(latestEvent == null)
            return;

        await outputProjections.AddAsync(JObject.FromObject(new VoteProjection { ReferendumId = latestEvent.ReferendumId, VoterId = latestEvent.VoterId, Vote = latestEvent.Vote }));
        outputNotify.UpdatedProjections.Add("VoteProjection");
    }

    public static async Task CreateVoterProjection(IQueryable<TableStorageBase> events, IAsyncCollector<JObject> outputProjections, NotifyQueueItem outputNotify) {
        var latestEvent = events.Where(e => e.PartitionKey == "VoterRegistered").ToList()?.OrderBy(e => e.Version).LastOrDefault() as VoterRegistered;
        if(latestEvent == null)
            return;

        await outputProjections.AddAsync(JObject.FromObject(new VoterProjection { VoterId = latestEvent.VoterId, Firstname = latestEvent.Firstname, Lastname = latestEvent.Lastname, Address = latestEvent.Address }));
        outputNotify.UpdatedProjections.Add("VoterProjection");
    }
}