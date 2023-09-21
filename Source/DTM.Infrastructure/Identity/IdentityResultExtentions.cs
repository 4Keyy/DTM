using DTM.Application.Common.Models;
using Microsoft.AspNetCore.Identity;

namespace DTM.Infrastructure.Identity
{
    public static class IdentityResultExtentions
    {
        public static Result ToApplicationResult(this IdentityResult result)
        {
            return result.Succeeded ? Result.Success() : Result.Failure(result.Errors.Select(error => error.Description));
        }
    }
}
