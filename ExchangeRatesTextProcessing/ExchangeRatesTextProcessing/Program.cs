Console.OutputEncoding = System.Text.Encoding.Unicode; 

const string url = "https://www.cnb.cz/cs/financni-trhy/devizovy-trh/kurzy-devizoveho-trhu/kurzy-devizoveho-trhu/denni_kurz.txt";

using var httpClient = new HttpClient();
string cnbWebContent = await httpClient.GetStringAsync(url);

var lines = cnbWebContent.Split('\n');
for (int i = 2; i < lines.Length; i++)
{
	var line = lines[i];
	if (!line.Contains("|"))
	{
		continue;
	}

	var segments = line.Split('|');

	var country = segments[0];
	var currencyName = segments[1];
	var amount = Convert.ToDecimal(segments[2]);
	var currencyCode = segments[3];
	var exchangeRate = segments[4];

	Console.Write(country.PadRight(25));
	Console.Write(currencyName.PadRight(15));
	Console.Write(String.Format("{0:n2} ", exchangeRate).PadLeft(10));
	Console.Write($"CZK / {amount} {currencyCode}");
	Console.WriteLine();
}