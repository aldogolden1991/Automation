using Newtonsoft.Json;
using System.IO;
using System.Xml.Serialization;

namespace Automation.API.Utility
{
    public static class Serializer
    {
        public static T DeserializeXml<T>(string input) where T : class
        {
            XmlSerializer ser = new XmlSerializer(typeof(T));
            using StringReader sr = new StringReader(input);
            return (T)ser.Deserialize(sr);
        }

        public static string SerializeXml<T>(T ObjectToSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(ObjectToSerialize.GetType());
            using StringWriter textWriter = new StringWriter();
            xmlSerializer.Serialize(textWriter, ObjectToSerialize);
            return textWriter.ToString();
        }

        public static T DeserializeJson<T>(string input) where T : class
        {
            return JsonConvert.DeserializeObject<T>(input);
        }

        public static string SerializeJson<T>(T ObjectToSerialize)
        {
            return JsonConvert.SerializeObject(ObjectToSerialize);
        }
    }
}
