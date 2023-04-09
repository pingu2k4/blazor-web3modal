using Blazor_Web3Modal.Converters;
using Nethereum.Hex.HexTypes;
using System.Text.Json.Serialization;

namespace Blazor_Web3Modal;

/// <summary>
/// Transaction Data
/// </summary>
/// <param name="TransactionHash">The hash of the transaction</param>
/// <param name="Type">The transaction type</param>
/// <param name="AccessList">The access list</param>
/// <param name="BlockHash">The hash of the block the transaciton belongs to</param>
/// <param name="BlockNumber">The block number the transaction belongs to</param>
/// <param name="TransactionIndex">The index of the transaction within the block</param>
/// <param name="Confirmations">How many confirmations the transaction has</param>
/// <param name="From">Who the transaction was sent from</param>
/// <param name="GasPrice">The gas price</param>
/// <param name="MaxPriorityFeePerGas">The max priority fee per gas</param>
/// <param name="MaxFeePerGas">The max fee per gas</param>
/// <param name="GasLimit">The gas limit</param>
/// <param name="To">Who the transaction was sent to</param>
/// <param name="Value">The amount of Ethereum sent in the transaction</param>
/// <param name="Nonce">The transaction nonce</param>
/// <param name="Data">The transaction raw data</param>
/// <param name="R">R</param>
/// <param name="S">S</param>
/// <param name="V">V</param>
/// <param name="ChainId">The chain ID the transaction took place on</param>
public record TransactionData([property: JsonPropertyName("hash")] string TransactionHash,
    [property: JsonPropertyName("type"), JsonConverter(typeof(IntToHexBigIntegerConverter))] HexBigInteger Type,
    [property: JsonPropertyName("accessList")] List<AccessList> AccessList,
    [property: JsonPropertyName("blockHash")] string? BlockHash,
    [property: JsonPropertyName("blockNumber"), JsonConverter(typeof(IntToHexBigIntegerConverter))] HexBigInteger? BlockNumber,
    [property: JsonPropertyName("transactionIndex"), JsonConverter(typeof(IntToHexBigIntegerConverter))] HexBigInteger? TransactionIndex,
    [property: JsonPropertyName("confirmations"), JsonConverter(typeof(IntToHexBigIntegerConverter))] HexBigInteger Confirmations,
    [property: JsonPropertyName("from")] string From,
    [property: JsonPropertyName("gasPrice"), JsonConverter(typeof(BigNumberToHexBigIntegerConverter))] HexBigInteger GasPrice,
    [property: JsonPropertyName("maxPriorityFeePerGas"), JsonConverter(typeof(BigNumberToHexBigIntegerConverter))] HexBigInteger MaxPriorityFeePerGas,
    [property: JsonPropertyName("maxFeePerGas"), JsonConverter(typeof(BigNumberToHexBigIntegerConverter))] HexBigInteger MaxFeePerGas,
    [property: JsonPropertyName("gasLimit"), JsonConverter(typeof(BigNumberToHexBigIntegerConverter))] HexBigInteger GasLimit,
    [property: JsonPropertyName("to")] string? To,
    [property: JsonPropertyName("value"), JsonConverter(typeof(BigNumberToHexBigIntegerConverter))] HexBigInteger Value,
    [property: JsonPropertyName("nonce"), JsonConverter(typeof(IntToHexBigIntegerConverter))] HexBigInteger Nonce,
    [property: JsonPropertyName("data")] string Data,
    [property: JsonPropertyName("r")] string R,
    [property: JsonPropertyName("s")] string S,
    [property: JsonPropertyName("v"), JsonConverter(typeof(IntToHexBigIntegerConverter))] HexBigInteger V,
    [property: JsonPropertyName("chainId"), JsonConverter(typeof(IntToHexBigIntegerConverter))] HexBigInteger ChainId
    );

/// <summary>
/// 
/// </summary>
/// <param name="Address"></param>
/// <param name="StorageKeys"></param>
public record AccessList([property: JsonPropertyName("address")] string Address,
    [property: JsonPropertyName("storageKeys")] List<string>? StorageKeys);