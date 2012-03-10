namespace Algorithms.Specs.StringDistance.DamerauLevenshtein_Tests.Distance
{
    using Algorithms.StringDistance;

    using Machine.Specifications;

    [Subject(typeof (DamerauLevenshtein))]
    public class When_passing_a_null_value_as_both_source_and_target
    {
        private static double distance;

        private Establish context = () => { };

        private Because of = () => { distance = DamerauLevenshtein.Distance(null, null); };

        private It should_return_zero = () => { distance.ShouldEqual(0); };
    }
}