long counter = 0;

void MyMethod()
{
	// increase stack-frame size
	//Int64 a, b, c, d, e, f, g, h, i, j, k, l, m, n, o, p;

	counter++;

	Console.WriteLine(counter.ToString());

	MyMethod();
}

MyMethod();

// increase stack size
//var thread = new Thread(MyMethod, int.MaxValue);
//thread.Start();
//thread.Join();