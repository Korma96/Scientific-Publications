using Microsoft.AspNetCore.Mvc.Filters;
using ScientificPublications.Common.Enums;
using ScientificPublications.Common.Exceptions;
using System;
using System.Threading.Tasks;

namespace ScientificPublications.Application
{
    public class AuthorizationFilterAttribute : ActionFilterAttribute
    {
        private readonly Role _requiredRole;

        public AuthorizationFilterAttribute(Role role)
        {
            _requiredRole = role;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var sessionInfo = (context.Controller as AbstractController).GetSession();
            if (sessionInfo == null)
                throw new UnauthorizedException("Not logged in");

            if (!Enum.TryParse(sessionInfo.Role, out Role roleFromSession))
                throw new UnauthorizedException("Invalid role");

            if (roleFromSession < _requiredRole)
                throw new UnauthorizedException("Action is not allowed for current role");

            await next();
        }
    }
}
