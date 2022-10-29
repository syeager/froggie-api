using Froggie.Domain.Users;

namespace Froggie.Domain.Test.Users.Services;

/*
 * email exists
 * username exists
 */
public sealed class UserRegisterServiceTest : UnitTest
{
    private IAddUserCommand addUserCommand = null!;
    private UserRegisterService testObj = null!;
    private IUserFactory userFactory = null!;

    [SetUp]
    public void SetUp()
    {
        addUserCommand = Substitute.For<IAddUserCommand>();
        userFactory = Substitute.For<IUserFactory>();
        testObj = new UserRegisterService(addUserCommand, userFactory);
    }

    [Test]
    public async ValueTask When_ValidData_Then_CreateUser()
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
    }
}