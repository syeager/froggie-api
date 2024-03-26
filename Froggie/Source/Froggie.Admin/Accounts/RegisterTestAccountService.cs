using Froggie.Accounts;
using Froggie.Domain.Users;

namespace Froggie.Admin.Accounts;

public sealed class TestCreateAccountService(
    IFindAccountByEmailQuery accountByEmailQuery,
    IDoesUserWithNameExistQuery userWithNameExistQuery,
    ICreateAccountCommand createAccountCommand) : CreateAccountService(accountByEmailQuery, userWithNameExistQuery,
    createAccountCommand);