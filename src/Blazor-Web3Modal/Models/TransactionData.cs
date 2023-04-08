using Blazor_Web3Modal.Converters;
using Nethereum.Hex.HexTypes;
using System.Text.Json.Serialization;

namespace Blazor_Web3Modal;

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

public record AccessList([property: JsonPropertyName("address")] string Address,
    [property: JsonPropertyName("storageKeys")] List<string>? StorageKeys);