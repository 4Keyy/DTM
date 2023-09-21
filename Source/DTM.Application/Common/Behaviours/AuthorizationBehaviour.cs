using System.Reflection;
using DTM.Application.Common.Attributes.Security;
using DTM.Application.Common.Exceptions;
using DTM.Application.Common.Interfaces;

namespace DTM.Application.Common.Behaviours
{
    public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly IUser _user;
        private readonly IIdentityService _identityService;

        public AuthorizationBehaviour(IUser user, IIdentityService identityService)
        {
            _user = user;
            _identityService = identityService;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>();

            if (authorizeAttributes.Any())
            {
                if (_user.Id == null)
                {
                    throw new UnauthorizedAccessException();
                }

                var authorizeAttributesWithRoles = authorizeAttributes.Where(attributes => !string.IsNullOrWhiteSpace(attributes.Roles));

                if (authorizeAttributesWithRoles.Any())
                {
                    var authorized = false;

                    foreach (var roles in authorizeAttributesWithRoles.Select(attributes => attributes.Roles.Split(',')))
                    {
                        foreach (var role in roles)
                        {
                            var isInRole = await _identityService.IsInRoleAsync(_user.Id, role.Trim());
                            if (isInRole)
                            {
                                authorized = true;
                                break;
                            }
                        }
                    }

                    if (!authorized)
                    {
                        throw new ForbiddenAccessException();
                    }
                }

                var authorizeAttributesWithPolicies = authorizeAttributes.Where(attributes => !string.IsNullOrWhiteSpace(attributes.Policy));
                if (authorizeAttributesWithPolicies.Any())
                {
                    foreach (var policy in authorizeAttributesWithPolicies.Select(attributes => attributes.Policy))
                    {
                        var authorized = await _identityService.AuthorizeAsync(_user.Id, policy);

                        if (!authorized)
                        {
                            throw new ForbiddenAccessException();
                        }
                    }
                }
            }

            return await next();
        }
    }
}
