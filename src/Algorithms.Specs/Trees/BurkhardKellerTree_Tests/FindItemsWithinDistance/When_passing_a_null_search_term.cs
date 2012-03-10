namespace Algorithms.Specs.Trees.BurkhardKellerTree_Tests.FindItemsWithinDistance
{
    using System;

    using Machine.Specifications;

    using Algorithms.StringDistance;
    using Algorithms.Trees;

    [Subject(typeof (BurkhardKellerTree<string>))]
    public class When_passing_a_null_search_term
    {
        private static Exception Exception;

        private static BurkhardKellerTree<string> tree;

        private Establish context =
            () => { tree = new BurkhardKellerTree<string>(DamerauLevenshtein.Similarity); };

        private Because of = () => { Exception = Catch.Exception(() => { tree.FindItemsWithinDistanceOf(null, 0); }); };

        private It should_fail_with_an_ArgumentException = () => Exception.ShouldBeOfType<ArgumentException>();
    }
}