using API.DataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace API.Authorization
{
    public class OrderCreatorAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Order>
    {

        UserManager<IdentityUser> _userManager;
        public OrderCreatorAuthorizationHandler(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Order order)
        {
            if (context.User == null || order == null)
                return Task.CompletedTask;

            if (requirement.Name != Constants.UpdateOperationName && requirement.Name != Constants.DeleteOperationName)
            {
                return Task.CompletedTask;
            }

            if (order.CreatedByUserName == _userManager.GetUserName(context.User))
                context.Succeed(requirement);


            return Task.CompletedTask;
        }
    }
}
