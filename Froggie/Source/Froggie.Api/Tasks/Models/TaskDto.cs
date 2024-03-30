using System.ComponentModel.DataAnnotations;
using Froggie.Domain.Tasks;

namespace Froggie.Api.Tasks;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public sealed class TaskDto : Dto
{
    [Required]
    [StringRange(TitleRules.LengthMin, TitleRules.LengthMax)]
    public string Title { get; init; } = null!;

    [Required]
    public Guid GroupId { get; init; }

    [Required]
    public List<Guid> Assignees { get; init; } = null!;

    [Required]
    public bool IsCompleted { get; init; }
}