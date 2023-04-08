using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Blazor_Web3Modal;
public record Web3ModalResult([property:JsonPropertyName("error"), JsonProperty("error")] string? Error)
{
    public bool IsErrored => Error is not null;
}

public record Web3ModalResult<T>(string? Error,
    [property: JsonPropertyName("result"), JsonProperty("result")] T? Result,
    [property: JsonPropertyName("success"), JsonProperty("success")] bool Success) : Web3ModalResult(Error);