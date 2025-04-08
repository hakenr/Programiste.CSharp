int[] data = Enumerable.Range(1, 20).Select(i => Random.Shared.Next(100)).ToArray();

Console.WriteLine(string.Join(", ", data));

QuickSort(data);

Console.WriteLine(string.Join(", ", data));


void QuickSort(int[] array)
{
	QuickSortCore(array, 0, array.Length - 1);
}

void QuickSortCore(int[] array, int left, int right)
{
	if (left >= right)
	{
		return;
	}

	int pivotIndex = Partition(array, left, right);

	QuickSortCore(array, left, pivotIndex - 1);
	QuickSortCore(array, pivotIndex + 1, right);
}

int Partition(int[] array, int left, int right)
{
	int pivot = array[right];  // One of strategies
	int i = left - 1;

	for (int j = left; j < right; j++)
	{
		if (array[j] < pivot)
		{
			i++;
			Swap(array, i, j);
		}
	}

	Swap(array, i + 1, right);
	return i + 1;
}

void Swap(int[] array, int i, int j)
{
	if (i != j)
	{
		(array[i], array[j]) = (array[j], array[i]);
	}
}
