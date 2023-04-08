using System.Text.Json.Serialization;

namespace Blazor_Web3Modal;
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
    public AccountState? AccountState { get; set; }
}