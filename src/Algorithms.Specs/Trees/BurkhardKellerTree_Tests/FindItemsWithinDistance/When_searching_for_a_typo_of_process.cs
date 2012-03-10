namespace Algorithms.Specs.Trees.BurkhardKellerTree_Tests.FindItemsWithinDistance
{
    using System.Collections.Generic;
    using System.Linq;

    using Algorithms.StringDistance;
    using Algorithms.Trees;

    using Machine.Specifications;

    [Subject(typeof (BurkhardKellerTree<string>))]
    public class When_searching_for_a_typo_of_process
    {
        private static List<string> items;

        private static string searchTerm;

        private static BurkhardKellerTree<string> tree;

        private Establish context = () =>
                                        {
                                            searchTerm = "Orocess";

                                            tree = new BurkhardKellerTree<string>(
                                                DamerauLevenshtein.Similarity);

                                            tree.AddItem("Engineering");
                                            tree.AddItem("Engineer");
                                            tree.AddItem("Operations");
                                            tree.AddItem("Maintanence");
                                            tree.AddItem("Maintanence");
                                            tree.AddItem("Process");
                                            tree.AddItem("and");
                                            tree.AddItem("the");
                                            tree.AddItem("we");
                                            tree.AddItem("you");
                                        };

        private Because of =
            () => { items = tree.FindItemsWithinDistanceOf(searchTerm, SearchPrecision.High).ToList(); };

        private It should_return_process = () => { items.ShouldContainOnly(new[] {"Process"}); };
    }
}