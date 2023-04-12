using System.ComponentModel.DataAnnotations;

namespace Froggie.Api.Groups;

public sealed class CreateGroupRequest
{
    [Required]
    public Guid CreatorId { get; set; }

    [Required]
    public string Name { get; init; } = null!;
}