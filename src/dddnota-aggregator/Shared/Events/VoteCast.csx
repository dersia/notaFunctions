#load "../TableStorageBase.csx"

public class VoteCast : TableStorageBase {
	public string ReferendumId { get; set; }
	public string VoterId { get; set; }
	public string Vote { get; set; }
}