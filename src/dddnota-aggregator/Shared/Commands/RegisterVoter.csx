#load "./CommandBase.csx"
#load "../Domain/PostalAddress.csx"

public class RegisterVoter : CommandBase {
	
	public string VoterId { get; set; }
	public string Firstname { get; set; }
	public string Lastname { get; set; }
	public PostalAddress Address { get; set; }
}