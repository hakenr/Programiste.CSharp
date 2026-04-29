// 1) Naivní řešení – sdílená Queue<int> bez synchronizace.
//
// POZOR: tato varianta ZÁMĚRNĚ obsahuje chyby a slouží jen k demonstraci problémů:
//   - race condition při souběžném přístupu (Queue<T> není thread-safe)
//   - busy waiting konzumenta (vytěžuje CPU)
//   - může dojít k pádu, zacyklení nebo vynechání prvku

const int N = 20;

var queue = new Queue<int>();

var producer = new Thread(() =>
{
	for (int i = 1; i <= N; i++)
	{
		queue.Enqueue(i); // bez zámku – race condition
		Console.WriteLine($"[P] enqueued {i}");
		Thread.Sleep(50);
	}
});

var consumer = new Thread(() =>
{
	int processed = 0;
	while (processed < N)
	{
		// Aktivní čekání (busy waiting) – consumer pálí CPU dokola.
		if (queue.Count > 0)
		{
			int item = queue.Dequeue(); // bez zámku – race condition
			Console.WriteLine($"[C] dequeued {item}");
			processed++;
		}
	}
});

producer.Start();
consumer.Start();
producer.Join();
consumer.Join();

Console.WriteLine("Hotovo (naivní).");
