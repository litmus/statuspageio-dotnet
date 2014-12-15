using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace StatusPageIo.Api.Converters
{
    /// <summary>
    /// Converts Pascal-cased .NET enum objects to Snake Case JSON values
    /// </summary>
    public class PascalCaseEnumConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var stringToWrite = string.Concat(value.ToString().Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();
            writer.WriteValue(stringToWrite);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var enumString = (string) reader.Value;

            var enumValue = enumString.ToLower().Replace("_", " ");
            var info = CultureInfo.CurrentCulture.TextInfo;
            enumValue = info.ToTitleCase(enumValue).Replace(" ", string.Empty);
            return Enum.Parse(objectType, enumValue);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }
    }
}
