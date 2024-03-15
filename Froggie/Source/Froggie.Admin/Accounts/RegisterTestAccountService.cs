using Froggie.Data.Accounts;
using Froggie.Domain.Groups;
using Froggie.Domain.Users;

namespace Froggie.Admin.Accounts;

public sealed class RegisterTestAccountService(
    IAddUserCommand userCommand,
    IFindAccountByEmailQuery accountByEmailQuery,
    IDoesUserWithNameExistQuery userWithNameExistQuery,
    ICreateGroupService groupService,
    ICreateAccountCommand createAccountCommand,
    TestUserFactory userFactory) : AccountRegisterService(userCommand, accountByEmailQuery, userWithNameExistQuery,
    groupService, createAccountCommand, userFactory);