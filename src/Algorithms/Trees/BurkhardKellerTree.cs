namespace Algorithms.Trees
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class BurkhardKellerTree<T>
    {
        private readonly Func<T, T, double> distanceCalculator;

        /// <summary>
        ///   Initializes a new instance of the <see cref="BurkhardKellerTree{T}" /> class.
        /// </summary>
        /// <param name="distanceCalculator"> The alogrithm to calculate the distance between two objects. </param>
        public BurkhardKellerTree(Func<T, T, double> distanceCalculator)
        {
            if (distanceCalculator == null)
            {
                throw new ArgumentNullException("distanceCalculator");
            }

            this.distanceCalculator = distanceCalculator;
        }

        private Node<T> RootNode { get; set; }

        public void AddItem(T item)
        {
            if (EqualityComparer<T>.Default.Equals(item, default(T)))
            {
                throw new ArgumentNullException("item", "Item must have a value.");
            }

            if (RootNode == null)
            {
                RootNode = new Node<T> {Data = item};
            }
            else
            {
                InsertNode(RootNode, item);
            }
        }

        /// <summary>
        ///   Finds all items in the tree within a specified distance.
        /// </summary>
        /// <param name="value"> The search term to match against. </param>
        /// <param name="distance"> The distance to a node that indicates a match. The range of values for difference varies depending on the difference alogrithm. This value should always be positive. </param>
        /// <returns> The matching items in the tree. </returns>
        public IEnumerable<T> FindItemsWithinDistanceOf(T value, double distance)
        {
            if (EqualityComparer<T>.Default.Equals(value, default(T)))
            {
                throw new ArgumentException("Value must have a value", "value");
            }

            if (distance < 0)
            {
                throw new ArgumentOutOfRangeException("distance", "Distance should be a positive number.");
            }

            return FindMatchingItems(RootNode, value, distance);
        }

        private IEnumerable<T> FindMatchingItems(Node<T> node, T value, double distance)
        {
            double differenceWithNode = distanceCalculator(node.Data, value);

            double minimum = differenceWithNode - distance;
            double maximum = differenceWithNode + distance;

            if (differenceWithNode <= distance)
            {
                yield return node.Data;
            }

            foreach (Edge<T> edge in node.Edges.Where(e => e.Value >= minimum && e.Value <= maximum))
            {
                foreach (T item in FindMatchingItems(edge.EndNode, value, distance))
                {
                    yield return item;
                }
            }
        }

        private void InsertNode(Node<T> node, T item)
        {
            double distance = distanceCalculator(node.Data, item);

            if (distance > 0)
            {
                Edge<T> edge = node.Edges.SingleOrDefault(e => distance.Equals(e.Value));

                if (edge != null)
                {
                    InsertNode(edge.EndNode, item);
                }
                else
                {
                    var newEdge = new Edge<T> {EndNode = new Node<T> {Data = item}, StartNode = node, Value = distance};

                    node.AddEdge(newEdge);
                }
            }
        }
    }
}