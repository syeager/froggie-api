using Froggie.Domain.Tasks.Models;
using Froggie.Domain.Tasks.Validators;

namespace Froggie.Domain.Test.Tasks;

public static class TV
{
    public static readonly Title Title = new(new string('a', TitleRules.LengthMin));
}