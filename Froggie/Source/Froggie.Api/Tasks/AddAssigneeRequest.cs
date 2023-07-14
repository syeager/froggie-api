using System.ComponentModel.DataAnnotations;

namespace Froggie.Api.Tasks;

public sealed class AddAssigneeRequest
{
    [Required]
    public Guid TaskId { get; init; }

    [Required]
    public Guid UserId { get; init; }
}