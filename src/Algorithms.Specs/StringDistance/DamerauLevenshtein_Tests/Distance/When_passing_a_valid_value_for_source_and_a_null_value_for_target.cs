namespace Algorithms.Specs.StringDistance.DamerauLevenshtein_Tests.Distance
{
    using Algorithms.StringDistance;

    using Machine.Specifications;

    [Subject(typeof (DamerauLevenshtein))]
    public class When_passing_a_valid_value_for_source_and_a_null_value_for_target
    {
        private static double distance;
        private static string source = "lorem";

        private Establish context = () => { };

        private Because of = () => { distance = DamerauLevenshtein.Distance(source, null); };

        private It should_return_the_length_of_source = () => { distance.ShouldEqual(source.Length); };
    }
}