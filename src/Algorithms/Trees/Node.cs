﻿namespace Algorithms.Trees
{
    using System.Collections.Generic;

    internal sealed class Node<T>
    {
        private readonly List<Edge<T>> edges = new List<Edge<T>>();

        public T Data { get; set; }

        public IEnumerable<Edge<T>> Edges
        {
            get { return this.edges.AsReadOnly(); }
        }

        public void AddEdge(Edge<T> edge)
        {
            this.edges.Add(edge);
        }
    }
}