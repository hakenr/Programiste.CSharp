// 3) Monitor.Wait / Monitor.Pulse – odstranění busy waitingu.
//
// Consumer se uspí (Monitor.Wait), když je fronta prázdná, a producer ho probudí
// (Monitor.Pulse) po vložení nového prvku. Žádné aktivní čekání.
//
// Proč WHILE místo IF? Po probuzení musíme znovu ověřit podmínku:
//   - může nastat tzv. spurious wakeup,
//   - při více konzumentech mohl být prvek mezitím odebrán jiným konzumentem.

const int N = 20;

var queue = new Queue<int>();
var gate = new object();
bool finished = false;

var producer = new Thread(() =>
{
	for (int i = 1; i <= N; i++)
	{
		lock (gate)
		{
			queue.Enqueue(i);
			Monitor.Pulse(gate); // probudíme jednoho čekajícího konzumenta
		}
		Console.WriteLine($"[P] enqueued {i}");
		Thread.Sleep(50);
	}

	// Signalizace ukončení – consumer se probudí, vidí finished a skončí.
	lock (gate)
	{
		finished = true;
		Monitor.PulseAll(gate);
	}
});

var consumer = new Thread(() =>
{
	while (true)
	{
		int item;
		lock (gate)
		{
			while (queue.Count == 0 && !finished)
			{
				Monitor.Wait(gate); // uvolní zámek a usne, dokud nepřijde Pulse
			}

			if (queue.Count == 0 && finished)
			{
				return;
			}

			item = queue.Dequeue();
		}

		Console.WriteLine($"[C] dequeued {item}");
	}
});

producer.Start();
consumer.Start();
producer.Join();
consumer.Join();

Console.WriteLine("Hotovo (Monitor.Wait/Pulse).");
