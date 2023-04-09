using Nethereum.Contracts;
using Nethereum.Contracts.MessageEncodingServices;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;

namespace Blazor_Web3Modal;
public static class TransactionUtils
{
    /// <summary>
    /// Creates a TransactionInput suitable for sending Ethereum for use with <see cref="IWeb3ModalInterop.SendTransaction(TransactionInput)"/>
    /// </summary>
    /// <param name="from">The address to send from</param>
    /// <param name="to">The address to send to</param>
    /// <param name="ethereumAmount">The amount of Ethereum to send</param>
    /// <returns>A valid TransactionInput</returns>
    public static TransactionInput SendEthereumTransactionInput(string from, string to, decimal ethereumAmount)
    {
        return new TransactionInput("", to, from, null, Nethereum.Util.UnitConversion.Convert.ToWei(ethereumAmount).ToHexBigInteger());
    }

    /// <summary>
    /// Creates a TransactionInput suitable for interacting with a smart contract for use with <see cref="IWeb3ModalInterop.SendTransaction(TransactionInput)"/>
    /// </summary>
    /// <typeparam name="T">The type of the function message</typeparam>
    /// <param name="request">The request to send</param>
    /// <param name="from">The address to send from</param>
    /// <param name="contractAddress">The contract address to interact with</param>
    /// <returns>A valid TransactionInput</returns>
    public static TransactionInput SendContractTransactionInput<T>(T request, string from, string contractAddress) where T : FunctionMessage, new()
    {
        request.FromAddress = from;

        var functionMessageEncodingService = new FunctionMessageEncodingService<T>();

        var transactionInput = functionMessageEncodingService.CreateTransactionInput(request);

        transactionInput.To = contractAddress;

        return transactionInput;
    }
}