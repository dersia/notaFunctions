#load "./CommandBase.csx"
#load "../Domain/PostalAddress.csx"

public class CreateElectionAdmin : CommandBase {
	public string ElectionAdminId { get; set; }
	public string Firstname { get; set; }
	public string Lastname { get; set; }
	public PostalAddress Address { get; set; }
}