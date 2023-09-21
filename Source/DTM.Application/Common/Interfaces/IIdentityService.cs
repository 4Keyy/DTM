using DTM.Application.Common.Models;

namespace DTM.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        public Task<string?> GetUserNameAsync(string userId);

        public Task<bool> IsInRoleAsync(string userId, string role);

        public Task<bool> AuthorizeAsync(string userId, string policyName);

        public Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);

        public Task<Result> DeleteUserAsync(string userId);
    }
}
