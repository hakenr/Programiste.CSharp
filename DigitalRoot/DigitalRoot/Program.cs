Console.WriteLine("Zadejte číslo:");
var input = Console.ReadLine();


var output = DigitalRoot1_StringBased(input);
Console.WriteLine($"Digital root: {output}");


var output2 = DigitalRoot2_Numeric(Convert.ToInt64(input));
Console.WriteLine($"Digital root: {output2}");

string DigitalRoot1_StringBased(string number)
{
	if (number.Length == 1)
	{
		return number;
	}
	long sum = 0;
	foreach (char digit in number)
	{
		if (Char.IsDigit(digit))
		{
			sum = sum + (int)Char.GetNumericValue(digit);
		}
	}
	return DigitalRoot1_StringBased(sum.ToString());
}

long DigitalRoot2_Numeric(long number)
{
	if (number < 10)
	{
		return number;
	}
	long sum = 0;
	do
	{
		sum = sum + number % 10;
		number = number / 10;
	}
	while (number > 1);
	sum = sum + number;

	return DigitalRoot2_Numeric(sum);
}
