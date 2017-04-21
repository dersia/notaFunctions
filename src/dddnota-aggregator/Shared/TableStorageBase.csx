#r "Microsoft.WindowsAzure.Storage"
using Microsoft.WindowsAzure.Storage.Table;

public class TableStorageBase : TableEntity {
	public int Version { get; set; }
}