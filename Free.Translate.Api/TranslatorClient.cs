using System.Text;
using System.Text.Json;

namespace Free.Translate.Api
{
    public class TranslatorClient(IHttpClientFactory httpClientFactory)
    {
        private static readonly JsonSerializerOptions SerializerOptions = new()
        {
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };



        public async Task<string> TranslateAsync(string text, string fromLanguage = "fa", string toLanguage = "en")
        {
            var jsonPayload = JsonSerializer.Serialize(new[] { text }, SerializerOptions);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            string requestUri = $"https://edge.microsoft.com/translate/translatetext?from={fromLanguage}&to={toLanguage}&isEnterpriseClient=true";

            try
            {
                var client = httpClientFactory.CreateClient("translator");

                using var response = await client.PostAsync(requestUri, content);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<List<ApiResponseItem>>(responseBody);

                return result is not null
                    ? string.Concat(result.SelectMany(item => item.Translations.Select(t => t.Text)))
                    : text;
            }
            catch
            {
                return text; // fallback if error
            }
        }
    }
}
