using System.IdentityModel.Tokens.Jwt;
using Froggie.Test;
using LittleByte.AspNet;

namespace Froggie.Accounts.Test;

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
        AddAccount();

        var result = await testObj.LogInAsync(ValidAccount.Email, ValidAccount.Password);

        AssertSuccess(result);
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
        AddAccount();
        findAccountQuery.TryFindAsync(null!, null!).ReturnsForAnyArgs((Account?)null);

        var result = await testObj.LogInAsync(ValidAccount.Email, new Password(""));

        AssertFailure(result);
    }

    #region Helpers

    private void AddAccount()
    {
        var account = new Account
        {
            Email = ValidAccount.Email,
            UserName = ValidUser.Name
        };
        findAccountQuery
            .TryFindAsync(ValidAccount.Email, ValidAccount.Password)
            .Returns(account);
        tokenGenerator.GenerateJwt(default!).ReturnsForAnyArgs(new JwtSecurityToken());
    }

    private static void AssertSuccess(LogInResult result)
    {
        Assert.Multiple(() =>
        {
            Assert.That(result.AccessToken, Is.Not.Null);
            Assert.That(result.Errors, Is.Null);
            Assert.That(result.Succeeded, Is.True);
        });
    }

    private static void AssertFailure(LogInResult result)
    {
        Assert.Multiple(() =>
        {
            Assert.That(result.AccessToken, Is.Null);
            Assert.That(result.Errors, Is.Not.Empty);
            Assert.That(result.Succeeded, Is.False);
        });
    }

    #endregion
}