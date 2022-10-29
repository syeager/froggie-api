using System.ComponentModel.DataAnnotations;

namespace Froggie.Api.Users;

[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
public sealed class CreateUserRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; init; } = null!;

    [Required]
    public string Name { get; init; } = null!;

    [Required]
    public string Password { get; init; } = null!;
}