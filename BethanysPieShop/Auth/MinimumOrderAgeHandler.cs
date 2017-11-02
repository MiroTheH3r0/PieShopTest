using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BethanysPieShop.Auth
{
    public class MinimumOrderAgeHandler: AuthorizationHandler<MinimumOrderAgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumOrderAgeRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.DateOfBirth))
            {
                Console.WriteLine("User has no claim Dateofbirth " + context.User.Claims.ToString());
                return Task.CompletedTask;
            }

            var birthDate = Convert.ToDateTime(context.User.FindFirst(c => c.Type == ClaimTypes.DateOfBirth).Value);
            var ageInYears = DateTime.Today.Year - birthDate.Year;

            if (ageInYears >= requirement._minimumOrderAge)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
