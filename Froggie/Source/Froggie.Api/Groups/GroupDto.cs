using System.ComponentModel.DataAnnotations;
using Froggie.Domain.Groups;
using LittleByte.Common.AspNet.Attributes;

namespace Froggie.Api.Groups;

public sealed class GroupDto : Dto
{
    [Required]
    [StringRange(NameRules.LengthMin, NameRules.LengthMax)]
    public string Name { get; init; } = null!;
}