namespace Algorithms.Trees
{
    internal sealed class Edge<T>
    {
        public Node<T> EndNode { get; set; }

        public Node<T> StartNode { get; set; }

        public double Value { get; set; }
    }
}