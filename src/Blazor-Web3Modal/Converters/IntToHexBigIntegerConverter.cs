using Nethereum.Hex.HexTypes;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Blazor_Web3Modal.Converters;
internal class IntToHexBigIntegerConverter : JsonConverter<HexBigInteger>
{
    public override HexBigInteger? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Number)
        {
            var value = reader.GetInt64();
            var bigInt = new BigInteger(value);
            return bigInt.ToHexBigInteger();
        }

        throw new JsonException();
    }

    public override void Write(Utf8JsonWriter writer, HexBigInteger value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}
