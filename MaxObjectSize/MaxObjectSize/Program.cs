using System.Diagnostics;

long dim1size = 1024 * 1024;
long dim2size = 3 * 1024;

//ulong[] obj = new ulong[size];
var obj = Array.CreateInstance(typeof(Int64), dim1size, dim2size);
Console.WriteLine($"{dim1size * dim2size * 8:n0} bytes"); /* 8 = Int64 */

Console.WriteLine("Writing...");
for (int d = 0; d < dim2size; d++)
{
	for (long i = 0; i < dim1size; i++)
		obj.SetValue(i, i, d);
}


Console .WriteLine(obj.GetValue(153, 0));
Console.WriteLine(obj.GetLongLength(0));
Console.ReadLine();
obj.SetValue(100, 123, 0);
