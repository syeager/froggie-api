using Froggie.Api.Tasks;
using Froggie.Api.Test.Integration.Groups;
using Froggie.Domain.Tasks;
using Froggie.Data.Test;
using LittleByte.Common;

namespace Froggie.Api.Test.Integration.Tasks;

public sealed class CompleteTaskTestTest : ApiIntegrationTest<CompleteTaskController>
{
    //[Test]
    //public async System.Threading.Tasks.Task CompleteNotCompletedTask()
    //{
    //    GetService<ICreateAccountService>().CreateAsync(Valid.Accounts.Email, Valid.Users)
    //    await saveCommand.CommitChangesAsync();

    //    var taskResult = await GetService<ICreateTaskService>()
    //        .CreateAsync("Test", group.Users.First(), Valid.Tasks.DueDate, group);
    //    await saveCommand.CommitChangesAsync();

    //    var request = new CompleteTaskRequest
    //    {
    //        TaskId = task.Id,
    //    };

    //    var response = await controller.Complete(request);


    //}
}