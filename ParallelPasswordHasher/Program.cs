using System.Collections.Concurrent;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

const int PasswordCount = 2_000;   // počet hesel (můžete měnit)
const int Iterations = 150_000;  // PBKDF2 iterace (100k–300k dle HW)

// Generátor vstupů (in-memory)
static List<string> GeneratePasswords(int count)
	=> Enumerable.Range(0, count).Select(i => $"pass{i:D6}").ToList();

// Fixní salt jen pro cvičení (deterministické výsledky)
byte[] Salt = Encoding.UTF8.GetBytes("salt-01");

// PBKDF2-SHA256 (CPU-bound)
byte[] HashPbkdf2(string password, int iterations) =>
	Rfc2898DeriveBytes.Pbkdf2(
		password: Encoding.UTF8.GetBytes(password),
		salt: Salt,
		iterations: iterations,
		hashAlgorithm: HashAlgorithmName.SHA256,
		outputLength: 32);

// A) SEKVENCE (baseline) – HOTOVO
IDictionary<string, string> HashAllSequential(IReadOnlyList<string> passwords, int iterations)
{
	var dict = new Dictionary<string, string>();
	foreach (var p in passwords)
	{
		dict[p] = Convert.ToBase64String(HashPbkdf2(p, iterations));
	}
	return dict;
}

// B) PARALELNĚ – Threads (DOPLŇTE)
IDictionary<string, string> HashAllThreads(IReadOnlyList<string> passwords, int iterations)
{
	// TODO
	return new Dictionary<string, string>();
}

// C) PARALELNĚ – Tasks (DOPLŇTE)
async Task<IDictionary<string, string>> HashAllTasksAsync(IReadOnlyList<string> passwords, int iterations)
{
	// TODO
	return new Dictionary<string, string>();
}

// D) PARALELNĚ – Parallel.ForEach (DOPLŇTE)
Dictionary<string, string> HashAllParallel(IReadOnlyList<string> passwords, int iterations)
{
	// TODO
	return new Dictionary<string, string>();
}

// DEMO & měření – start
var passwords = GeneratePasswords(PasswordCount);
Console.WriteLine($"Passwords: {passwords.Count}, Iterations: {Iterations} (CPU Cores: {Environment.ProcessorCount})\n");

var sw = Stopwatch.StartNew();

// 1) Sekvenčně
sw.Restart();
var seq = HashAllSequential(passwords, Iterations);
sw.Stop();
var tSeq = sw.Elapsed.TotalMilliseconds;
Console.WriteLine($"[SEQ]       {tSeq,8:F0} ms");

//2) Threads
 sw.Restart();
var th = HashAllThreads(passwords, Iterations);
sw.Stop();
var tTh = sw.Elapsed.TotalMilliseconds;
Console.WriteLine($"[THREADS]   {tTh,8:F0} ms   (speedup {tSeq / tTh:0.00}×)");

//3) Tasks
 sw.Restart();
var ta = await HashAllTasksAsync(passwords, Iterations);
sw.Stop();
var tTa = sw.Elapsed.TotalMilliseconds;
Console.WriteLine($"[TASKS]     {tTa,8:F0} ms   (speedup {tSeq / tTa:0.00}×)");

//4) Parallel.ForEach
 sw.Restart();
var pf = HashAllParallel(passwords, Iterations);
sw.Stop();
var tPf = sw.Elapsed.TotalMilliseconds;
Console.WriteLine($"[PARALLEL]  {tPf,8:F0} ms   (speedup {tSeq / tPf:0.00}×)");

//5) Kontroly shody
 Console.WriteLine();
if (th != null)
{
	bool sameTh = seq.Count == th.Count && seq.All(kv => th.TryGetValue(kv.Key, out var v) && v == kv.Value);
	Console.WriteLine($"Match (SEQ vs THREADS):   {sameTh}");
}
if (ta != null)
{
	bool sameTa = seq.Count == ta.Count && seq.All(kv => ta.TryGetValue(kv.Key, out var v) && v == kv.Value);
	Console.WriteLine($"Match (SEQ vs TASKS):     {sameTa}");
}
if (pf != null)
{
	bool samePf = seq.Count == pf.Count && seq.All(kv => pf.TryGetValue(kv.Key, out var v) && v == kv.Value);
	Console.WriteLine($"Match (SEQ vs PARALLEL):  {samePf}");
}

Console.WriteLine("\nHotovo.");
