using System.Text.Json.Serialization;

namespace Blazor_Web3Modal;

/// <summary>
/// The account state for the currently connected wallet
/// </summary>
/// <param name="Address">The connected address, if any</param>
/// <param name="IsConnected">True if the wallet is connected</param>
/// <param name="IsConnecting">True if the wallet is connecting</param>
/// <param name="IsDisconnected">True if the wallet has disconnected</param>
/// <param name="IsReconnecting">True if the wallet is reconnecting</param>
/// <param name="Status">The current status of the wallet connection</param>
public record AccountState(
        [property: JsonPropertyName("address")] string? Address,
        [property: JsonPropertyName("isConnected")] bool IsConnected,
        [property: JsonPropertyName("isConnecting")] bool IsConnecting,
        [property: JsonPropertyName("isDisconnected")] bool IsDisconnected,
        [property: JsonPropertyName("isReconnecting")] bool IsReconnecting,
        [property: JsonPropertyName("status")] string Status
    );

public class AccountChangedEventArgs : EventArgs
{
    /// <summary>
    /// The new account state
    /// </summary>
    public AccountState? AccountState { get; set; }
}