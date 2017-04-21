#load "./CommandBase.csx"

public class CastVote : CommandBase {
	public string ReferendumId { get; set; }
	public string VoterId { get; set; }
	public string Vote { get; set; }
}