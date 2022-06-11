using Microsoft.AspNetCore.Identity;

namespace API.Projections
{
    public class UserProjection
    {
   
            public UserProjection(IdentityUser user)
            {
                UserName = user.UserName;
                Email = user.Email;
                Id = user.Id;
            }

            public string Email { get; set; }
            public string UserName { get; set; }
            public string Id { get; set; }
    }
    
}
