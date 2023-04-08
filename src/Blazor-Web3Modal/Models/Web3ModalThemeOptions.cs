using System.Text.Json.Serialization;

namespace Blazor_Web3Modal;
public class Web3ModalThemeOptions
{
    [JsonPropertyName("themeMode")]
    public ThemeMode ThemeMode { get; set; } = ThemeMode.Light;

    [JsonPropertyName("themeVariables")]
    public Dictionary<string, string>? ThemeVariables { get; set; } = null;
}