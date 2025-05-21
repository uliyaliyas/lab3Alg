using System;

class Program
{
    class TreeNode
    {
        public int Value;
        public TreeNode Left;
        public TreeNode Right;

        public TreeNode(int value)
        {
            Value = value;
            Left = null;
            Right = null;
        }
    }
    static void RemoveLeaves(ref TreeNode node)
    {
        if (node == null) return;

        if (node.Left != null)
        {
            if (node.Left.Left == null && node.Left.Right == null)
            {
                Console.WriteLine($"Удаляется лист: {node.Left.Value}");
                node.Left = null;
            }
            else
            {
                RemoveLeaves(ref node.Left);
            }
        }

        if (node.Right != null)
        {
            if (node.Right.Left == null && node.Right.Right == null)
            {
                Console.WriteLine($"Удаляется лист: {node.Right.Value}");
                node.Right = null;
            }
            else
            {
                RemoveLeaves(ref node.Right);
            }
        }
    }

    static void PrintTree(TreeNode node, string indent = "", bool isLeft = true)
    {
        if (node != null)
        {
            Console.WriteLine(indent + (isLeft ? "├──" : "└──") + node.Value);
            PrintTree(node.Left, indent + (isLeft ? "│   " : "    "), true);
            PrintTree(node.Right, indent + (isLeft ? "│   " : "    "), false);
        }
    }

    static void Main()
    {
        TreeNode P1 = new TreeNode(10);
        P1.Left = new TreeNode(5);
        P1.Left.Left = new TreeNode(2);
        P1.Right = new TreeNode(15);
        P1.Right.Left = new TreeNode(12);
        P1.Right.Right = new TreeNode(20);

        Console.WriteLine("Исходное дерево:");
        PrintTree(P1);

        RemoveLeaves(ref P1);

        Console.WriteLine("\nДерево после удаления листьев:");
        PrintTree(P1);
    }
}