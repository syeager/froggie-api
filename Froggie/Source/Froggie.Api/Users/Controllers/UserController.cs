using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using Controller = LittleByte.Common.AspNet.Core.Controller;

namespace Froggie.Api.Users.Controllers;

[AllowAnonymous]
[Route("users", Name = "Users")]
[OpenApiTag("Users")]
[ApiController]
public abstract class UserController : Controller { }