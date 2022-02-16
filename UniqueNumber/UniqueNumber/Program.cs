Console.WriteLine(GetUnique(new int[] { 1, 2, 2, 2 }));     // 1
Console.WriteLine(GetUnique(new int[] { 1, 2, 1, 1 }));     // 2
Console.WriteLine(GetUnique(new int[] { -2, 2, 2, 2 }));    // -2
Console.WriteLine(GetUnique(new int[] { 2, 2, 14, 2, 2 })); // 14
Console.WriteLine(GetUnique(new int[] { 3, 3, 3, 3, 4 }));  // 4

int GetUnique(IEnumerable<int> numbers)
{
	int? prevNum = null;
	int? lastNum = null;
	bool diff = false;
	foreach (int n in numbers)
	{
		if (diff && (n == lastNum))
		{
			return prevNum.Value;
		}
		if (n != lastNum)
		{
			if (lastNum != null)
			{
				diff = true;
			}
			if ((prevNum == lastNum) && lastNum.HasValue)
			{
				return n;
			}
			else if (prevNum == n)
			{
				return lastNum.Value;
			}
			else if ((lastNum == n) && prevNum.HasValue)
			{
				return prevNum.Value;
			}
		}
		prevNum = lastNum;
		lastNum = n;
	}
	return -1;
}
