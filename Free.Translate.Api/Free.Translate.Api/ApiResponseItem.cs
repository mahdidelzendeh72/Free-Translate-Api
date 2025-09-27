using System.Text.Json.Serialization;

namespace Free.Translate.Api;

public class ApiResponseItem
{
    [JsonPropertyName("translations")]
    public List<TranslationItem> Translations { get; set; } = new();

    public class TranslationItem
    {
        [JsonPropertyName("text")]
        public string Text { get; set; } = string.Empty;

        [JsonPropertyName("to")]
        public string To { get; set; } = string.Empty;
    }
}
