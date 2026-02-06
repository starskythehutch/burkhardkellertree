namespace Algorithms.Specs.Trees.BurkhardKellerTree_Tests.AddItem
{
    using System;

    using Algorithms.StringDistance;
    using Algorithms.Trees;

    using Machine.Specifications;

    [Subject(typeof (BurkhardKellerTree<string>))]
    public class When_adding_an_item_that_is_null
    {
        private static Exception Exception;

        private static BurkhardKellerTree<string> tree;

        private Establish context =
            () => { tree = new BurkhardKellerTree<string>(DamerauLevenshtein.Similarity); };

        private Because of = () => { Exception = Catch.Exception(() => { tree.AddItem(null); }); };

        private It should_fail_with_an_ArgumentNullException = () => Exception.ShouldBeOfExactType<ArgumentNullException>();
    }
}