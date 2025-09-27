using Free.Translate.Api;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net.Http.Headers;

// Entry point
class Program
{
    static async Task Main(string[] args)
    {
        // Build DI container with IHttpClientFactory + TranslatorClientV2
        using var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
            {
                // Register HttpClient with headers
                services.AddHttpClient("translator", client =>
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
                    client.DefaultRequestHeaders.AcceptLanguage.ParseAdd("en-US,en;q=0.9");
                    client.DefaultRequestHeaders.UserAgent.ParseAdd(
                        "Mozilla/5.0 (Windows NT 10.0; Win64; x64) " +
                        "AppleWebKit/537.36 (KHTML, like Gecko) " +
                        "Chrome/138.0.0.0 Safari/537.36 Edg/138.0.0.0");

                    // extra headers if needed
                    client.DefaultRequestHeaders.Add("origin", "null");
                    client.DefaultRequestHeaders.Add("priority", "u=1, i");
                });

                // Register our translator service
                services.AddSingleton<TranslatorClient>();
            })
            .Build();

        // Resolve service
        var translator = host.Services.GetRequiredService<TranslatorClient>();

        string input = "سلام. من مهدی هستم و از آشنایی با شما خوشبختم";

        string translated = await translator.TranslateAsync(input);

        Console.WriteLine($"Translated: {translated}");
    }
}
