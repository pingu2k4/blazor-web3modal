using System.Text.Json.Serialization;

namespace Blazor_Web3Modal;
public class Web3ModalOptions
{
    [JsonPropertyName("projectId")]
    public required string ProjectId { get; set; }

    [JsonPropertyName("selectedChains")]
    public List<Chain> Chains { get; set; } = new List<Chain>() { Chain.Mainnet };

    [JsonPropertyName("autoConnect")]
    public bool AutoConnect { get; set; }

    [JsonPropertyName("themeOptions")]
    public Web3ModalThemeOptions ThemeOptions { get; } = new Web3ModalThemeOptions();

    [JsonPropertyName("mobileWallets")]
    public List<WalletDetails>? MobileWallets { get; set; }

    [JsonPropertyName("desktopWallets")]
    public List<WalletDetails>? DesktopWallets { get; set; }

    [JsonPropertyName("walletImages")]
    public Dictionary<string, string>? WalletImages { get; set; }

    [JsonPropertyName("chainImages")]
    public Dictionary<string, string>? ChainImages { get; set; }

    [JsonPropertyName("tokenImages")]
    public Dictionary<string, string>? TokenImages { get; set; }

    [JsonPropertyName("explorerAllowList")]
    public List<string>? ExplorerAllowList { get; set; }

    [JsonPropertyName("explorerDenyList")]
    public List<string>? ExplorerDenyList { get; set; }

    [JsonPropertyName("termsOfServiceUrl")]
    public string? TermsOfServiceUrl { get; set; }

    [JsonPropertyName("privacyPolicyUrl")]
    public string? PrivacyPolicyUrl { get; set; }

    [JsonPropertyName("enableNetworkView")]
    public bool EnableNetworkView { get; set; }

    [JsonPropertyName("enableAccountView")]
    public bool EnableAccountView { get; set; } = true;

    [JsonPropertyName("enableExplorer")]
    public bool EnableExplorer { get; set; } = true;
}