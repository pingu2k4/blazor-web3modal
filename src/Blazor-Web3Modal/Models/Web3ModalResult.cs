using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Blazor_Web3Modal;
public record Web3ModalResult([property:JsonPropertyName("error"), JsonProperty("error")] string? Error)
{
    /// <summary>
    /// Whether the request errored or not
    /// </summary>
    public bool IsErrored => Error is not null;
}

/// <summary>
/// A response from a request made to Web3Modal, containing a result or error
/// </summary>
/// <typeparam name="T">The type of result</typeparam>
/// <param name="Error">If errored, the error message encountered</param>
/// <param name="Result">If successful, the result of the request</param>
/// <param name="Success">Whether the request was successful or not</param>
public record Web3ModalResult<T>(string? Error,
    [property: JsonPropertyName("result"), JsonProperty("result")] T? Result,
    [property: JsonPropertyName("success"), JsonProperty("success")] bool Success) : Web3ModalResult(Error);