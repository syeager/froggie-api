using Microsoft.AspNetCore.Identity;

namespace Froggie.Data.Users;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
internal class UserDao : IdentityUser<Guid> { }