using Refit;
using ReqResRefitDemo.Models;

namespace ReqResRefitDemo.ApiClient;

public interface IAuthApi
{
	[Post("/api/login")]
	Task<LoginResponse> Login([Body] LoginRequest request);
}
