using System.ComponentModel.DataAnnotations;

namespace Froggie.Api.Groups;

public record AddUserToGroupRequest([Required] Guid UserId, [Required] Guid GroupId);