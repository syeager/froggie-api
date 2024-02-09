using Microsoft.AspNetCore.Authorization;

namespace Froggie.Api.Accounts;

[AllowAnonymous]
[Route("accounts", Name = "Accounts")]
[OpenApiTag("Accounts")]
[ApiController]
public abstract class AccountController : Controller { }