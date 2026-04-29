// 2) Synchronizace pomocí lock.
//
// Přístup ke sdílené frontě je atomický a thread-safe.
// Stále ale zůstává busy waiting – consumer aktivně točí smyčku, dokud se neobjeví prvek.

const int N = 20;

var queue = new Queue<int>();
var gate = new object();

var producer = new Thread(() =>
{
	for (int i = 1; i <= N; i++)
	{
		lock (gate)
		{
			queue.Enqueue(i);
		}
		Console.WriteLine($"[P] enqueued {i}");
		Thread.Sleep(50);
	}
});

var consumer = new Thread(() =>
{
	int processed = 0;
	while (processed < N)
	{
		int item = 0;
		bool got = false;

		lock (gate)
		{
			if (queue.Count > 0)
			{
				item = queue.Dequeue();
				got = true;
			}
		}

		if (got)
		{
			Console.WriteLine($"[C] dequeued {item}");
			processed++;
		}
		// Pokud nic nebylo, smyčka se točí dál – stále busy waiting.
	}
});

producer.Start();
consumer.Start();
producer.Join();
consumer.Join();

Console.WriteLine("Hotovo (lock).");
