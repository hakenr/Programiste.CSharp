using Refit;
using ReqResRefitDemo.Models;

namespace ReqResRefitDemo.ApiClient;

public interface IUsersApi
{
    [Get("/api/users")]
    Task<UserListResponse> GetUsers([AliasAs("page")] int page = 1, [AliasAs("per_page")] int? perPage = null);

    [Get("/api/users/{id}")]
    Task<UserResponse> GetUser(int id);

    [Post("/api/users")]
    Task<UserMutationResponse> CreateUser([Body] UserMutationRequest request);

    [Put("/api/users/{id}")]
    Task<UserMutationResponse> UpdateUser(int id, [Body] UserMutationRequest request);

    [Delete("/api/users/{id}")]
    Task DeleteUser(int id);
}
