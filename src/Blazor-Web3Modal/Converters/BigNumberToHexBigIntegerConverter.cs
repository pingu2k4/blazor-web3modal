using Nethereum.Hex.HexTypes;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Blazor_Web3Modal.Converters;
internal class BigNumberToHexBigIntegerConverter : JsonConverter<HexBigInteger>
{
    public override HexBigInteger? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if(reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException();
        }

        HexBigInteger? value = null;

        while (reader.Read())
        {
            if(reader.TokenType == JsonTokenType.EndObject)
            {
                if (value is not null)
                    return value;
                throw new JsonException();
            }
            if(reader.TokenType == JsonTokenType.PropertyName)
            {
                var propertyName = reader.GetString();
                reader.Read();
                if(propertyName == "hex")
                {
                    value = new HexBigInteger(reader.GetString());
                }
            }
        }

        throw new Exception();
    }

    public override void Write(Utf8JsonWriter writer, HexBigInteger value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}