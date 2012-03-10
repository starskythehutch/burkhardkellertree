namespace Algorithms.Specs.Trees.BurkhardKellerTree_Tests.ctor
{
    using System;

    using Algorithms.Trees;

    using Machine.Specifications;

    [Subject(typeof (BurkhardKellerTree<string>))]
    public class When_initialising_a_new_instance_with_a_null_distance_calculator
    {
        private static Exception Exception;

        private Establish context = () => { };

        private Because of =
            () => { Exception = Catch.Exception(() => { new BurkhardKellerTree<string>(null); }); };

        private It should_fail_with_an_argument_null_exception = () => Exception.ShouldBeOfType<ArgumentNullException>();
    }
}