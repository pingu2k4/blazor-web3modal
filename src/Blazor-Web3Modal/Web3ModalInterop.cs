using Blazor_Web3Modal.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;
using Nethereum.ABI.EIP712;
using Nethereum.RPC.Eth.DTOs;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Blazor_Web3Modal;

public class Web3ModalInterop : IAsyncDisposable, IWeb3ModalInterop
{
    private readonly Lazy<Task<IJSObjectReference>> _moduleTask;
    private readonly Web3ModalOptions _options;
    private readonly DotNetObjectReference<Web3ModalInterop> _jsRef;

    private bool _configured;
    private AccountState? _cachedAccountState;
    private NetworkState? _cachedNetworkState;

    const string AddressNotConnectedError = "Address is not yet connected. Specify an address instead.";
    const string DeserializingResultError = "Error deserialising result.";

    public Web3ModalInterop(IJSRuntime jsRuntime, IOptions<Web3ModalOptions> options)
    {
        _moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>("import",
            "./_content/Blazor-Web3Modal/index.bundle.js").AsTask());
        _options = options.Value;
        _jsRef = DotNetObjectReference.Create(this);
    }

    /// <inheritdoc />
    public async Task Configure()
    {
        if (!_configured)
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("configure", JsonSerializer.Serialize(_options), _jsRef);
            _configured = true;
        }
    }

    private async ValueTask EnsureConfigured()
    {
        if (!_configured)
        {
            await Configure();
        }
    }

    /// <inheritdoc />
    public async Task OpenModal()
    {
        await EnsureConfigured();
        var module = await _moduleTask.Value;
        await module.InvokeVoidAsync("openModal");
    }

    /// <inheritdoc />
    public async Task CloseModal()
    {
        await EnsureConfigured();
        var module = await _moduleTask.Value;
        await module.InvokeVoidAsync("closeModal");
    }

    /// <inheritdoc />
    public async Task SetTheme(Web3ModalThemeOptions themeOptions)
    {
        await EnsureConfigured();
        var module = await _moduleTask.Value;
        await module.InvokeVoidAsync("setTheme", themeOptions.ThemeMode.GetEnumDescription(), themeOptions.ThemeVariables);
    }

    /// <inheritdoc />
    public async Task Disconnect()
    {
        await EnsureConfigured();
        var module = await _moduleTask.Value;
        await module.InvokeVoidAsync("disconnect");
    }

    /// <inheritdoc />
    public async Task<Web3ModalResult<AccountState>> GetAccountState()
    {
        await EnsureConfigured();
        var module = await _moduleTask.Value;
        var stringResult = await module.InvokeAsync<string>("getAccount");
        var result = JsonSerializer.Deserialize<Web3ModalResult<AccountState>>(stringResult);

        if (result is null)
            return new Web3ModalResult<AccountState>(DeserializingResultError, null, false);

        return result;
    }

    /// <inheritdoc />
    public async Task<Web3ModalResult<NetworkState>> GetNetworkState()
    {
        await EnsureConfigured();
        var module = await _moduleTask.Value;
        var stringResult = await module.InvokeAsync<string>("getNetwork");
        var result = JsonSerializer.Deserialize<Web3ModalResult<NetworkState>>(stringResult);

        if (result is null)
            return new Web3ModalResult<NetworkState>(DeserializingResultError, null, false);

        return result;
    }

    /// <inheritdoc />
    public async Task<Web3ModalResult<NetworkState>> SwitchChain(int chainId)
    {
        await EnsureConfigured();
        var module = await _moduleTask.Value;
        var stringResult = await module.InvokeAsync<string>("switchNetwork", chainId);
        var result = JsonSerializer.Deserialize<Web3ModalResult<NetworkState>>(stringResult);

        if (result is null)
            return new Web3ModalResult<NetworkState>(DeserializingResultError, null, false);

        return result;
    }

    /// <inheritdoc />
    public async Task<Web3ModalResult<string>> SendTransaction(TransactionInput transactionInput)
    {
        await EnsureConfigured();
        var module = await _moduleTask.Value;
        var stringResult = await module.InvokeAsync<string>("trySendTransaction", Newtonsoft.Json.JsonConvert.SerializeObject(transactionInput), _jsRef);
        var result = JsonSerializer.Deserialize<Web3ModalResult<string>>(stringResult);

        if (result is null)
            return new Web3ModalResult<string>(DeserializingResultError, null, false);

        return result;
    }

    /// <inheritdoc />
    public async Task<Web3ModalResult<string>> SignMessage(string message)
    {
        await EnsureConfigured();
        var module = await _moduleTask.Value;
        var stringResult = await module.InvokeAsync<string>("trySignMessage", message);
        var result = JsonSerializer.Deserialize<Web3ModalResult<string>>(stringResult);

        if (result is null)
            return new Web3ModalResult<string>(DeserializingResultError, null, false);

        return result;
    }

    /// <inheritdoc />
    public async Task<Web3ModalResult<string>> SignTypedData<T>(TypedData<Domain> typedData, T value)
    {
        await EnsureConfigured();
        var module = await _moduleTask.Value;

        var newtonsoftContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver
        {
            NamingStrategy = new Newtonsoft.Json.Serialization.CamelCaseNamingStrategy()
        };
        var newtonsoftSettings = new Newtonsoft.Json.JsonSerializerSettings
        {
            ContractResolver = newtonsoftContractResolver
        };

        var stringResult = await module.InvokeAsync<string>("trySignTypedData",
            Newtonsoft.Json.JsonConvert.SerializeObject(typedData.Domain, newtonsoftSettings),
            Newtonsoft.Json.JsonConvert.SerializeObject(typedData),
            JsonSerializer.Serialize(value));
        var result = JsonSerializer.Deserialize<Web3ModalResult<string>>(stringResult);

        if (result is null)
            return new Web3ModalResult<string>(DeserializingResultError, null, false);

        return result;
    }

    /// <inheritdoc />
    public async Task<Web3ModalResult<FetchBalanceResult>> GetBalance()
    {
        if (_cachedAccountState is null || _cachedAccountState.Address is null)
            return new Web3ModalResult<FetchBalanceResult>(AddressNotConnectedError, null, false);

        return await GetBalance(_cachedAccountState.Address);
    }

    /// <inheritdoc />
    public async Task<Web3ModalResult<FetchBalanceResult>> GetBalance(string address, string? tokenAddress = null, int? chainId = null)
    {
        await EnsureConfigured();
        var module = await _moduleTask.Value;
        var stringResult = await module.InvokeAsync<string>("getBalance", address, chainId, tokenAddress);
        var result = JsonSerializer.Deserialize<Web3ModalResult<FetchBalanceResult>>(stringResult);

        if (result is null)
            return new Web3ModalResult<FetchBalanceResult>(DeserializingResultError, null, false);

        return result;
    }

    /// <inheritdoc />
    public async Task<Web3ModalResult<string>> GetEnsAvatar()
    {
        if (_cachedAccountState is null || _cachedAccountState.Address is null)
            return new Web3ModalResult<string>(AddressNotConnectedError, null, false);

        return await GetEnsAvatar(_cachedAccountState.Address);
    }

    /// <inheritdoc />
    public async Task<Web3ModalResult<string>> GetEnsAvatar(string address, int? chainId = null)
    {
        await EnsureConfigured();
        var module = await _moduleTask.Value;
        var stringResult = await module.InvokeAsync<string>("getEnsAvatar", address, chainId);
        var result = JsonSerializer.Deserialize<Web3ModalResult<string>>(stringResult);

        if (result is null)
            return new Web3ModalResult<string>(DeserializingResultError, null, false);

        return result;
    }

    /// <inheritdoc />
    public async Task<Web3ModalResult<string>> GetEnsName()
    {
        if (_cachedAccountState is null || _cachedAccountState.Address is null)
            return new Web3ModalResult<string>(AddressNotConnectedError, null, false);

        return await GetEnsName(_cachedAccountState.Address);
    }

    /// <inheritdoc />
    public async Task<Web3ModalResult<string>> GetEnsName(string address, int? chainId = null)
    {
        await EnsureConfigured();
        var module = await _moduleTask.Value;
        var stringResult = await module.InvokeAsync<string>("getEnsName", address, chainId);
        var result = JsonSerializer.Deserialize<Web3ModalResult<string>>(stringResult);

        if (result is null)
            return new Web3ModalResult<string>(DeserializingResultError, null, false);

        return result;
    }

    /// <inheritdoc />
    public async Task<Web3ModalResult<int?>> GetBlockNumber(int? chainId = null)
    {
        await EnsureConfigured();
        var module = await _moduleTask.Value;
        var stringResult = await module.InvokeAsync<string>("getBlockNumber", chainId);
        var result = JsonSerializer.Deserialize<Web3ModalResult<int?>>(stringResult);

        if (result is null)
            return new Web3ModalResult<int?>(DeserializingResultError, null, false);

        return result;
    }

    /// <inheritdoc />
    public async Task<Web3ModalResult<string>> GetEnsAddress(string name, int? chainId = null)
    {
        await EnsureConfigured();
        var module = await _moduleTask.Value;
        var stringResult = await module.InvokeAsync<string>("getEnsAddress", name, chainId);
        var result = JsonSerializer.Deserialize<Web3ModalResult<string>>(stringResult);

        if (result is null)
            return new Web3ModalResult<string>(DeserializingResultError, null, false);

        return result;
    }

    /// <inheritdoc />
    public async Task<Web3ModalResult<FeeData>> GetFeeData(int? chainId = null)
    {
        await EnsureConfigured();
        var module = await _moduleTask.Value;
        var stringResult = await module.InvokeAsync<string>("getFeeData", chainId);

        var options = new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowReadingFromString
        };

        var result = JsonSerializer.Deserialize<Web3ModalResult<FeeData>>(stringResult, options);

        if (result is null)
            return new Web3ModalResult<FeeData>(DeserializingResultError, null, false);

        return result;
    }

    /// <inheritdoc />
    public async Task<Web3ModalResult<TransactionData>> GetTransaction(string hash, int? chainId = null)
    {
        await EnsureConfigured();
        var module = await _moduleTask.Value;
        var stringResult = await module.InvokeAsync<string>("getTransaction", hash, chainId);
        var result = JsonSerializer.Deserialize<Web3ModalResult<TransactionData>>(stringResult);

        if (result is null)
            return new Web3ModalResult<TransactionData>(DeserializingResultError, null, false);

        return result;
    }

    /// <inheritdoc />
    public async Task<Web3ModalResult<TokenData>> GetToken(string address, int? chainId = null)
    {
        await EnsureConfigured();
        var module = await _moduleTask.Value;
        var stringResult = await module.InvokeAsync<string>("getToken", address, chainId);
        var result = JsonSerializer.Deserialize<Web3ModalResult<TokenData>>(stringResult);

        if (result is null)
            return new Web3ModalResult<TokenData>(DeserializingResultError, null, false);

        return result;
    }

    /// <inheritdoc />
    public async Task StartWatchingBlockNumber(int? chainId = null)
    {
        await EnsureConfigured();
        var module = await _moduleTask.Value;
        await module.InvokeVoidAsync("startWatchingBlockNumber", chainId, _jsRef);
    }

    /// <inheritdoc />
    public async Task StopWatchingBlockNumber()
    {
        await EnsureConfigured();
        var module = await _moduleTask.Value;
        await module.InvokeVoidAsync("stopWatchingBlockNumber");
    }

    [JSInvokable()]
    public Task OnAccountChanged(string? account)
    {
        if (account is null)
        {
            _cachedAccountState = null;
        }
        else
        {
            _cachedAccountState = JsonSerializer.Deserialize<AccountState>(account);
        }

        RaiseAccountChanged(_cachedAccountState);

        return Task.CompletedTask;
    }

    [JSInvokable()]
    public Task OnNetworkChanged(string? network)
    {
        if (network is null)
        {
            _cachedNetworkState = null;
        }
        else
        {
            _cachedNetworkState = JsonSerializer.Deserialize<NetworkState>(network);
        }

        RaiseNetworkChanged(_cachedNetworkState);

        return Task.CompletedTask;
    }

    [JSInvokable()]
    public Task OnBlockNumber(string? blockNumber)
    {
        if (blockNumber is not null)
        {
            var blockNum = JsonSerializer.Deserialize<int>(blockNumber);
            RaiseOnBlockNumber(blockNum);
        }

        return Task.CompletedTask;
    }

    [JSInvokable()]
    public Task OnTransactionComplete(string? transactionReceipt)
    {
        if (transactionReceipt is not null)
        {
            var receipt = JsonSerializer.Deserialize<TransactionReceipt>(transactionReceipt);
            if (receipt is not null)
            {
                RaiseTransactionMined(receipt);
            }
        }

        return Task.CompletedTask;
    }


    /// <inheritdoc />
    public event EventHandler<AccountChangedEventArgs>? AccountChanged;
    private void RaiseAccountChanged(AccountState? accountState)
    {
        var e = new AccountChangedEventArgs
        {
            AccountState = accountState
        };
        AccountChanged?.Invoke(this, e);
    }

    /// <inheritdoc />
    public event EventHandler<NetworkChangedEventArgs>? NetworkChanged;
    private void RaiseNetworkChanged(NetworkState? networkState)
    {
        var e = new NetworkChangedEventArgs
        {
            NetworkState = networkState
        };
        NetworkChanged?.Invoke(this, e);
    }

    /// <inheritdoc />
    public event EventHandler<BlockNumberEventArgs>? BlockNumberChanged;
    private void RaiseOnBlockNumber(int blockNum)
    {
        var e = new BlockNumberEventArgs { BlockNumber = blockNum };
        BlockNumberChanged?.Invoke(this, e);
    }

    /// <inheritdoc />
    public event EventHandler<TransactionMinedEventArgs>? TransactionMined;
    private void RaiseTransactionMined(TransactionReceipt transactionReceipt)
    {
        var e = new TransactionMinedEventArgs { TransactionReceipt = transactionReceipt };
        TransactionMined?.Invoke(this, e);
    }

    public async ValueTask DisposeAsync()
    {
        if (_moduleTask.IsValueCreated)
        {
            var module = await _moduleTask.Value;
            await module.DisposeAsync();
        }
    }
}
