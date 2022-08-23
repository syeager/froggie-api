using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Froggie.Domain.Tasks.Validators;
using LittleByte.Extensions.AspNet.Attributes;

namespace Froggie.Api.Tasks.Requests;

[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
public sealed class CreateTaskRequest
{
    [Required]
    [StringRange(TitleRules.LengthMin, TitleRules.LengthMax)]
    public string Title { get; init; } = null!;
}