﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace Froggie.Api.Users;

[AllowAnonymous]
[Route("users", Name = "Users")]
[OpenApiTag("Users")]
[ApiController]
public abstract class UserController : Controller { }