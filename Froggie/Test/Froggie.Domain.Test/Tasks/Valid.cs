﻿using Froggie.Domain.Groups;
using Froggie.Domain.Tasks;
using Froggie.Domain.Users;
using LittleByte.Common.Domain;
using LittleByte.Common.Extensions;
using LittleByte.Common.Validation;
using LittleByte.Test.Validation;

// ReSharper disable once CheckNamespace
namespace Froggie.Domain.Test;

public static partial class Valid
{
    public static class Tasks
    {
        private static readonly IModelValidator<Task> validator = Validator.WillPass<Task>();

        public static readonly Title Title = new(new string('a', TitleRules.LengthMin));
        public static readonly DateTime DueDate = DateTime.MaxValue;

        public static Task New(Guid creatorId, Guid groupId) =>
            Task.Create(validator, new Id<Task>(), Title, new Id<User>(creatorId), DueDate, new Id<Group>(groupId));

        public static IReadOnlyList<Task> New(int count, Guid creatorId, Guid groupId)
        {
            return new List<Task>().Init(count,
                i => Task.Create(validator, new Id<Task>(), new Title($"{Title}-{i}"),
                    new Id<User>(creatorId), DueDate, new Id<Group>(groupId)));
        }
    }
}