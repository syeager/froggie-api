using Froggie.Domain.Groups;

namespace Froggie.Domain.Users;

public interface IUserRegisterService
{
    ValueTask<User> RegisterAsync(string emailValue, string nameValue, string passwordValue);
}

internal sealed class UserRegisterService : IUserRegisterService
{
    private readonly IAddUserCommand addUserCommand;
    private readonly IDoesUserWithNameExistQuery doesUserWithNameExistQuery;
    private readonly IFindUserByEmailQuery findUserByEmailQuery;
    private readonly IUserFactory userFactory;
    private readonly ICreateGroupService createGroupService;

    public UserRegisterService(IAddUserCommand addUserCommand,
                               IUserFactory userFactory,
                               IFindUserByEmailQuery findUserByEmailQuery,
                               IDoesUserWithNameExistQuery doesUserWithNameExistQuery,
                               ICreateGroupService createGroupService)
    {
        this.addUserCommand = addUserCommand;
        this.userFactory = userFactory;
        this.findUserByEmailQuery = findUserByEmailQuery;
        this.doesUserWithNameExistQuery = doesUserWithNameExistQuery;
        this.createGroupService = createGroupService;
    }

    public async ValueTask<User> RegisterAsync(string emailValue, string nameValue, string passwordValue)
    {
        using var logger = this.NewLogger()
            .Push<Email>(emailValue)
            .Push<Name>(nameValue)
            .Info("Register user");

        var nameIsTaken = await doesUserWithNameExistQuery.SearchAsync(nameValue);
        if(nameIsTaken)
        {
            throw new NameIsTakenException(nameValue);
        }

        var user = await findUserByEmailQuery.FindAsync(emailValue);
        if(user is not null)
        {
            throw new EmailIsTakenException(emailValue);
        }

        var id = new Id<User>();
        user = userFactory.Create(id, emailValue, nameValue);

        var password = new Password(passwordValue);
        await addUserCommand.AddAsync(user, password);

        // TODO: Add to a new RegisterResult.
        _ = await createGroupService.CreatePersonalAsync(user);

        return user;
    }
}