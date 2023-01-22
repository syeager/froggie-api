using System.ComponentModel.DataAnnotations;

namespace Froggie.Api.Users;

public sealed class LogInResponse
{
    [MemberNotNullWhen(true, nameof(Succeeded))]
    public string? AccessToken { get; init; }

    [MemberNotNullWhen(false, nameof(Succeeded))]
    public string[]? Errors { get; init; }

    [Required]
    public bool Succeeded { get; init; }

    [MemberNotNullWhen(true, nameof(Succeeded))]
    public UserDto? User { get; init; }
}