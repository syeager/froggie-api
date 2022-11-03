using Froggie.Domain.Users;

namespace Froggie.Domain.Test.Users.Services;

public sealed class UserRegisterServiceTest : UnitTest
{
    private IAddUserCommand addUserCommand = null!;
    private IDoesUserWithNameExistQuery doesUserWithNameExistQuery = null!;
    private IFindUserByEmailQuery findUserByEmailQuery = null!;
    private UserRegisterService testObj = null!;
    private IUserFactory userFactory = null!;

    [SetUp]
    public void SetUp()
    {
        addUserCommand = Substitute.For<IAddUserCommand>();
        doesUserWithNameExistQuery = Substitute.For<IDoesUserWithNameExistQuery>();
        findUserByEmailQuery = Substitute.For<IFindUserByEmailQuery>();
        userFactory = Substitute.For<IUserFactory>();
        testObj = new UserRegisterService(addUserCommand, userFactory, findUserByEmailQuery, doesUserWithNameExistQuery);
    }

    [Test]
    public async ValueTask With_ValidData_Then_CreateUser()
    {
        var expectedUser = Valid.Users.New();
        userFactory
            .Create(Arg.Any<Guid>(), expectedUser.Email.Value, expectedUser.Name.Value)
            .Returns(expectedUser);

        var user = await testObj.RegisterAsync(
            expectedUser.Email.Value,
            expectedUser.Name.Value,
            Valid.Users.Password.Value);

        Assert.AreNotEqual(Guid.Empty, user.Id.Value);
        Assert.AreSame(expectedUser, user);
        await addUserCommand.Received(1).AddAsync(expectedUser, Valid.Users.Password);
    }

    [Test]
    public void When_EmailTaken_Then_Throw()
    {
        var existingUser = Valid.Users.New();
        findUserByEmailQuery.FindAsync(Valid.Users.Email.Value).Returns(existingUser);

        var exception = Assert.ThrowsAsync<EmailIsTakenException>(() => testObj.RegisterAsync(
            existingUser.Email.Value,
            Valid.Users.Name2.Value,
            Valid.Users.Password.Value).AsTask());

        Assert.AreEqual(existingUser.Email.Value, exception!.EmailValue);
    }

    [Test]
    public void When_NameTaken_Then_Throw()
    {
        var existingUser = Valid.Users.New();
        doesUserWithNameExistQuery.SearchAsync(existingUser.Name.Value).Returns(true);

        var exception = Assert.ThrowsAsync<NameIsTakenException>(() => testObj.RegisterAsync(
            Valid.Users.Email2.Value,
            existingUser.Name.Value,
            Valid.Users.Password.Value).AsTask());

        Assert.AreEqual(existingUser.Name.Value, exception!.NameValue);
    }
}