using System.ComponentModel.DataAnnotations;
using Froggie.Domain.Tasks;
using LittleByte.Common.AspNet.Attributes;

namespace Froggie.Api.Tasks;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public sealed class TaskDto : Dto
{
    [Required]
    [StringRange(TitleRules.LengthMin, TitleRules.LengthMax)]
    public string Title { get; init; } = null!;

    [Required]
    public Guid GroupId { get; init; }
}