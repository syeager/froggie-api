using System.ComponentModel.DataAnnotations;
using Froggie.Domain.Tasks.Validators;
using LittleByte.Common.AspNet.Attributes;
using LittleByte.Common.AspNet.Core;

namespace Froggie.Api.Tasks.Models;

public sealed class TaskDto : Dto
{
    [Required]
    [StringRange(TitleRules.LengthMin, TitleRules.LengthMax)]
    public string Title { get; set; } = null!;
}