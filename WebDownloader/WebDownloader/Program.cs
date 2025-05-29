using System.Text.RegularExpressions;

HttpClient client = new HttpClient();
string stranka = client.GetStringAsync("https://www.mensagymnazium.cz/cs/kontakty").Result;

Regex re = new Regex(@"<span class=""name"">(?<jmeno>.*)</span>");
foreach (Match match in re.Matches(stranka))
{
	Console.WriteLine(match.Groups["jmeno"]);
}

//Console.WriteLine(stranka);

//			HttpRequestMessage request = new HttpRequestMessage();
//			request.Method = HttpMethod.Get;
//			request.Headers.Add("User-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.61 Safari/537.36 Edg/83.0.478.44");
//			request.RequestUri = new Uri("https://www.mensagymnazium.cz/cs/kontakty");

//			using var response2 = client.SendAsync(request).Result;
//			var statusCode = response2.StatusCode;
//// 			var header = response2.Headers.GetValues("Content-Type");
//			var stranka = response2.Content.ReadAsStringAsync().Result;