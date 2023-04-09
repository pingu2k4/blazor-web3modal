using System.Text.Json.Serialization;

namespace Blazor_Web3Modal;

/// <summary>
/// Data for the fees on the current network conditions
/// </summary>
/// <param name="GasPrice">The current gas price</param>
/// <param name="MaxFeePerGas">The current max fee per gas</param>
/// <param name="MaxPriorityFeePerGas">The current max priority fee per gas</param>
public record FeeData([property: JsonPropertyName("gasPrice")] decimal GasPrice,
                      [property: JsonPropertyName("maxFeePerGas")] decimal MaxFeePerGas,
                      [property: JsonPropertyName("maxPriorityFeePerGas")] decimal MaxPriorityFeePerGas);