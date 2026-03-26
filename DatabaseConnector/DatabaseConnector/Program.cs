using System.Data.SqlClient;

using var conn = new SqlConnection("***");
conn.Open();
using var cmd = new SqlCommand("SELECT COUNT(*) FROM Zapisnik", conn);
Console.WriteLine(cmd.ExecuteScalar());

cmd.CommandText = "INSERT INTO Zapisnik(Jmeno, Obsah) VALUES('První pokus', 'Hokus pokus')";
cmd.ExecuteNonQuery();

cmd.CommandText = "SELECT COUNT(*) FROM Zapisnik";
Console.WriteLine(cmd.ExecuteScalar());

using var cmd2 = new SqlCommand("SELECT TOP 10 * FROM Customer", conn);
using (var reader = cmd2.ExecuteReader())
{
	while (reader.Read())
	{
		var firstName = reader["FirstName"];
		var lastName = reader["LastName"];

		Console.WriteLine($"Načten zákazník: {firstName} {lastName}");
	}
}
