using System.Text.Json.Serialization;

namespace Blazor_Web3Modal;

/// <summary>
/// Options for Web3Modal. See: <see href="https://docs.walletconnect.com/2.0/web3modal/options"/>
/// </summary>
public class Web3ModalOptions
{
    /// <summary>
    /// Required. Your project’s unique identifier that can be obtained at <see href="https://cloud.walletconnect.com"/>. Enables following functionalities within Web3Modal: wallet and chain logos, optional WalletConnect RPC, support for all wallets from <see href="https://explorer.walletconnect.com"/> and WalletConnect v2 support
    /// <br /><see href="https://docs.walletconnect.com/2.0/web3modal/options#projectid-required"/>
    /// </summary>
    [JsonPropertyName("projectId")]
    public required string ProjectId { get; set; }

    /// <summary>
    /// All chains you want to add support for with Web3Modal. Defaults to <see cref="Chain.Mainnet"/>
    /// </summary>
    [JsonPropertyName("selectedChains")]
    public List<Chain> Chains { get; set; } = new List<Chain>() { Chain.Mainnet };

    /// <summary>
    /// Select whether Web3Modal should auto connect as soon as it is configured
    /// </summary>
    [JsonPropertyName("autoConnect")]
    public bool AutoConnect { get; set; }

    /// <summary>
    /// Theming options
    /// <br /><see href="https://docs.walletconnect.com/2.0/web3modal/theming"/>
    /// </summary>
    [JsonPropertyName("themeOptions")]
    public Web3ModalThemeOptions ThemeOptions { get; } = new Web3ModalThemeOptions();

    /// <summary>
    /// You can define an array of custom mobile wallets. Note: you will also need to add appropriate wallet images in <see cref="WalletImages"/>. Native link represents deeplinking url like rainbow:// and Universal link represent webpage link that can redirect to the app or fallback page
    /// <br /><see href="https://docs.walletconnect.com/2.0/web3modal/options#mobilewallets-optional"/>
    /// </summary>
    [JsonPropertyName("mobileWallets")]
    public List<WalletDetails>? MobileWallets { get; set; }

    /// <summary>
    /// You can define an array of custom desktop or web based wallets. Note: you will also need to add appropriate wallet images in <see cref="WalletImages"/>. Native link represents deeplinking url like ledgerlive:// and Universal link represents webpage link that can redirect to the app or fallback page
    /// <br /><see href="https://docs.walletconnect.com/2.0/web3modal/options#desktopwallets-optional"/>
    /// </summary>
    [JsonPropertyName("desktopWallets")]
    public List<WalletDetails>? DesktopWallets { get; set; }

    /// <summary>
    /// Array of wallet id's and their logo mappings. This will override default logos. Id's in this case can be: <see href="https://explorer.walletconnect.com"/> id's, wallet id's you provided in <see cref="MobileWallets"/> or <see cref="DesktopWallets"/>
    /// <br /><see href="https://docs.walletconnect.com/2.0/web3modal/options#walletimages-optional"/>
    /// </summary>
    [JsonPropertyName("walletImages")]
    public Dictionary<string, string>? WalletImages { get; set; }

    /// <summary>
    /// Array of chain id's and their logo mappings. This will override default logos. You can find detailed chain data at <see href="https://chainlist.org"/>
    /// <br /><see href="https://docs.walletconnect.com/2.0/web3modal/options#chainimages-optional"/>
    /// </summary>
    [JsonPropertyName("chainImages")]
    public Dictionary<string, string>? ChainImages { get; set; }

    /// <summary>
    /// Array of token symbols and their logo mappings
    /// <br /><see href="https://docs.walletconnect.com/2.0/web3modal/options#tokenimages-optional"/>
    /// </summary>
    [JsonPropertyName("tokenImages")]
    public Dictionary<string, string>? TokenImages { get; set; }

    /// <summary>
    /// Some wallet data in Web3Modal is fetched from our explorer api <see href="https://explorer.walletconnect.com"/>. You can define an allow list only for the wallets that you want to be shown. Allow list is an array of wallet id's. You can get / copy these id's from the explorer link mentioned before
    /// <br /><see href="https://docs.walletconnect.com/2.0/web3modal/options#explorerallowlist-optional"/>
    /// </summary>
    [JsonPropertyName("explorerAllowList")]
    public List<string>? ExplorerAllowList { get; set; }

    /// <summary>
    /// Some wallet data in Web3Modal is fetched from our explorer api <see href="https://explorer.walletconnect.com"/>. You can define a deny list for the wallets that you want to be excluded. Deny list is an array of wallet id's. You can get / copy these id's from the explorer link mentioned before
    /// <br /><see href="https://docs.walletconnect.com/2.0/web3modal/options#explorerdenylist-optional"/>
    /// </summary>
    [JsonPropertyName("explorerDenyList")]
    public List<string>? ExplorerDenyList { get; set; }

    /// <summary>
    /// String URL to your terms of service page, if specified will append special "legal info" footer to the modal
    /// <br /><see href="https://docs.walletconnect.com/2.0/web3modal/options#termsofserviceurl-optional"/>
    /// </summary>
    [JsonPropertyName("termsOfServiceUrl")]
    public string? TermsOfServiceUrl { get; set; }

    /// <summary>
    /// String URL to your privacy policy page, if specified will append special "legal info" footer to the modal
    /// <br /><see href="https://docs.walletconnect.com/2.0/web3modal/options#privacypolicyurl-optional"/>
    /// </summary>
    [JsonPropertyName("privacyPolicyUrl")]
    public string? PrivacyPolicyUrl { get; set; }

    /// <summary>
    /// If more than 1 chain was provided in modal or wagmi configuration, users will be show network selection view before selecting a wallet. This option can enable or disable this behavior
    /// <br /><see href="https://docs.walletconnect.com/2.0/web3modal/options#enablenetworkview-optional"/>
    /// </summary>
    [JsonPropertyName("enableNetworkView")]
    public bool EnableNetworkView { get; set; }

    /// <summary>
    /// Option to enable or disable the modal's account view
    /// <br /><see href="https://docs.walletconnect.com/2.0/web3modal/options#enableaccountview-optional"/>
    /// </summary>
    [JsonPropertyName("enableAccountView")]
    public bool EnableAccountView { get; set; } = true;

    /// <summary>
    /// Option to enable or disable wallet fetching from <see href="https://explorer.walletconnect.com"/>
    /// <br /><see href="https://docs.walletconnect.com/2.0/web3modal/options#enableexplorer-optional"/>
    /// </summary>
    [JsonPropertyName("enableExplorer")]
    public bool EnableExplorer { get; set; } = true;
}