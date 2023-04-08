using System.Text.Json.Serialization;

namespace Blazor_Web3Modal;
public class WalletDetails
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("links")]
    public required WalletLinks Links { get; set; }
}