using Froggie.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace Froggie.Data.Accounts;

public class Account : IdentityUser<Guid>
{
    public Id<User> UserId { get; set; }

    public User? User { get; set; }
}