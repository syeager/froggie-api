using Froggie.Data.Tasks;
using Froggie.Test;
using LittleByte.Common;

namespace Froggie.Data.Test.Tasks.Filters;

public sealed class CompletedFilterTest : UnitTest
{
    private CompletedFilter testObj = null!;

    [SetUp]
    public void SetUp()
    {
        testObj = new CompletedFilter();
    }

    [TestCase(true, 2)]
    [TestCase(false, 1)]
    public async ValueTask Given_Value_Return_Task(bool isCompleted, int expectedCount)
    {
        var query = new EnumerableQuery<Task>([
                ValidTask.New(isCompleted: true),
                ValidTask.New(isCompleted: true),
                ValidTask.New(isCompleted: false),
            ]);

        var queryRequest = new SearchParams
        {
            IsCompleted = isCompleted
        };

        var result = await testObj.FilterAsync(query, queryRequest).NoAwait();

        Assert.That(result.ToArray(), Has.Length.EqualTo(expectedCount));
    }

    [Test]
    public async ValueTask Given_Null_Then_Skip()
    {
        var query = new EnumerableQuery<Task>([
            ValidTask.New(isCompleted: true),
            ValidTask.New(isCompleted: false),
        ]);

        var queryRequest = new SearchParams
        {
            IsCompleted = null
        };

        var result = await testObj.FilterAsync(query, queryRequest).NoAwait();

        Assert.That(result.ToArray(), Has.Length.EqualTo(2));
    }
}