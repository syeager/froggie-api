using System.ComponentModel.DataAnnotations;
using Froggie.Domain.Tasks;
using LittleByte.Common.AspNet.Attributes;

namespace Froggie.Api.Tasks;

[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
public sealed class CreateTaskRequest
{
    [Required]
    [StringRange(TitleRules.LengthMin, TitleRules.LengthMax)]
    public string Title { get; init; } = null!;
    [Required]
    public Guid CreatorId { get; init; }
    [Required]
    public DateTime DueDate { get; init; }
}