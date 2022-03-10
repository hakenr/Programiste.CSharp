# Spojový seznam (LinkedList)

[Spojový (lineární) seznam](https://cs.wikipedia.org/wiki/Line%C3%A1rn%C3%AD_seznam) je datová struktura pro uložení množiny prvků, přičemž datové položky jsou navzájem provázány
vzájemnými odkazy (reference, pointery):
* *jednosměrný* spojový seznam - každá datová položka se odkazuje na položku následující,
    ![](https://upload.wikimedia.org/wikipedia/commons/thumb/6/6d/Singly-linked-list.svg/612px-Singly-linked-list.svg.png)
* *obousměrný* spojový seznam - každá datový položka se odkazuje na položku předcházející a položku následující,
    ![](https://media.geeksforgeeks.org/wp-content/uploads/20190326091540/Untitled-Diagram11.jpg) 
* *kruhový* spojový seznam - "první" a "poslední" položka jsou navzájem provázány - seznam se uzavírá do kruhu,

V .NET je připravena hotová třída `LinkedList<T>`, my si však zkusíme **obousměrný** spojový seznam implementovat vlastními silami.
Dávejme do něj třeba textové řetězce.

Spojový seznam chceme naučit následující operace:
1. `AddStart(string item)` - přidání prvku na začátek
1. `Print()` - vypsání seznamu prvků
1. `AddEnd(string item)` - přidání prvku na konec
1. `AddBefore(string itemToAdd, string beforeItem)` - přidání prvku před existující prvek
1. `RemoveFirst()` - odebrání prvního prvku
1. `RemoveLast()` - odebrání posledního prvku
1. `Remove(string item)` - odebrání konkrétního prvku
1. `bool Contains(string item)` - ověření existence prvku
1. `int IndexOf(string item)` - zjištění (první) pozice prvku
1. `InsertAt(string item, int index)` - vložení prvku na konkrétní pozici
1. `int Count()` - vrátí počet prvků v seznamu

...implementujte, co během hodiny stihnete.

## Challenges
1. `Exchange(string item1, string item2)` - výměna prvků v seznamu
1. `Sort()` - seřazení prvků v seznamu

## Inspirace
```csharp
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
```