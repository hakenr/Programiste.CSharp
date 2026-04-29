// 5) Bonus: System.Threading.Channels – moderní řešení.
//
// Channel<T> je hotový thread-safe asynchronní buffer. Řeší za nás:
//   - synchronizaci přístupu ke sdílené frontě
//   - asynchronní čekání (WaitToReadAsync / ReadAllAsync)
//   - signalizaci ukončení (Writer.Complete)
//   - volitelně omezenou kapacitu a strategii backpressure (Channel.CreateBounded)

using System.Threading.Channels;

const int N = 20;

var channel = Channel.CreateUnbounded<int>();

var producerTask = Task.Run(async () =>
{
	for (int i = 1; i <= N; i++)
	{
		await channel.Writer.WriteAsync(i);
		Console.WriteLine($"[P] wrote {i}");
		await Task.Delay(50);
	}
	channel.Writer.Complete(); // consumer pozná konec – await foreach skončí sám
});

var consumerTask = Task.Run(async () =>
{
	await foreach (var item in channel.Reader.ReadAllAsync())
	{
		Console.WriteLine($"[C] read {item}");
	}
});

await Task.WhenAll(producerTask, consumerTask);

Console.WriteLine("Hotovo (Channel<T>).");
