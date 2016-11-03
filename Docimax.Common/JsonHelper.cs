using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docimax.Common
{
    public class JsonHelper
    {
        public static string SerializeObject(object o)
        {
            var settings = new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore,
                    DateFormatString = "yyyy-MM-dd HH:mm:ss",
                    Converters = new List<JsonConverter> { new DecimalConverter() },
                    ContractResolver = new OrderedContractResolver(),
                };
            string output = JsonConvert.SerializeObject(o, settings);
            return output;
        }

        public static T DeserializeObject<T>(string value) where T : class
        {
            var settings = new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
                DateFormatString = "yyyy-MM-dd HH:mm:ss",
                Converters = new List<JsonConverter> { new DecimalConverter() },
                ContractResolver = new OrderedContractResolver(),
            };
            try
            {
                return JsonConvert.DeserializeObject<T>(value, settings);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }

    /// <summary>
    /// Decimal保留两位小数 ep.:5.00
    /// </summary>
    class DecimalConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(decimal) || objectType == typeof(decimal?));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken token = JToken.Load(reader);
            if (token.Type == JTokenType.Float || token.Type == JTokenType.Integer)
            {
                return token.ToObject<decimal>();
            }
            if (token.Type == JTokenType.String)
            {
                return Decimal.Parse(token.ToString());
            }
            if (token.Type == JTokenType.Null && objectType == typeof(decimal?))
            {
                return 0;
            }
            throw new JsonSerializationException("Unexpected token type: " +
                                                  token.Type.ToString());
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Decimal d = 0;
            if (value != null)
            {
                d = (Decimal)value;
            }
            writer.WriteValue(string.Format("{0:N2}", d));
        }
    }

    /// <summary>
    /// 序列化时，按照属性名称排序
    /// </summary>
    class OrderedContractResolver : DefaultContractResolver
    {
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            return base.CreateProperties(type, memberSerialization).OrderBy(p => p.Order).ThenBy(t => t.PropertyName).ToList();
        }
    }
}
