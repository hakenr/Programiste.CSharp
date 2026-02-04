using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Refit;
using ReqResRefitDemo;
using ReqResRefitDemo.ApiClient;
using ReqResRefitDemo.HttpAuth;
using ReqResRefitDemo.Models;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((ctx, services) =>
    {
        services.AddSingleton<ApiKeyProvider>();
        services.AddSingleton<TokenProvider>();

        services.AddTransient<ReqResHeadersHandler>();

        services.AddRefitClient<IAuthApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://reqres.in"))
            .AddHttpMessageHandler<ReqResHeadersHandler>();

        services.AddRefitClient<IUsersApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://reqres.in"))
            .AddHttpMessageHandler<ReqResHeadersHandler>();

        services.AddTransient<App>();
    })
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddConsole();
    })
    .Build();

await host.Services.GetRequiredService<App>().RunAsync();
