using AutoMapper;
using Froggie.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Froggie.Data.Users;

public interface IUserPageQuery
{
    ValueTask<PageResponse<User>> RunAsync(PageRequest request);
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

    public async ValueTask<PageResponse<User>> RunAsync(PageRequest request)
    {
        var userDaos = await froggieDb.Users
            .Skip(request.Page * request.PageSize)
            .Take(request.PageSize)
            .ToArrayAsync().ConfigureAwait(false);

        var users = userDaos
            .Select(u => mapper.Map<User>(u))
            .ToArray();

        var response = new PageResponse<User>(request.PageSize, request.Page, 0, 0, users);
        return response;
    }
}