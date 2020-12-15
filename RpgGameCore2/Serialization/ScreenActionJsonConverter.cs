using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RpgGameCore2.Screens;

namespace RpgGameCore2.Serialization
{
    public class ScreenActionJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type typeToConvert) => typeToConvert == typeof(ScreenAction);

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            JObject jObject = new JObject();
            ScreenAction action = value as ScreenAction;

            if (action == null) {
                throw new Exception($"{nameof(value)} should be {nameof(ScreenAction)}");
            }
            
            jObject.Add("Description", new JValue(action.Description?.Invoke() ?? ""));
            jObject.Add("Dialogue", new JValue(action.Dialogue?.Invoke() ?? ""));
            jObject.Add("SilverPrice", new JValue(action.SilverPrice?.Invoke() ?? 0));
            jObject.Add("GoldPrice", new JValue(action.GoldPrice?.Invoke() ?? 0));
            jObject.Add("DiamondsPrice", new JValue(action.DiamondsPrice?.Invoke() ?? 0));
            jObject.Add("EnergyPrice", new JValue(action.EnergyPrice?.Invoke() ?? 0));

            jObject.WriteTo(writer);
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer) => null;
    }
}
