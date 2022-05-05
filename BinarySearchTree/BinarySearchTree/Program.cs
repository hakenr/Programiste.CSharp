var tree = new BinarySearchTree();
tree.Insert(7);
tree.Insert(5);
tree.Insert(6);
tree.Insert(3);
tree.Insert(4);
tree.Insert(1);
tree.Insert(2);
tree.Insert(8);
tree.Insert(9);

foreach (var value in tree.GetValues())
{
	Console.WriteLine(value);
}
tree.Print();

Console.WriteLine(tree.Contains(2));
Console.WriteLine(tree.Contains(23));

public class Node
{
	public Node(int value)
	{
		this.Value = value;
	}

	public int Value { get; set; }
	public Node Left { get; set; }
	public Node Right { get; set; }
}

public class BinarySearchTree
{
	public Node Root { get; set; }

	public void Insert(int value)
	{
		Insert(value, Root);
	}
	private void Insert(int value, Node node)
	{
		if (node == null)
		{
			Root = new Node(value);
			return;
		}
		if (value < node.Value)
		{
			if (node.Left == null)
			{
				node.Left = new Node(value);
			}
			else
			{
				Insert(value, node.Left);
			}
		}
		else
		{
			if (node.Right == null)
			{
				node.Right = new Node(value);
			}
			else
			{
				Insert(value, node.Right);
			}
		}
	}

	public bool Contains(int value)
	{
		return Contains(value, Root);
	}
	private bool Contains(int value, Node node)
	{
		if (node == null)
		{
			return false;
		}
		if (value == node.Value)
		{
			return true;
		}
		if (value < node.Value)
		{
			return Contains(value, node.Left);
		}
		else
		{
			return Contains(value, node.Right);
		}
	}

	public List<int> GetValues()
	{
		return GetValues(Root);
	}
	private List<int> GetValues(Node node)
	{
		if (node == null)
		{
			return new List<int>();
		}
		var values = new List<int>();
		values.AddRange(GetValues(node.Left));
		values.Add(node.Value);
		values.AddRange(GetValues(node.Right));
		return values;
	}

	public void Print()
	{
		Print(Root, 0, String.Empty);
	}
	private void Print(Node node, int level, string direction)
	{
		if (node == null)
		{
			return;
		}
		Print(node.Left, level + 1, "/");
		Console.WriteLine(direction.PadLeft(level * 4) + " " + node.Value.ToString().PadRight(4));
		Print(node.Right, level + 1, "\\");
	}
}