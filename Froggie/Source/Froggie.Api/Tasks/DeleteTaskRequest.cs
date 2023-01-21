using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;

namespace Froggie.Api.Tasks;

public sealed class DeleteTaskRequest
{
    [Required]
    public Guid Id { get; [UsedImplicitly] init; }
}