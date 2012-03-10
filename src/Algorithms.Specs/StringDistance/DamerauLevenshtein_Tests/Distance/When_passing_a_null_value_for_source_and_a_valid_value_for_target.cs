namespace Algorithms.Specs.StringDistance.DamerauLevenshtein_Tests.Distance
{
    using Algorithms.StringDistance;

    using Machine.Specifications;

    [Subject(typeof (DamerauLevenshtein))]
    public class When_passing_a_null_value_for_source_and_a_valid_value_for_target
    {
        private static double distance;
        private static string target = "lorem";

        private Establish context = () => { };

        private Because of = () => { distance = DamerauLevenshtein.Distance(null, target); };

        private It should_return_the_length_of_target = () => { distance.ShouldEqual(target.Length); };
    }
}