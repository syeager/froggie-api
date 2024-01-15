using Froggie.Domain.Users;

namespace Froggie.Data.Users;

public interface IUserPageQuery
{
    ValueTask<Page<User>> RunAsync(PageRequest request);
}

internal sealed class UserPageQuery : IUserPageQuery
{
    private readonly FroggieDb froggieDb;
    private readonly IMapper mapper;

    public UserPageQuery(FroggieDb froggieDb, IMapper mapper)
    {
        this.froggieDb = froggieDb;
        this.mapper = mapper;
    }

    public async ValueTask<Page<User>> RunAsync(PageRequest request)
    {
        var userDaos = await froggieDb.Users
            .Skip(request.PageIndex * request.PageSize)
            .Take(request.PageSize)
            .ToArrayAsync().ConfigureAwait(false);

        var users = userDaos
            .Select(u => mapper.Map<User>(u))
            .ToArray();

        var response = new Page<User>(request.PageSize, request.PageIndex, 0, 0, users);
        return response;
    }
}