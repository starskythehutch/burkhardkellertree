namespace Algorithms.Specs.Trees.BurkhardKellerTree_Tests.FindItemsWithinDistance
{
    using System.Collections.Generic;
    using System.Linq;

    using Algorithms.StringDistance;
    using Algorithms.Trees;

    using Machine.Specifications;

    [Subject(typeof (BurkhardKellerTree<string>))]
    public class When_populating_the_tree_with_lionel_richie
    {
        private static List<string> items;

        private static string searchTerm;

        private static BurkhardKellerTree<string> tree;

        private Establish context = () =>
                                        {
                                            searchTerm = "me";
                                            tree = new BurkhardKellerTree<string>(
                                                DamerauLevenshtein.Distance);

                                            tree.AddItem("Hello?");
                                            tree.AddItem("Is");
                                            tree.AddItem("it");
                                            tree.AddItem("me");
                                            tree.AddItem("you're");
                                            tree.AddItem("looking");
                                            tree.AddItem("for?");
                                        };

        private Because of = () =>
                                 {
                                     items =
                                         tree.FindItemsWithinDistanceOf(searchTerm, SearchPrecision.ExactPrecision).ToList
                                             ();
                                 };

        private It should_be_me_youre_looking_for = () => items.ShouldContainOnly(new[] {searchTerm});
    }
}