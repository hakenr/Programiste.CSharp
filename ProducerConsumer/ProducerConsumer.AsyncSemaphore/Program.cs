// 4) Async consumer pomocí SemaphoreSlim.WaitAsync().
//
// Rozdíl oproti Monitor.Wait:
//   - Monitor.Wait BLOKUJE celé vlákno (vlákno z thread-poolu sedí a nic nedělá).
//   - SemaphoreSlim.WaitAsync() vrátí Task – vlákno se uvolní pro jinou práci
//     a continuation se naplánuje až při Release().
//
// Hodí se zejména v serverových/UI scénářích, kde je málo vláken a mnoho čekajících.

const int N = 20;

var queue = new Queue<int>();
var gate = new object();
var available = new SemaphoreSlim(0); // počet prvků, které čekají ve frontě
bool finished = false;

var producerTask = Task.Run(async () =>
{
	for (int i = 1; i <= N; i++)
	{
		lock (gate)
		{
			queue.Enqueue(i);
		}
		Console.WriteLine($"[P] enqueued {i}");
		available.Release(); // signál: máme nový prvek
		await Task.Delay(50);
	}

	finished = true;
	available.Release(); // probudíme consumera, aby si všiml ukončení
});

var consumerTask = Task.Run(async () =>
{
	while (true)
	{
		await available.WaitAsync(); // ASYNC čekání – neblokuje vlákno

		int item;
		lock (gate)
		{
			if (queue.Count == 0)
			{
				if (finished) return;
				continue;
			}
			item = queue.Dequeue();
		}

		Console.WriteLine($"[C] dequeued {item}");
	}
});

await Task.WhenAll(producerTask, consumerTask);

Console.WriteLine("Hotovo (async + SemaphoreSlim).");
