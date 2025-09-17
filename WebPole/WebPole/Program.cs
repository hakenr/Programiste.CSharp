using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using WebPole;

var builder = Host.CreateDefaultBuilder(args)
	.ConfigureWebHostDefaults(webBuilder =>
	{
		webBuilder.UseStartup<Startup>();
	});

builder.Build().Run();
