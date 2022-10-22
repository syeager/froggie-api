using System.ComponentModel.DataAnnotations;
using Froggie.Domain.Tasks.Validators;
using LittleByte.Common.AspNet.Attributes;

namespace Froggie.Api.Tasks.Requests;

[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
public sealed class CreateTaskRequest
{
    [Required]
    [StringRange(TitleRules.LengthMin, TitleRules.LengthMax)]
    public string Title { get; init; } = null!;
}