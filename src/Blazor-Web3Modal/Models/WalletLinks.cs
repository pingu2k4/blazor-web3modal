using System.Text.Json.Serialization;

namespace Blazor_Web3Modal;
public class WalletLinks
{
    [JsonPropertyName("native")]
    public required string Native { get; set; }

    [JsonPropertyName("universal")]
    public string? Universal { get; set; }
}