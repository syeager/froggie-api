using System.Net;
using AutoMapper;
using Froggie.Data.Users.Queries;
using LittleByte.Common.AspNet.Responses;
using LittleByte.Common.Infra.Models;
using Microsoft.AspNetCore.Mvc;

namespace Froggie.Api.Users;

public sealed class GetUserPageController : UserController
{
    private readonly IMapper mapper;
    private readonly IUserPageQuery userPageQuery;

    public GetUserPageController(IMapper mapper, IUserPageQuery userPageQuery)
    {
        this.mapper = mapper;
        this.userPageQuery = userPageQuery;
    }

    [HttpGet(Routes.GetByPage)]
    [ResponseType(HttpStatusCode.OK, typeof(PageResponse<UserDto>))]
    public async ValueTask<ApiResponse<PageResponse<UserDto>>> GetPage([FromQuery] PageRequest? request)
    {
        request ??= new PageRequest();
        var response = await userPageQuery.RunAsync(request);
        var dtos = response.CastResults(mapper.Map<UserDto>);
        return new OkResponse<PageResponse<UserDto>>(dtos);
    }
}