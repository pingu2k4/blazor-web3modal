using Blazor_Web3Modal.Converters;
using Nethereum.Hex.HexTypes;
using System.Text.Json.Serialization;

namespace Blazor_Web3Modal;

/// <summary>
/// The result of a GetBalance request
/// </summary>
/// <param name="Decimals">The number of decimals used</param>
/// <param name="Formatted">The string formatted representation of the balance</param>
/// <param name="Symbol">The currency symbol used</param>
/// <param name="Value">The value for the balance</param>
public record FetchBalanceResult(
    [property: JsonPropertyName("decimals")] int Decimals,
    [property: JsonPropertyName("formatted")] string Formatted,
    [property: JsonPropertyName("symbol")] string Symbol,
    [property: JsonPropertyName("value"), JsonConverter(typeof(BigNumberToHexBigIntegerConverter))] HexBigInteger Value
);