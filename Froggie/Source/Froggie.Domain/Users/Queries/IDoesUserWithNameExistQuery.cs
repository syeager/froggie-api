namespace Froggie.Domain.Users;

public interface IDoesUserWithNameExistQuery
{
    ValueTask<bool> SearchAsync(string nameValue);
}