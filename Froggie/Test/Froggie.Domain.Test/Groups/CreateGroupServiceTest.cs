﻿using Froggie.Domain.Groups;
using Froggie.Domain.Users;

namespace Froggie.Domain.Test.Groups;

public sealed class CreateGroupServiceTest : UnitTest
{
    private static readonly Group ExpectedGroup = Valid.Groups.New();
    private static readonly User User = Valid.Users.New();

    private CreateGroupService testObj = null!;
    private IAddGroupCommand addGroupCommand = null!;

    [SetUp]
    public void SetUp()
    {
        addGroupCommand = Substitute.For<IAddGroupCommand>();
        testObj = new CreateGroupService(addGroupCommand);
    }

    [Test]
    public void When_ValidData_Then_CreateGroup()
    {
        var group = testObj.Create(User, ExpectedGroup.Name);

        addGroupCommand.Received(1).Add(group);
    }

    [Test]
    public void When_CreatingPersonalGroup_Then_CreateGroup()
    {
        var group = testObj.CreatePersonal(User);

        Assert.That(group.Name, Is.EqualTo(GroupNameRules.PersonalName));
    }
}