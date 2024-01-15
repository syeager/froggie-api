using System.IdentityModel.Tokens.Jwt;
using Froggie.Api.Users;
using Froggie.Domain.Users;
using LittleByte.AspNet;
using LittleByte.Common;
using LittleByte.Test.Categories;
using LittleByte.Validation.Test;
using NSubstitute;

namespace Froggie.Domain.Test.Users;

public sealed class LogInServiceTest : UnitTest
{
    private IFindUserByEmailAndPasswordQuery findUserQuery = null!;
    private LogInService testObj = null!;
    private ITokenGenerator tokenGenerator = null!;

    [SetUp]
    public void SetUp()
    {
        tokenGenerator = Substitute.For<ITokenGenerator>();
        findUserQuery = Substitute.For<IFindUserByEmailAndPasswordQuery>();
        testObj = new LogInService(tokenGenerator, findUserQuery);
    }

    [Test]
    public async ValueTask When_ValidData_Then_LogIn()
    {
        var user = AddUser();

        var result = await testObj.LogInAsync(Valid.Users.Email, Valid.Users.Password);

        AssertSuccess(result, user);
    }

    [Test]
    public async ValueTask When_NoUserWithEmail_Then_Fail()
    {
        var result = await testObj.LogInAsync(Valid.Users.Email, Valid.Users.Password);

        AssertFailure(result);
    }

    [Test]
    public async ValueTask When_BadPassword_Then_Fail()
    {
        AddUser();
        var wrongPassword = new Password("abd124");

        var result = await testObj.LogInAsync(Valid.Users.Email, wrongPassword);

        AssertFailure(result);
    }

    #region Helpers

    private User AddUser()
    {
        var validator = Validator.WillPass<User>();
        var user = User.Create(validator, new Id<User>(), Valid.Users.Email, Valid.Users.Name);
        findUserQuery
            .TryFindAsync(Valid.Users.Email, Valid.Users.Password)
            .Returns(user);
        tokenGenerator.GenerateJwt(default!).ReturnsForAnyArgs(new JwtSecurityToken());
        return user;
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