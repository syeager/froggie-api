using System.ComponentModel.DataAnnotations;
using LittleByte.Common.Infra.Models;

namespace Froggie.Api.Tasks;

public sealed class GetTasksByUserRequest : PageRequest
{
    [Required]
    public Guid UserId { get; init; }
}