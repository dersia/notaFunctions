#load "../TableStorageBase.csx"

using System.Collections.Generic;

public class ReferendumCreated : TableStorageBase {
	
	public string Name { get; set; }
	public string ReferendumId { get; set; }
	public string Proposal { get; set; }
	public IList<string> Options { get; set; }
}