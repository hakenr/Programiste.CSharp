var linkedList = new LinkedList();
linkedList.AddEnd("Marek");
linkedList.AddStart("Ahoj");
linkedList.AddStart("Nazdar");
linkedList.AddStart("Hello");
linkedList.AddEnd("Marek");
linkedList.Print();

public class Node
{
	public string Value { get; set; }
	public Node PreviousNode { get; set; }
	public Node NextNode { get; set; }
}

public class LinkedList
{
	private Node firstNode;
	private Node lastNode;

	public void AddStart(string item)
	{
		var newNode = new Node();
		newNode.Value = item;
		newNode.PreviousNode = null;
		newNode.NextNode = this.firstNode;
		
		// dosavadní první uzel musí ukazovat na nový předchozí uzel
		if (this.firstNode != null)
		{
			this.firstNode.PreviousNode = newNode;
		}
		
		this.firstNode = newNode;
		
		// pokud vkládámé první uzel, je zároveň posledním
		if (lastNode == null)
		{
			this.lastNode = newNode;
		}
	}

	public void Print()
	{
		var currentNode = this.firstNode;
		while (currentNode != null)
		{
			Console.WriteLine(currentNode.Value);
			currentNode = currentNode.NextNode;
		}
	}

	public void AddEnd(string item)
	{
		var newNode = new Node();
		newNode.Value = item;
		newNode.PreviousNode = this.lastNode;
		newNode.NextNode = null;

		// dosavadní poslední uzel musí ukazovat na nový následující uzel
		if (this.lastNode != null)
		{
			this.lastNode.NextNode = newNode;
		}

		this.lastNode = newNode;

		// pokud vkládámé první uzel, je zároveň prvním vůbec
		if (firstNode == null)
		{
			this.firstNode = newNode;
		}
	}

}
