using System.ComponentModel.DataAnnotations;

namespace Froggie.Api.Tasks;

public sealed class GetTasksByUserRequest : PageRequest
{
    [Required]
    public Guid UserId { get; init; }
}