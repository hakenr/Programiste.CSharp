namespace ReqResRefitDemo.Models;

public sealed class UserMutationResponse
{
	public string Name { get; set; }
	public string Job { get; set; }

	public string Id { get; set; }
	public DateTime? CreatedAt { get; set; }
	public DateTime? UpdatedAt { get; set; }
}
