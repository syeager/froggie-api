using Microsoft.AspNetCore.Identity;

namespace Froggie.Data.Users.Models;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
internal class UserDao : IdentityUser<Guid> { }