using System.ComponentModel.DataAnnotations;
using Froggie.Api.Users.Models;

namespace Froggie.Api.Users.Responses;

public sealed class LogInResponse
{
    [Required]
    public bool Succeeded { get; init; }

    [MemberNotNullWhen(true, nameof(Succeeded))]
    public string? AccessToken { get; init; }

    [MemberNotNullWhen(false, nameof(Succeeded))]
    public string[]? Errors { get; init; }

    [MemberNotNullWhen(true, nameof(Succeeded))]
    public UserDto? User { get; init; }
}