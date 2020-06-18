using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Data.SqlClient;

namespace SqlInjectionDemo
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Username:");
			var username = Console.ReadLine();

			Console.WriteLine("Password:");
			var password = Console.ReadLine();

			// SELECT UserID FROM User WHERE Username='pepa' AND Password = 'hesllo'
			string cmdText = "SELECT UserId FROM [User] WHERE Username=@username AND Password=@password";
			Console.WriteLine(cmdText);


			using var conn = new SqlConnection("Server=rhdemosql.database.windows.net;Database=AdventureWorksLT;User Id=PrgStudent;Password=Karel je borec.;");
			conn.Open();
			using var cmd = new SqlCommand(cmdText, conn);
			cmd.Parameters.AddWithValue("username", username);
			cmd.Parameters.AddWithValue("password", password);

			var userID = cmd.ExecuteScalar();
			Console.Write("ID přihlášeného uživatele:");
			Console.WriteLine(userID);
		}
	}
}
