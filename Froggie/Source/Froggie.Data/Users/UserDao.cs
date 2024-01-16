using Microsoft.AspNetCore.Identity;

namespace Froggie.Data.Users;

internal sealed class UserDao : IdentityUser<Guid>, IIdObject { }