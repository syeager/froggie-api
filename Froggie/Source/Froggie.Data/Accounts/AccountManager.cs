using Microsoft.AspNetCore.Identity;

namespace Froggie.Data.Accounts;

internal interface IAccountManager
{
    string NormalizeEmail(Email email);
    Task<IdentityResult> AddAsync(Account account, Password password);
    Task<Account?> FindByEmailAsync(Email email);
    Task<bool> CheckPasswordAsync(Account account, Password password);
}

internal sealed class AccountManager : IAccountManager
{
    private readonly UserManager<Account> userManager;

    public AccountManager(UserManager<Account> userManager)
    {
        this.userManager = userManager;
        userManager.UserValidators.Clear();
        userManager.PasswordValidators.Clear();
    }

    public string NormalizeEmail(Email email) => userManager.NormalizeEmail(email);

    public Task<IdentityResult> AddAsync(Account account, Password password) => userManager.CreateAsync(account, password);
    public Task<Account?> FindByEmailAsync(Email email) => userManager.FindByEmailAsync(email);
    public Task<bool> CheckPasswordAsync(Account account, Password password) => userManager.CheckPasswordAsync(account, password);
}