using System.Collections.Generic;

namespace AdventOfCodeLib.Models;

public class Node<T>
{
    public Node(T currentNode)
    {
        CurrentNode = currentNode;
    }

    public T CurrentNode { get; set; }

    public bool IsLeaf { get; set; }

    public IList<Node<T>> ChildNodes { get; } = new List<Node<T>>();
}