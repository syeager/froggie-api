using System.ComponentModel.DataAnnotations;
using Froggie.Domain.Tasks.Validators;
using LittleByte.Extensions.AspNet.Attributes;
using LittleByte.Extensions.AspNet.Core;

namespace Froggie.Api.Tasks.Models;

public sealed class TaskDto : Dto
{
    [Required]
    [StringRange(TitleRules.LengthMin, TitleRules.LengthMax)]
    public string Title { get; set; } = null!;
}