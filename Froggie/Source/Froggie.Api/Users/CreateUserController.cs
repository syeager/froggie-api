using Froggie.Domain.Users;
using LittleByte.EntityFramework;

namespace Froggie.Api.Users;

public sealed class CreateUserController(
    ILogInService inService,
    IUserRegisterService service,
    IMapper mapper,
    ISaveContextCommand command)
    : UserController
{
    // TODO: Should have RegisterResponse.
    [HttpPost(Routes.Create)]
    [ResponseType(HttpStatusCode.OK, typeof(LogInResponse))]
    [ResponseType(HttpStatusCode.BadRequest)]
    public async ValueTask<ApiResponse<LogInResponse>> Create(CreateUserRequest request)
    {
        var user = await service.RegisterAsync(request.Email, request.Name, request.Password);

        // TODO: There has to be a better way to get the password. Maybe RegisterAsync should return a RegisterResult?
        var password = new Password(request.Password);

        var logInResult = await inService.LogInAsync(user.Email, password);
        var response = mapper.Map<LogInResponse>(logInResult);

        await command.CommitChangesAsync();

        // TODO: Should be CreatedResponse.
        return new OkResponse<LogInResponse>(response);
    }
}