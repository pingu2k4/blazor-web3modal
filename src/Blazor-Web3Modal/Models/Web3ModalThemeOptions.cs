using System.Text.Json.Serialization;

namespace Blazor_Web3Modal;
public class Web3ModalThemeOptions
{
    /// <summary>
    /// Puts Web3Modal into dark or light mode
    /// </summary>
    [JsonPropertyName("themeMode")]
    public ThemeMode ThemeMode { get; set; } = ThemeMode.Light;

    /// <summary>
    /// Allows to override Web3Modal's css styles. See <see href="https://docs.walletconnect.com/2.0/web3modal/theming">theming</see> for all available options
    /// </summary>
    [JsonPropertyName("themeVariables")]
    public Dictionary<string, string>? ThemeVariables { get; set; } = null;
}