using System.Text.Json.Serialization;

namespace Blazor_Web3Modal;
public record NetworkState(
        [property: JsonPropertyName("id")] int Id,
        [property: JsonPropertyName("network")] string Network,
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("nativeCurrency")] Currency NativeCurrency,
        [property: JsonPropertyName("rpcUrls")] IReadOnlyDictionary<string, RpcUrl> RpcUrls,
        [property: JsonPropertyName("blockExplorers")] IReadOnlyDictionary<string, BlockExplorer> BlockExplorers,
        [property: JsonPropertyName("contracts")] IReadOnlyDictionary<string, Contract>? Contracts,
        [property: JsonPropertyName("unsupported")] bool Unsupported
    );

public record Currency(
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("symbol")] string Symbol,
        [property: JsonPropertyName("decimals")] int Decimals
    );

public record RpcUrl(
        [property: JsonPropertyName("http")] IReadOnlyList<string> Http,
        [property: JsonPropertyName("webSocket")] IReadOnlyList<string>? WebSocket
    );

public record BlockExplorer(
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("url")] string Url
    );

public record Contract(
        [property: JsonPropertyName("address")] string Address,
        [property: JsonPropertyName("blockCreated")] int? BlockCreated
    );

public class NetworkChangedEventArgs : EventArgs
{
    public NetworkState? NetworkState { get; set; }
}