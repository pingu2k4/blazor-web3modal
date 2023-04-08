using Blazor_Web3Modal.Converters;
using Nethereum.Hex.HexTypes;
using System.Text.Json.Serialization;

namespace Blazor_Web3Modal;
public record TransactionReceipt([property: JsonPropertyName("to")] string? To,
    [property: JsonPropertyName("from")] string From,
    [property: JsonPropertyName("contractAddress")] string? ContractAddress,
    [property: JsonPropertyName("transactionIndex"), JsonConverter(typeof(IntToHexBigIntegerConverter))] HexBigInteger? TransactionIndex,
    [property: JsonPropertyName("gasUsed"), JsonConverter(typeof(BigNumberToHexBigIntegerConverter))] HexBigInteger GasUsed,
    [property: JsonPropertyName("logsBloom")] string LogsBloom,
    [property: JsonPropertyName("blockHash")] string BlockHash,
    [property: JsonPropertyName("transactionHash")] string TransactionHash,
    [property: JsonPropertyName("logs")] List<Log> Logs,
    [property: JsonPropertyName("blockNumber"), JsonConverter(typeof(IntToHexBigIntegerConverter))] HexBigInteger BlockNumber,
    [property: JsonPropertyName("confirmations"), JsonConverter(typeof(IntToHexBigIntegerConverter))] HexBigInteger Confirmations,
    [property: JsonPropertyName("cumulativeGasUsed"), JsonConverter(typeof(BigNumberToHexBigIntegerConverter))] HexBigInteger CumulativeGasUsed,
    [property: JsonPropertyName("effectiveGasPrice"), JsonConverter(typeof(BigNumberToHexBigIntegerConverter))] HexBigInteger EffectiveGasPrice,
    [property: JsonPropertyName("status"), JsonConverter(typeof(IntToHexBigIntegerConverter))] HexBigInteger Status,
    [property: JsonPropertyName("type"), JsonConverter(typeof(IntToHexBigIntegerConverter))] HexBigInteger Type,
    [property: JsonPropertyName("byzantium")] bool Byzantium
    );

public record Log([property: JsonPropertyName("transactionIndex"), JsonConverter(typeof(IntToHexBigIntegerConverter))] HexBigInteger? TransactionIndex,
    [property: JsonPropertyName("blockNumber"), JsonConverter(typeof(IntToHexBigIntegerConverter))] HexBigInteger? BlockNumber,
    [property: JsonPropertyName("transactionHash")] string TransactionHash,
    [property: JsonPropertyName("address")] string Address,
    [property: JsonPropertyName("topics")] List<string> Topics,
    [property: JsonPropertyName("data")] string Data,
    [property: JsonPropertyName("logIndex"), JsonConverter(typeof(IntToHexBigIntegerConverter))] HexBigInteger? LogIndex,
    [property: JsonPropertyName("blockHash")] string BlockHash
    );

public class TransactionMinedEventArgs : EventArgs
{
    public required TransactionReceipt TransactionReceipt { get; set; }
}