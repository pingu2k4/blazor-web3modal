using Nethereum.ABI.FunctionEncoding.Attributes;
using System.Text.Json.Serialization;

namespace Example.Models;

[Struct("Mail")]
public class Mail
{
    [Parameter("tuple[]", "to", 1, "Person[]")]
    [JsonPropertyName("to")]
    public List<Person> To { get; set; } = new List<Person>();

    [Parameter("string", "contents", 2)]
    [JsonPropertyName("contents")]
    public string Contents { get; set; } = null!;
}