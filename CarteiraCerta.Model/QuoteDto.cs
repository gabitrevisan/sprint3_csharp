using System.Text.Json.Serialization;

namespace CarteiraCerta.Model
{
    public class QuoteDto
    {
        [JsonPropertyName("c")]
        public decimal CurrentPrice { get; set; }

        [JsonPropertyName("d")]
        public decimal Change { get; set; }

        [JsonPropertyName("dp")]
        public decimal PercentChange { get; set; }

        [JsonPropertyName("h")]
        public decimal HighPriceOfDay { get; set; }

        [JsonPropertyName("l")]
        public decimal LowPriceOfDay { get; set; }

        [JsonPropertyName("o")]
        public decimal OpenPriceOfDay { get; set; }
    }
}