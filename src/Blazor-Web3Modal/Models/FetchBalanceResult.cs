using System.Text.Json.Serialization;

namespace Blazor_Web3Modal;
public record FetchBalanceResult(
    [property: JsonPropertyName("decimals")] int Decimals,
    [property: JsonPropertyName("formatted")] string Formatted,
    [property: JsonPropertyName("symbol")] string Symbol,
    [property: JsonPropertyName("value")] HexValue Value
);

public record HexValue(
    [property: JsonPropertyName("type")] string Type,
    [property: JsonPropertyName("hex")] string Hex
);