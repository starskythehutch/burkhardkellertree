namespace Algorithms.Trees
{
    internal sealed class Edge<T>
    {
        public Edge(Node<T> endNode, double value)
        {
            EndNode = endNode;
            Value = value;
        }

        public Node<T> EndNode { get; private set; }

        public double Value { get; private set; }
    }
}
