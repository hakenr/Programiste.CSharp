const int size = 10;

List<KeyValuePair<int, string>>[] hashTable = new List<KeyValuePair<int, string>>[size];

for (int i = 0; i < size; i++)
{
	hashTable[i] = new List<KeyValuePair<int, string>>();
}

int GetIndex(int key) => Math.Abs(key) % size;

void Insert(int key, string value)
{
	int index = GetIndex(key);
	hashTable[index].Add(new KeyValuePair<int, string>(key, value));
}

string Search(int key)
{
	int index = GetIndex(key);
	foreach (var pair in hashTable[index])
	{
		if (pair.Key == key)
			return pair.Value;
	}
	return null;
}

bool Delete(int key)
{
	int index = GetIndex(key);
	var bucket = hashTable[index];
	for (int i = 0; i < bucket.Count; i++)
	{
		if (bucket[i].Key == key)
		{
			bucket.RemoveAt(i);
			return true;
		}
	}
	return false;
}

// Ukázkové použití
Insert(101, "A sweet red fruit");
Insert(202, "A long yellow fruit");
Insert(303, "A green vegetable");

Console.WriteLine(Search(101)); // A sweet red fruit
Console.WriteLine(Search(202)); // A long yellow fruit

Delete(202);
Console.WriteLine(Search(202)); // null
