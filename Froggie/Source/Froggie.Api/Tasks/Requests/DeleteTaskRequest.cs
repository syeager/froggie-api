using LittleByte.Common.Domain;
using System.ComponentModel.DataAnnotations;

namespace Froggie.Api.Tasks.Requests;

public sealed class DeleteTaskRequest
{
    [Required]
    public Guid Id { get; init; }
}
