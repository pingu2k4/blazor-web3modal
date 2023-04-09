using System.Text.Json.Serialization;

namespace Blazor_Web3Modal;

/// <summary>
/// The state of the network for the connected wallet
/// </summary>
/// <param name="Id">Chain ID</param>
/// <param name="Network">Network</param>
/// <param name="Name">Network name</param>
/// <param name="NativeCurrency">Native currency for the network</param>
/// <param name="RpcUrls">Available RPCs for the network</param>
/// <param name="BlockExplorers">Available block explorers for the network</param>
/// <param name="Contracts">Noteworthy contracts for the network</param>
/// <param name="Unsupported">Whether the network is supported or not (based on provided config)</param>
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

/// <summary>
/// Currency Information
/// </summary>
/// <param name="Name">Currency Name</param>
/// <param name="Symbol">Currency Symbol</param>
/// <param name="Decimals">Currency Decimals used</param>
public record Currency(
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("symbol")] string Symbol,
        [property: JsonPropertyName("decimals")] int Decimals
    );

/// <summary>
/// RPC Urls
/// </summary>
/// <param name="Http">The http connection for the rpc</param>
/// <param name="WebSocket">The websocket connection for the rpc</param>
public record RpcUrl(
        [property: JsonPropertyName("http")] IReadOnlyList<string> Http,
        [property: JsonPropertyName("webSocket")] IReadOnlyList<string>? WebSocket
    );

/// <summary>
/// Block explorer
/// </summary>
/// <param name="Name">The block explorer name</param>
/// <param name="Url">The block explorer base URL</param>
public record BlockExplorer(
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("url")] string Url
    );

/// <summary>
/// Notworthy Contracts
/// </summary>
/// <param name="Address">The contract address</param>
/// <param name="BlockCreated">The block in which the contract was created</param>
public record Contract(
        [property: JsonPropertyName("address")] string Address,
        [property: JsonPropertyName("blockCreated")] int? BlockCreated
    );

public class NetworkChangedEventArgs : EventArgs
{
    /// <summary>
    /// The new network state
    /// </summary>
    public NetworkState? NetworkState { get; set; }
}