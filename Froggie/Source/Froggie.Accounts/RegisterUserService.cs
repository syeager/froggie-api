using Froggie.Domain.Groups;
using Froggie.Domain.Users;
using LittleByte.Common;
using LittleByte.Domain;

namespace Froggie.Accounts;

public sealed record RegisterResult(User User, Account Account, Group PersonalGroup);

public interface IRegisterUserService
{
    ValueTask<Result<RegisterResult>> RegisterAsync(string email, string userName, string password);
}

public sealed class RegisterUserService(
    ICreateAccountService createAccount,
    IAddUserCommand addUser,
    ICreateGroupService createGroup
) : IRegisterUserService
{
    public async ValueTask<Result<RegisterResult>> RegisterAsync(string email, string userName, string password)
    {
        var accountResult = await createAccount.CreateAsync(email, userName, password);

        if(!accountResult.Succeeded)
        {
            // TODO
            throw new Exception();
        }

        var account = accountResult.Value;
        var id = new Id<User>(account.Id);
        var name = new UserName(userName);
        var user = User.Create(id, name);
        addUser.Add(user);
        var group = createGroup.CreatePersonal(user);

        var result = new RegisterResult(user, account, group);
        return Result<RegisterResult>.Success(result);
    }
}