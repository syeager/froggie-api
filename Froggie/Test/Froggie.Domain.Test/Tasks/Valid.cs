﻿using Froggie.Domain.Tasks;
using LittleByte.Common.Domain;
using LittleByte.Common.Extensions;

// ReSharper disable once CheckNamespace
namespace Froggie.Domain.Test;

public static partial class Valid
{
    public static class Tasks
    {
        public static readonly Title Title = new(new string('a', TitleRules.LengthMin));

        public static Task New() => Task.Create(new Id<Task>(), Title);

        public static IReadOnlyList<Task> New(int count)
        {
            return new List<Task>().Init(count, i => Task.Create(new Id<Task>(), new Title($"{Title}-{i}")));
        }
    }
}