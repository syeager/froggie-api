using System.ComponentModel.DataAnnotations;

namespace Froggie.Api.Groups;

public sealed class CreateGroupRequest
{
    [Required]
    public string Name { get; init; } = null!;
}