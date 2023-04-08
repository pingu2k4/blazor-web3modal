using System.Text.Json.Serialization;

namespace Blazor_Web3Modal;
public record FeeData([property: JsonPropertyName("gasPrice")] decimal GasPrice,
                      [property: JsonPropertyName("maxFeePerGas")] decimal MaxFeePerGas,
                      [property: JsonPropertyName("maxPriorityFeePerGas")] decimal MaxPriorityFeePerGas);