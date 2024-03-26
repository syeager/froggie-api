using Froggie.Domain.Users;
using Froggie.Test;

namespace Froggie.Accounts.Test;

// TODO: Split into CreateAccount and RegisterUser
public sealed class CreateAccountServiceTest : UnitTest
{
    private ICreateAccountCommand createAccountCommand = null!;
    private IDoesUserWithNameExistQuery doesUserWithNameExistQuery = null!;
    private IFindAccountByEmailQuery findUserByEmailQuery = null!;
    private CreateAccountService testObj = null!;

    [SetUp]
    public void SetUp()
    {
        doesUserWithNameExistQuery = Substitute.For<IDoesUserWithNameExistQuery>();
        findUserByEmailQuery = Substitute.For<IFindAccountByEmailQuery>();
        createAccountCommand = Substitute.For<ICreateAccountCommand>();
        testObj = new CreateAccountService(findUserByEmailQuery, doesUserWithNameExistQuery, createAccountCommand);
    }

    [Test]
    public async ValueTask With_ValidData_Then_CreateUser()
    {
        var expected = new Account();
        createAccountCommand.CreateAsync(Arg.Any<Guid>(), ValidUser.Name, ValidAccount.Email, ValidAccount.Password).Returns(expected);
        
        var result = await testObj.CreateAsync(
            ValidAccount.Email,
            ValidUser.Name,
            ValidAccount.Password);

        Assert.That(result.Value, Is.Not.Null);
    }

    [Test]
    public async ValueTask When_EmailTaken_Then_Throw()
    {
        var existingAccount = ValidAccount.New();
        findUserByEmailQuery.FindAsync(ValidAccount.Email).Returns(existingAccount);

        var result = await testObj.CreateAsync(
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

        var result = await testObj.CreateAsync(
            ValidAccount.Email2,
            existingUser.Name,
            ValidAccount.Password);

        Assert.That(result, Is.TypeOf<UsernameIsNotAvailable>());
    }
}