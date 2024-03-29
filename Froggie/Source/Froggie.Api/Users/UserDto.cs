﻿using System.ComponentModel.DataAnnotations;

namespace Froggie.Api.Users;

public class UserDto : Dto
{
    [Required]
    public string Name { get; init; } = null!;
}