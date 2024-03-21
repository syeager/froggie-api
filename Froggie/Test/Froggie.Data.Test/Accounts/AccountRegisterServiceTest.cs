using Froggie.Data.Accounts;
using Froggie.Data.Users;
using Froggie.Domain.Groups;
using Froggie.Domain.Users;
using Froggie.Test;
using LittleByte.Common;

namespace Froggie.Data.Test.Accounts;

public sealed class AccountRegisterServiceTest : UnitTest
{
    private IAddUserCommand addUserCommand = null!;
    private ICreateAccountCommand createAccountCommand = null!;
    private ICreateGroupService createGroupService = null!;
    private IDoesUserWithNameExistQuery doesUserWithNameExistQuery = null!;
    private IFindAccountByEmailQuery findUserByEmailQuery = null!;
    private AccountRegisterService testObj = null!;

    [SetUp]
    public void SetUp()
    {
        addUserCommand = Substitute.For<IAddUserCommand>();
        doesUserWithNameExistQuery = Substitute.For<IDoesUserWithNameExistQuery>();
        findUserByEmailQuery = Substitute.For<IFindAccountByEmailQuery>();
        createGroupService = Substitute.For<ICreateGroupService>();
        createAccountCommand = Substitute.For<ICreateAccountCommand>();
        var userFactory = Substitute.For<IUserFactory>();
        userFactory.Create(default, default!).ReturnsForAnyArgs(ValidUser.New());
        testObj = new AccountRegisterService(addUserCommand, findUserByEmailQuery, doesUserWithNameExistQuery,
            createGroupService, createAccountCommand, userFactory);
    }

    [Test]
    public async ValueTask With_ValidData_Then_CreateUser()
    {
        var result = await testObj.RegisterAsync(
            ValidAccount.Email,
            ValidUser.Name,
            ValidAccount.Password);

        var user = result.Value!;
        Assert.That(user.Id, Is.Not.EqualTo(Id<User>.Empty));
        addUserCommand.ReceivedWithAnyArgs(1).Add(null!);
        createGroupService.Received(1).CreatePersonal(user);
    }

    [Test]
    public async ValueTask When_EmailTaken_Then_Throw()
    {
        var existingAccount = ValidAccount.New();
        findUserByEmailQuery.FindAsync(ValidAccount.Email).Returns(existingAccount);

        var result = await testObj.RegisterAsync(
            ValidAccount.Email,
            ValidUser.Name2,
            ValidAccount.Password);

        Assert.That(result, Is.TypeOf<EmailAlreadyExists>());
    }

    [Test]
    public async ValueTask When_NameTaken_Then_Throw()
    {
        var existingUser = ValidUser.New();
        doesUserWithNameExistQuery.SearchAsync(existingUser.Name).Returns(true);

        var result = await testObj.RegisterAsync(
            ValidAccount.Email2,
            existingUser.Name,
            ValidAccount.Password);

        Assert.That(result, Is.TypeOf<UsernameIsNotAvailable>());
    }
}