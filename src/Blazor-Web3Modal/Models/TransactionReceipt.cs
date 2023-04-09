using Blazor_Web3Modal.Converters;
using Nethereum.Hex.HexTypes;
using System.Text.Json.Serialization;

namespace Blazor_Web3Modal;
/// <summary>
/// The receipt for a transaction
/// </summary>
/// <param name="To">Who the transaction was sent to (for contract creation transactions, see <see cref="ContractAddress"/></param>
/// <param name="From">Who the transaction was sent from</param>
/// <param name="ContractAddress">The contract address created when the transaction creates a contract</param>
/// <param name="TransactionIndex">The index of the transaction within the block</param>
/// <param name="GasUsed">The amount of gas used for mining the transaction</param>
/// <param name="LogsBloom">The log bloom</param>
/// <param name="BlockHash">The hash of the block the transaciton belongs to</param>
/// <param name="TransactionHash">The hash of the transaction</param>
/// <param name="Logs">The logs of the transaction</param>
/// <param name="BlockNumber">The block number the transaction belongs to</param>
/// <param name="Confirmations">How many confirmations the transaction has</param>
/// <param name="CumulativeGasUsed">How much fas was used</param>
/// <param name="EffectiveGasPrice">The effective gas price</param>
/// <param name="Status">The transaction status</param>
/// <param name="Type">The transaction type</param>
/// <param name="Byzantium"></param>
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

/// <summary>
/// Ethereum Transaction Log
/// </summary>
/// <param name="TransactionIndex">The index of the transaction within the block</param>
/// <param name="BlockNumber">The block number the transaction resides within</param>
/// <param name="TransactionHash">The transaction hash the log belongs to</param>
/// <param name="Address">The address the log belongs to</param>
/// <param name="Topics">The log topics</param>
/// <param name="Data">The log data</param>
/// <param name="LogIndex">The log index within the transaction</param>
/// <param name="BlockHash">The block hash of the block which the transaction belongs to</param>
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