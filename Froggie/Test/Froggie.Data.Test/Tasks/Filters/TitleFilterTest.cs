using Froggie.Data.Tasks;
using Froggie.Test;
using LittleByte.Common;

namespace Froggie.Data.Test.Tasks.Filters;

public sealed class TitleFilterTest : UnitTest
{
    private TitleFilter testObj = null!;

    [SetUp]
    public void SetUp()
    {
        testObj = new TitleFilter();
    }

    [TestCase("oooo", 0)]
    [TestCase("HELLO", 1)]
    [TestCase("  o ", 2)]
    public async ValueTask Given_TitleSegment_Return_True(string segment, int expectedCount)
    {
        var query = new EnumerableQuery<Task>(ValidTask.New("Hello World", "Another one"));
        var queryRequest = new SearchParams
        {
            TitleSegment = segment
        };

        var result = await testObj.FilterAsync(query, queryRequest).NoAwait();

        Assert.That(result.ToArray(), Has.Length.EqualTo(expectedCount));
    }
}