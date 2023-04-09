using Blazor_Web3Modal.Converters;
using Nethereum.Hex.HexTypes;
using System.Text.Json.Serialization;

namespace Blazor_Web3Modal;

/// <summary>
/// Data for the token
/// </summary>
/// <param name="Address">The token address</param>
/// <param name="Decimals">How many decimals the token employs</param>
/// <param name="Name">Token name</param>
/// <param name="Symbol">Token symbol</param>
/// <param name="TotalSupply">The total supply of the token</param>
public record TokenData([property: JsonPropertyName("address")] string Address,
    [property: JsonPropertyName("decimals")] int Decimals,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("symbol")] string Symbol,
    [property: JsonPropertyName("totalSupply"), JsonConverter(typeof(BigNumberToHexBigIntegerConverter))] HexBigInteger TotalSupply
    );