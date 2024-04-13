//using Froggie.Data.Tasks;
//using Froggie.Domain.Tasks;
//using Froggie.Test;

//namespace Froggie.Data.Test.Tasks.Filters;

//public sealed class SearchTasksTest : DataIntegrationTest
//{
//    private FroggieDb froggieDb = null!;
//    private SearchTasks testObj = null!;
//    private TaskFilter<ITitleFilterParams> titleFilter = null!;

//    [SetUp]
//    public override void SetUp()
//    {
//        base.SetUp();

//        froggieDb = GetService<FroggieDb>();
//        titleFilter = Substitute.For<TaskFilter<ITitleFilterParams>>();
//        testObj = new SearchTasks(froggieDb, titleFilter);
//    }

//    [TearDown]
//    public override void TearDown()
//    {
//        base.TearDown();

//        froggieDb.Dispose();
//    }

//    [Test]
//    public void Given_TitleFilter_Then_ReturnTasks()
//    {
//        var user = ValidUser.New();
//        var group = ValidGroup.New();
//        var 
//        var task = GetService<ICreateTaskService>().CreateAsync("Hello World", )
//        froggieDb.Tasks.Add(task);
//    }
//}