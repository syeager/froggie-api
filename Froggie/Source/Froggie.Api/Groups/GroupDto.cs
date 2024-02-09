using System.ComponentModel.DataAnnotations;
using Froggie.Domain.Groups;

namespace Froggie.Api.Groups;

public sealed class GroupDto : Dto
{
    [Required]
    [StringRange(GroupNameRules.LengthMin, GroupNameRules.LengthMax)]
    public string Name { get; init; } = null!;
}