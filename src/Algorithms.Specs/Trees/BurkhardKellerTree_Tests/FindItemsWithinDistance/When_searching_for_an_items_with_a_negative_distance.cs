namespace Algorithms.Specs.Trees.BurkhardKellerTree_Tests.FindItemsWithinDistance
{
    using System;

    using Algorithms.StringDistance;
    using Algorithms.Trees;

    using Machine.Specifications;

    [Subject(typeof (BurkhardKellerTree<string>))]
    public class When_searching_for_an_items_with_a_negative_distance
    {
        private static Exception Exception;

        private static BurkhardKellerTree<string> tree;

        private Establish context =
            () => { tree = new BurkhardKellerTree<string>(DamerauLevenshtein.Similarity); };

        private Because of =
            () => { Exception = Catch.Exception(() => { tree.FindItemsWithinDistanceOf("Lorem", -0.01f); }); };

        private It should_fail_with_an_ArgumentOutOfRangeException =
            () => Exception.ShouldBeOfExactType<ArgumentOutOfRangeException>();
    }
}