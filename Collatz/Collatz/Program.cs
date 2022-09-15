int number = 9;

Console.WriteLine(number);
int step = 0;

while (number != 1)
{
	if (number % 2 == 0)
	{
		number = number / 2;
	}
	else
	{
		number = number * 3 + 1;
	}

	Console.WriteLine(number);

	step++;
}

Console.WriteLine("Number of steps: " + step);