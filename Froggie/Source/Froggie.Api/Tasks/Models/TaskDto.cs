using System.ComponentModel.DataAnnotations;
using Froggie.Domain.Tasks.Validators;
using LittleByte.Extensions.AspNet.Core;

namespace Froggie.Api.Tasks.Models;

public sealed class TaskDto : Dto
{
    [Required]
    [StringLength(TitleRules.LengthMax, MinimumLength = TitleRules.LengthMin)]
    public string Title { get; set; } = null!;
}