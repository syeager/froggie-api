using System.ComponentModel.DataAnnotations;

namespace Froggie.Api.Tasks;

public sealed class DeleteTaskRequest
{
    [Required]
    public Guid Id { get; init; }
}
