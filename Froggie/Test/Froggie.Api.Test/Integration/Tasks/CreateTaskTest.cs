﻿using System.Net;
using Froggie.Api.Tasks;
using Froggie.Domain.Groups;
using Froggie.Domain.Test;
using LittleByte.AspNet.Test;
using Microsoft.Extensions.DependencyInjection;

namespace Froggie.Api.Test.Integration.Tasks;

public sealed class CreateTaskTest : ApiIntegrationTest<CreateTaskController>
{
    [Test]
    public async ValueTask CreateTask_Success()
    {
        var group = Valid.Groups.New();
        var user = Valid.Users.New();

        var userGroupCreateCommand = services.GetRequiredService<IUserGroupCreateCommand>();
        userGroupCreateCommand.Create(user, group);

        await saveCommand.CommitChangesAsync();

        var request = new CreateTaskRequest
        {
            Title = Valid.Tasks.Title,
            CreatorId = user.Id,
            DueDate = Valid.Tasks.DueDate,
            GroupId = group.Id
        };

        var response = await controller.Create(request);

       Assert.Multiple(() =>
       {
           ApiAssert.IsSuccess(response, HttpStatusCode.Created);
           Assert.That(response.Obj!.GroupId, Is.EqualTo(group.Id.Value));
       });
    }
}