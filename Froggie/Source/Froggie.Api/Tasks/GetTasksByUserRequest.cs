using System.ComponentModel.DataAnnotations;
using LittleByte.Data;

namespace Froggie.Api.Tasks;

public sealed record GetTasksByUserRequest(
    [Required] Guid UserId,
    int PageIndex = 0,
    int PageSize = PageRequest.DefaultPageSize)
    : PageRequest(PageIndex, PageSize) { }