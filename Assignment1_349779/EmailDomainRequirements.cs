using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Assignment1_349779
{
    public class EmailDomainRequirements : IAuthorizationRequirement
    {
        public string EmailDomain { get; }
        public EmailDomainRequirements(string emailDomain)
        {
            EmailDomain = emailDomain;
        }

        public class EmailDomainHandler : AuthorizationHandler<EmailDomainRequirements>
        {
            protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EmailDomainRequirements requirement)
            {
                if (!context.User.HasClaim(c => c.Type == ClaimTypes.Email))
                {
                    return Task.CompletedTask;
                }

                var emailAddress = context.User.FindFirst(c => c.Type == ClaimTypes.Email).Value;
                if (emailAddress.EndsWith(requirement.EmailDomain))
                {
                    context.Succeed(requirement);
                }

                return Task.CompletedTask;
            }
        }
    }
}
