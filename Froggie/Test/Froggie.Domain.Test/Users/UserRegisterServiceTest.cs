using Froggie.Domain.Groups;
using Froggie.Domain.Users;
using LittleByte.Common;

namespace Froggie.Domain.Test.Users;

public sealed class UserRegisterServiceTest : UnitTest
{
    private IAddUserCommand addUserCommand = null!;
    private IDoesUserWithNameExistQuery doesUserWithNameExistQuery = null!;
    private IFindUserByEmailQuery findUserByEmailQuery = null!;
    private UserRegisterService testObj = null!;
    private IUserFactory userFactory = null!;
    private ICreateGroupService createGroupService = null!;

    [SetUp]
    public void SetUp()
    {
        addUserCommand = Substitute.For<IAddUserCommand>();
        doesUserWithNameExistQuery = Substitute.For<IDoesUserWithNameExistQuery>();
        findUserByEmailQuery = Substitute.For<IFindUserByEmailQuery>();
        userFactory = Substitute.For<IUserFactory>();
        createGroupService = Substitute.For<ICreateGroupService>();
        testObj = new UserRegisterService(addUserCommand, userFactory, findUserByEmailQuery, doesUserWithNameExistQuery, createGroupService);
    }

    [Test]
    public async ValueTask With_ValidData_Then_CreateUser()
    {
        var expectedUser = Valid.Users.New();
        userFactory
            .WhenForAnyArgs(c => c.Create(default, null!, null!))
            .Do(info => info.Arg<Id<User>>());
        userFactory
            .Create(default, default!, default!)
            .ReturnsForAnyArgs(expectedUser);

        var user = await testObj.RegisterAsync(
            expectedUser.Email,
            expectedUser.Name,
            Valid.Users.Password);

        Assert.Multiple(() =>
        {
            Assert.That(user.Id, Is.Not.EqualTo(Id<User>.Empty));
            Assert.That(user, Is.SameAs(expectedUser));
        });

        await addUserCommand.Received(1).AddAsync(expectedUser, Valid.Users.Password);
        await createGroupService.Received(1).CreatePersonalAsync(user);
    }

    [Test]
    public void When_EmailTaken_Then_Throw()
    {
        var existingUser = Valid.Users.New();
        findUserByEmailQuery.FindAsync(Valid.Users.Email).Returns(existingUser);

        var exception = Assert.ThrowsAsync<EmailIsTakenException>(() => testObj.RegisterAsync(
            existingUser.Email,
            Valid.Users.Name2,
            Valid.Users.Password).AsTask());

        Assert.That(exception!.EmailValue, Is.EqualTo(existingUser.Email.Value));
    }

    [Test]
    public void When_NameTaken_Then_Throw()
    {
        var existingUser = Valid.Users.New();
        doesUserWithNameExistQuery.SearchAsync(existingUser.Name).Returns(true);

        var exception = Assert.ThrowsAsync<NameIsTakenException>(() => testObj.RegisterAsync(
            Valid.Users.Email2,
            existingUser.Name,
            Valid.Users.Password).AsTask());

        Assert.That(exception!.NameValue, Is.EqualTo(existingUser.Name.Value));
    }
}