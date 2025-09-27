
# 📝 Free Translator Client (C# .NET)

A lightweight, fast  **C# client** for free translator endpoint.

---

## 🚀 Features

* Translate text between languages API.
* Built with `IHttpClientFactory` for best practices (connection pooling, DI-friendly).
* Clean architecture: injectable, testable, and easy to extend.
* Support `html` formatting.

---

## 📂 Project Structure

```
/Sample.Free.Translate.Api
  Program.cs
/Free.Translate.Api
  TranslatorClient.cs

```

---

## ⚡ Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/your-username/free-translator-client.git
cd free-translator-client
```

### 2. Build the Project

```bash
dotnet build
```

### 3. Run the Console App

```bash
dotnet run --project Sample.Free.Translate.Api
```

You’ll be prompted to enter text in **Persian (fa)**, and it will translate to **English (en)**:

```
input:"سلام. من مهدی هستم و از آشنایی با شما خوشبختم"

Translated: Hello. I am Mehdi and I am happy to meet you
```

---


## 💻 Example Usage (in Console App)

```csharp
using Free.Translate.Api;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net.Http.Headers;

class Program
{
    static async Task Main(string[] args)
    {
        using var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
            {
                services.AddHttpClient("translator", client =>
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
                    client.DefaultRequestHeaders.AcceptLanguage.ParseAdd("en-US,en;q=0.9");
                });

                services.AddSingleton<TranslatorClientV2>();
            })
            .Build();

        var translator = host.Services.GetRequiredService<TranslatorClientV2>();

        Console.WriteLine("Enter text (fa -> en): ");
        string input = Console.ReadLine() ?? "سلام";

        string translated = await translator.TranslateAsync(input, "en");

        Console.WriteLine($"Translated: {translated}");
    }
}
```

---

## ⚙️ Configuration

* Default source language: **fa** (Persian).
* To change, modify the `TranslateAsync` call:

```csharp
string translated = await translator.TranslateAsync("Bonjour", "en");
```

---


## 📄 License

MIT License – feel free to use, modify, and distribute.


