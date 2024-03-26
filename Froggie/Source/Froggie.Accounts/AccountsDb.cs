using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Froggie.Accounts;

internal sealed class AccountsDb(DbContextOptions<AccountsDb> options)
    : IdentityDbContext<Account, IdentityRole<Guid>, Guid>(options) { }