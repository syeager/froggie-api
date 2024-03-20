using Froggie.Data.Accounts;
using Froggie.Data.Users;
using Froggie.Domain.Groups;
using Froggie.Domain.Users;
using LittleByte.Common;

namespace Froggie.Data.Test.Accounts;

public sealed class AccountRegisterServiceTest : UnitTest
{
    private IAddUserCommand addUserCommand = null!;
    private IDoesUserWithNameExistQuery doesUserWithNameExistQuery = null!;
    private IFindAccountByEmailQuery findUserByEmailQuery = null!;
    private AccountRegisterService testObj = null!;
    private ICreateGroupService createGroupService = null!;
    private ICreateAccountCommand createAccountCommand = null!;

    [SetUp]
    public void SetUp()
    {
        addUserCommand = Substitute.For<IAddUserCommand>();
        doesUserWithNameExistQuery = Substitute.For<IDoesUserWithNameExistQuery>();
        findUserByEmailQuery = Substitute.For<IFindAccountByEmailQuery>();
        createGroupService = Substitute.For<ICreateGroupService>();
        createAccountCommand = Substitute.For<ICreateAccountCommand>();
        var userFactory = Substitute.For<IUserFactory>();
        userFactory.Create(default, default!).ReturnsForAnyArgs(Domain.Test.Valid.Users.New());
        testObj = new AccountRegisterService(addUserCommand, findUserByEmailQuery, doesUserWithNameExistQuery, createGroupService, createAccountCommand, userFactory);
    }

    [Test]
    public async ValueTask With_ValidData_Then_CreateUser()
    {
        var result = await testObj.RegisterAsync(
            Valid.Accounts.Email,
            Domain.Test.Valid.Users.Name,
            Valid.Accounts.Password);

        var user = result.Value!;
        Assert.That(user.Id, Is.Not.EqualTo(Id<User>.Empty));
        addUserCommand.ReceivedWithAnyArgs(1).Add(null!);
        createGroupService.Received(1).CreatePersonal(user);
    }

    [Test]
    public async ValueTask When_EmailTaken_Then_Throw()
    {
        var existingAccount = Valid.Accounts.New();
        findUserByEmailQuery.FindAsync(Valid.Accounts.Email).Returns(existingAccount);

        var result = await testObj.RegisterAsync(
            Valid.Accounts.Email,
            Domain.Test.Valid.Users.Name2,
            Valid.Accounts.Password);

        Assert.That(result, Is.TypeOf<EmailAlreadyExists>());
    }

    [Test]
    public async ValueTask When_NameTaken_Then_Throw()
    {
        var existingUser = Domain.Test.Valid.Users.New();
        doesUserWithNameExistQuery.SearchAsync(existingUser.Name).Returns(true);

        var result = await testObj.RegisterAsync(
            Valid.Accounts.Email2,
            existingUser.Name,
            Valid.Accounts.Password);

        Assert.That(result, Is.TypeOf<UsernameIsNotAvailable>());
    }
}