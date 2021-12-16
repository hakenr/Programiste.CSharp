using System;
using System.IO;

var outputFile = "output.txt";
if (File.Exists(outputFile))
{
	File.Delete(outputFile);
}

var searchDirectory = "D:\\Development\\Programiste.CSharp";

var files = Directory.EnumerateFiles(searchDirectory, "*.cs", SearchOption.AllDirectories);
string directory = null;

foreach (var fileName in files)
{
	var fileExtension = Path.GetExtension(fileName);

	if (Path.GetDirectoryName(fileName) != directory)
	{
		directory = Path.GetDirectoryName(fileName);
		File.AppendAllText(outputFile, Environment.NewLine + directory + Environment.NewLine);
	}

	if (fileExtension == ".cs")
	{
		var fileContents = File.ReadAllText(fileName);
		var fileOutput = String.Empty;
		var lines = fileContents.Split(Environment.NewLine);

		for (int lineNumber = 0; lineNumber < lines.Length; lineNumber++)
		{
			if (lines[lineNumber].Contains("Console."))
			{
				fileOutput += $"\t Line {lineNumber + 1}: {lines[lineNumber].Trim()}" + Environment.NewLine;
			}
		}

		if (!String.IsNullOrWhiteSpace(fileOutput))
		{
			File.AppendAllText(outputFile, Path.GetFileName(fileName) + Environment.NewLine);
			File.AppendAllText(outputFile, fileOutput);
		}


	}
}

Console.OutputEncoding = System.Text.Encoding.Unicode;
Console.Write(File.ReadAllText(outputFile));