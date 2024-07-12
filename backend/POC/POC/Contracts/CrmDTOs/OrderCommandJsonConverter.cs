using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace POC.Contracts.CrmDTOs;
using JsonConverter = Newtonsoft.Json.JsonConverter;
using JsonReader = Newtonsoft.Json.JsonReader;
using JsonWriter = Newtonsoft.Json.JsonWriter;

public class OrderCommandJsonConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(OrderCommand);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var jsonObject = JObject.Load(reader);
        var cmd = jsonObject["cmd"].Value<string>();

        BaseOrderDto order = null;

        if (cmd == "create" || cmd == "update")
        {
            order = jsonObject["order"].ToObject<CrmOrder>(serializer);
        }
        else if (cmd == "delete")
        {
            order = jsonObject["order"].ToObject<DeleteOrderDto>(serializer);
        }

        return new OrderCommand
        {
            cmd = cmd,
            order = order
        };
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        var orderCommand = (OrderCommand)value;

        writer.WriteStartObject();
        writer.WritePropertyName("cmd");
        writer.WriteValue(orderCommand.cmd);
        writer.WritePropertyName("order");
        serializer.Serialize(writer, orderCommand.order);
        writer.WriteEndObject();
    }
}