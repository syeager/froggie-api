using System.ComponentModel.DataAnnotations;

namespace Froggie.Api.Accounts;

[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
public sealed class CreateAccountRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; init; } = null!;

    [Required]
    public string Name { get; init; } = null!;

    [Required]
    public string Password { get; init; } = null!;
}