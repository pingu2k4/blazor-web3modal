using Nethereum.Contracts;
using Nethereum.Contracts.MessageEncodingServices;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;

namespace Blazor_Web3Modal;
public static class TransactionUtils
{
    public static TransactionInput SendEthereumTransactionInput(string from, string to, decimal ethereumAmount)
    {
        return new TransactionInput("", to, from, null, Nethereum.Util.UnitConversion.Convert.ToWei(ethereumAmount).ToHexBigInteger());
    }

    public static TransactionInput SendContractTransactionInput<T>(T request, string from, string contractAddress) where T : FunctionMessage, new()
    {
        request.FromAddress = from;

        var functionMessageEncodingService = new FunctionMessageEncodingService<T>();

        var transactionInput = functionMessageEncodingService.CreateTransactionInput(request);

        transactionInput.To = contractAddress;

        return transactionInput;
    }
}