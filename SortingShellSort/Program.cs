int[] data = { 64, 34, 25, 12, 22, 11, 90, 88, 42, 19 };

PrintArray(data);

ShellSort(data);

PrintArray(data);

void ShellSort(int[] array)
{
	int n = array.Length;
	int gap = n / 2;

	while (gap > 0)
	{
		for (int i = gap; i < n; i++)
		{
			int temp = array[i];
			int j = i;

			while (j >= gap && array[j - gap] > temp)
			{
				array[j] = array[j - gap];
				j -= gap;
			}

			array[j] = temp;
		}

		gap /= 2;
	}
}

void PrintArray(int[] array)
{
	Console.WriteLine(string.Join(", ", array));
}