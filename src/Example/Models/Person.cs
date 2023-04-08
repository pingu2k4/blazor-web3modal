using Nethereum.ABI.FunctionEncoding.Attributes;
using System.Text.Json.Serialization;

namespace Example.Models;

[Struct("Person")]
public class Person
{
    [Parameter("string", "name", 1)]
    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    [Parameter("address[]", "wallets", 2)]
    [JsonPropertyName("wallets")]
    public List<string> Wallets { get; set; } = new List<string>();
}