using System.Net;
using System;
using Gsemac.Polyfills.Microsoft.Extensions.DependencyInjection;
using Gsemac.IO.Logging;
using Gsemac.Net;
using Gsemac.Net.Cloudflare.FlareSolverr;

static ServiceProvider CreateServiceProvider()
{

    return new ServiceCollection()
        .AddSingleton<ILogger, ConsoleLogger>()
        .AddSingleton<IWebClientFactory, WebClientFactory>()
        .AddSingleton<IFlareSolverrService, FlareSolverrService>()
        .AddSingleton<WebRequestHandler, FlareSolverrChallengeHandler>()
        .BuildServiceProvider();

}

static void Main(string[] args)
{

    //using (ServiceProvider serviceProvider = CreateServiceProvider())
    //{

    //    IWebClientFactory webClientFactory = serviceProvider.GetRequiredService<IWebClientFactory>();

    //    using (IWebClient webClient = webClientFactory.Create())
    //        Console.WriteLine(webClient.DownloadString("https://example.com/"));

    //}

    Console.WriteLine("sadsa");
    Console.ReadKey();

}