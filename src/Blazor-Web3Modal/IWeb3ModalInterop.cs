using Nethereum.ABI.EIP712;
using Nethereum.RPC.Eth.DTOs;

namespace Blazor_Web3Modal;
public interface IWeb3ModalInterop
{
    /// <summary>
    /// Fires when the connected wallet address changes
    /// </summary>
    event EventHandler<AccountChangedEventArgs>? AccountChanged;
    /// <summary>
    /// Fires when block number updates (and we are <see cref="StartWatchingBlockNumber(int?)">watching block number</see>)
    /// </summary>
    event EventHandler<BlockNumberEventArgs>? BlockNumberChanged;
    /// <summary>
    /// Fires when the connected wallets network changes
    /// </summary>
    event EventHandler<NetworkChangedEventArgs>? NetworkChanged;
    /// <summary>
    /// Fires when a transaction we have <see cref="SendTransaction(TransactionInput)">sent</see> is mined, providing the transaction reveipt
    /// </summary>
    event EventHandler<TransactionMinedEventArgs>? TransactionMined;

    /// <summary>
    /// Programmatically close the modal
    /// </summary>
    Task CloseModal();
    /// <summary>
    /// Configures The Web3Modal component - loading in the required JS library and setting it up with the options defined at program startup
    /// </summary>
    Task Configure();
    /// <summary>
    /// Disconnect the currently connected wallet from the Dapp
    /// </summary>
    Task Disconnect();
    /// <summary>
    /// Gets the current AccountState
    /// </summary>
    /// <returns>Contains the connected address (if applicable) and the connection state</returns>
    Task<Web3ModalResult<AccountState>> GetAccountState();
    /// <summary>
    /// Gets the balance of the currently connected wallet in the native currency to the current chain
    /// </summary>
    /// <returns>The current balance, as well as details such as the currencies symbol</returns>
    Task<Web3ModalResult<FetchBalanceResult>> GetBalance();
    /// <summary>
    /// Gets the balance information for Ethereum or ERC-20 tokens
    /// </summary>
    /// <param name="address">Address to fetch balance for</param>
    /// <param name="tokenAddress">Address for ERC-20 token</param>
    /// <param name="chainId">Force a specific chain id for the request</param>
    /// <returns>The current balance, as well as details such as the currencies symbol</returns>
    Task<Web3ModalResult<FetchBalanceResult>> GetBalance(string address, string? tokenAddress = null, int? chainId = null);
    /// <summary>
    /// Gets the current block number
    /// </summary>
    /// <param name="chainId">Force a specific chain id for the request</param>
    /// <returns>the current block number</returns>
    Task<Web3ModalResult<int?>> GetBlockNumber(int? chainId = null);
    /// <summary>
    /// Gets address for ENS name
    /// </summary>
    /// <param name="name">ENS name to fetch address for</param>
    /// <param name="chainId">Force a specific chain id for the request</param>
    /// <returns>The address the name resolves to</returns>
    Task<Web3ModalResult<string>> GetEnsAddress(string name, int? chainId = null);
    /// <summary>
    /// Gets avatar for ENS name for the currently connected address
    /// </summary>
    /// <returns>The location of the avatar, if set</returns>
    Task<Web3ModalResult<string>> GetEnsAvatar();
    /// <summary>
    /// Gets avatar for ENS name
    /// </summary>
    /// <param name="address">Address or ENS name to fetch avatar for</param>
    /// <param name="chainId">Force a specific chain id for the request</param>
    /// <returns>The location of the avatar, if set</returns>
    Task<Web3ModalResult<string>> GetEnsAvatar(string address, int? chainId = null);
    /// <summary>
    /// Gets ENS name for the currently connected address
    /// </summary>
    /// <returns>The ENS name</returns>
    Task<Web3ModalResult<string>> GetEnsName();
    /// <summary>
    /// Gets ENS name for address
    /// </summary>
    /// <param name="address">Address to fetch ENS name for</param>
    /// <param name="chainId">Force a specific chain id for the request</param>
    /// <returns>The ENS name</returns>
    Task<Web3ModalResult<string>> GetEnsName(string address, int? chainId = null);
    /// <summary>
    /// Gets network fee information
    /// </summary>
    /// <param name="chainId">Force a specific chain id for the request</param>
    /// <returns>Information on the prices for the current network fees</returns>
    Task<Web3ModalResult<FeeData>> GetFeeData(int? chainId = null);
    /// <summary>
    /// Gets the current NetworkState
    /// </summary>
    /// <returns>Contains information on the currently selected network for the connected wallet</returns>
    Task<Web3ModalResult<NetworkState>> GetNetworkState();
    /// <summary>
    /// Gets ERC-20 token information
    /// </summary>
    /// <param name="address">Address of ERC-20 token</param>
    /// <param name="chainId">Force a specific chain id for the request</param>
    /// <returns>Data for the token such as symbol, total supply etc.</returns>
    Task<Web3ModalResult<TokenData>> GetToken(string address, int? chainId = null);
    /// <summary>
    /// Gets transaction by hash
    /// </summary>
    /// <param name="hash">Hash of the transaction</param>
    /// <param name="chainId">Force a specific chain id for the request</param>
    /// <returns>Detailed data for the transaction</returns>
    Task<Web3ModalResult<TransactionData>> GetTransaction(string hash, int? chainId = null);
    /// <summary>
    /// Programmatically open the modal
    /// </summary>
    Task OpenModal();
    /// <summary>
    /// Requests the user send a transaction
    /// </summary>
    /// <param name="transactionInput">Data describing the proposed transaction. This can be generated with either <see cref="TransactionUtils.SendEthereumTransactionInput(string, string, decimal)">SendEthereumTransactionInput</see> or <see cref="TransactionUtils.SendContractTransactionInput{T}(T, string, string)">SendContractTransactionInput</see></param>
    /// <returns>The hash of the transaction sent</returns>
    Task<Web3ModalResult<string>> SendTransaction(TransactionInput transactionInput);
    /// <summary>
    /// Programmatically set or update modal's theme
    /// </summary>
    /// <param name="themeOptions">options describing the proposed theme for the modal</param>
    Task SetTheme(Web3ModalThemeOptions themeOptions);
    /// <summary>
    /// Sign messages with connected account
    /// </summary>
    /// <param name="message">Message to sign with wallet</param>
    /// <returns>Signature</returns>
    Task<Web3ModalResult<string>> SignMessage(string message);
    /// <summary>
    /// Sign typed data with connected account
    /// </summary>
    /// <typeparam name="T">The type of the root message value</typeparam>
    /// <param name="typedData">Typed data definition. This can be easily generated with <see cref="SignUtils.GetTypedData(Domain, Type[])">GetTypedData</see></param>
    /// <param name="value">The message you want signed</param>
    /// <returns>Signature</returns>
    Task<Web3ModalResult<string>> SignTypedData<T>(TypedData<Domain> typedData, T value);
    /// <summary>
    /// Starts watching block numbers as they change, firing <see cref="BlockNumberChanged">BlockNumberChanged</see>
    /// </summary>
    /// <param name="chainId">Force a specific chain id for the request</param>
    Task StartWatchingBlockNumber(int? chainId = null);
    /// <summary>
    /// Stops watching block numbers
    /// </summary>
    Task StopWatchingBlockNumber();
    /// <summary>
    /// Switches chains
    /// </summary>
    /// <param name="chainId">Force a specific chain id for the request</param>
    /// <returns>Details on the newly selected chain</returns>
    Task<Web3ModalResult<NetworkState>> SwitchChain(int chainId);
}