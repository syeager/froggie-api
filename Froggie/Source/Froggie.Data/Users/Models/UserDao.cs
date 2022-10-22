using Froggie.Domain.Users.Models;
using Microsoft.AspNetCore.Identity;
using User = Froggie.Domain.Users.Models.User;

namespace Froggie.Data.Users.Models;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
internal class UserDao : IdentityUser<Guid>
{
    public User ToUser()
    {
        var user = User.Create(Id, new Email(Email), new Name(UserName));
        return user;
    }
}