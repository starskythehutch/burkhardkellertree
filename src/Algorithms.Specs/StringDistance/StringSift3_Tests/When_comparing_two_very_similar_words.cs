namespace Algorithms.Specs.StringDistance.StringSift3_Tests
{
    using Algorithms.StringDistance;

    using Machine.Specifications;

    [Subject(typeof (StringSift3))]
    public class When_comparing_two_very_similar_words
    {
        private static StringSift3 sift;
        private static double distance;

        private Establish context = () => { sift = new StringSift3(); };

        private Because of = () => { distance = sift.Distance("Process", "Orocess"); };

        private It should_return_a_distance_of_one = () => { distance.ShouldEqual(1); };
    }
}