#load "./CommandBase.csx"

using System.Collections.Generic;

public class CreateReferendum : CommandBase {
	
	public string ReferendumId { get; set; }
	public string Name { get; set; }
	public string Proposal { get; set; }
	public IList<string> Options { get; set; }
}