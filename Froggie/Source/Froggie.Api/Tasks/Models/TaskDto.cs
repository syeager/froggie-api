using System.ComponentModel.DataAnnotations;
using Froggie.Domain.Tasks.Validators;
using LittleByte.Common.AspNet.Attributes;

namespace Froggie.Api.Tasks.Models;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public sealed class TaskDto : Dto
{
    [Required]
    [StringRange(TitleRules.LengthMin, TitleRules.LengthMax)]
    public string Title { get; set; } = null!;
}