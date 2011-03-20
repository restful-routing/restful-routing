using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;

namespace RestfulRouting.Tests.Unit
{
    public class inflector
    {
        It allows_exceptions = () =>
                                   {
                                       Inflector.AddPlural("die", "dice");
                                       Inflector.Pluralize("die").ShouldEqual("dice");

                                       Inflector.AddSingular("dice", "die");
                                       Inflector.Singularize("dice").ShouldEqual("die");

                                       Inflector.Reset();
                                   };

        It returns_the_word_for_unknown_singular = () => Inflector.Singularize("dice").ShouldEqual("dice");

        It returns_the_simplest_attempt_for_unknown_plural = () => Inflector.Pluralize("die").ShouldEqual("dies");
    }
}
