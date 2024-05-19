Console.WriteLine("Kolik prvků mám seřadit?");
int size = Convert.ToInt32(Console.ReadLine());

// generate input data
int[] inputData = new int[size];
for (int i = 0; i < size; i++)
{
	inputData[i] = Random.Shared.Next(size * 2);
}

PrintArray(inputData);

BubbleSort(inputData);

PrintArray(inputData);


void BubbleSort(int[] data)
{
	int temp;
	for (int j = 0; j <= data.Length - 2; j++)
	{
		for (int i = 0; i <= j; i++)
		{
			if (data[i] > data[i + 1])
			{
				temp = data[i + 1];
				data[i + 1] = data[i];
				data[i] = temp;
			}
			PrintArray(data);
		}
	}
}


void PrintArray(int[] inputData)
{
	for (int i = 0; i < inputData.Length; i++)
	{
		Console.Write($"{inputData[i]}".PadLeft(4));
	}
	Console.WriteLine();
}