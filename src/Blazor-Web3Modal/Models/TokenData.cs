using Blazor_Web3Modal.Converters;
using Nethereum.Hex.HexTypes;
using System.Text.Json.Serialization;

namespace Blazor_Web3Modal;
public record TokenData([property: JsonPropertyName("address")] string Address,
    [property: JsonPropertyName("decimals")] int Decimals,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("symbol")] string Symbol,
    [property: JsonPropertyName("totalSupply"), JsonConverter(typeof(BigNumberToHexBigIntegerConverter))] HexBigInteger TotalSupply
    );