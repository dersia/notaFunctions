#load "../TableStorageBase.csx"
#load "../Domain/PostalAddress.csx"

public class ElectionAdminCreated : TableStorageBase {
	
	public string ElectionAdminId { get; set; }
	public string Firstname { get; set; }
	public string Lastname { get; set; }
	public PostalAddress Address { get; set; } 
}