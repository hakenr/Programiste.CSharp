namespace ReqResRefitDemo.Models;

public sealed class UserListResponse
{
	public int Page { get; set; }
	public int PerPage { get; set; }
	public int Total { get; set; }
	public int TotalPages { get; set; }
	public UserDto[] Data { get; set; }
}
