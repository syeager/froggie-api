using System.ComponentModel.DataAnnotations;
using Froggie.Domain.Tasks.Validators;

namespace Froggie.Api.Tasks.Requests;

public sealed class CreateTaskRequest
{
    [Required]
    [StringLength(TitleRules.LengthMax, MinimumLength = TitleRules.LengthMin)]
    public string Title { get; init; } = null!;
}