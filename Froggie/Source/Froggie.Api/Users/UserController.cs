using Microsoft.AspNetCore.Authorization;

namespace Froggie.Api.Users;

[AllowAnonymous]
[Route("users", Name = "Users")]
[OpenApiTag("Users")]
public abstract class UserController : Controller;