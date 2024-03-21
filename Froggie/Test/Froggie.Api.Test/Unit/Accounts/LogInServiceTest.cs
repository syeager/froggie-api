using System.IdentityModel.Tokens.Jwt;
using Froggie.Api.Accounts;
using Froggie.Data.Accounts;
using Froggie.Domain.Users;
using Froggie.Test;
using LittleByte.AspNet;
using LittleByte.Common;
using LittleByte.Test.Categories;
using NSubstitute;

namespace Froggie.Api.Test.Unit.Accounts;

public sealed class LogInServiceTest : UnitTest
{
    private IFindAccountByEmailAndPassword findAccountQuery = null!;
    private LogInService testObj = null!;
    private ITokenGenerator tokenGenerator = null!;

    [SetUp]
    public void SetUp()
    {
        tokenGenerator = Substitute.For<ITokenGenerator>();
        findAccountQuery = Substitute.For<IFindAccountByEmailAndPassword>();
        testObj = new LogInService(tokenGenerator, findAccountQuery);
    }

    [Test]
    public async ValueTask When_ValidData_Then_LogIn()
    {
        var user = AddUser();

        var result = await testObj.LogInAsync(ValidAccount.Email, ValidAccount.Password);

        AssertSuccess(result, user);
    }

    [Test]
    public async ValueTask When_NoUserWithEmail_Then_Fail()
    {
        findAccountQuery.TryFindAsync(null!, null!).ReturnsForAnyArgs((Account?)null);

        var result = await testObj.LogInAsync(ValidAccount.Email, ValidAccount.Password);

        AssertFailure(result);
    }

    [Test]
    public async ValueTask When_BadPassword_Then_Fail()
    {
        AddUser();
        findAccountQuery.TryFindAsync(null!, null!).ReturnsForAnyArgs((Account?)null);

        var result = await testObj.LogInAsync(ValidAccount.Email, new Password(""));

        AssertFailure(result);
    }

    #region Helpers

    private User AddUser()
    {
        var account = new Account
        {
            User = User.Create(new Id<User>(), ValidUser.Name),
            Email = ValidAccount.Email,
            UserName = ValidUser.Name
        };
        findAccountQuery
            .TryFindAsync(ValidAccount.Email, ValidAccount.Password)
            .Returns(account);
        tokenGenerator.GenerateJwt(default!).ReturnsForAnyArgs(new JwtSecurityToken());
        return account.User;
    }

    private static void AssertSuccess(LogInResult result, User user)
    {
        Assert.Multiple(() =>
        {
            Assert.That(result.AccessToken, Is.Not.Null);
            Assert.That(result.Errors, Is.Null);
            Assert.That(result.Succeeded, Is.True);
            Assert.That(result.User, Is.SameAs(user));
        });
    }

    private static void AssertFailure(LogInResult result)
    {
        Assert.Multiple(() =>
        {
            Assert.That(result.AccessToken, Is.Null);
            Assert.That(result.Errors, Is.Not.Empty);
            Assert.That(result.Succeeded, Is.False);
            Assert.That(result.User, Is.Null);
        });
    }

    #endregion
}