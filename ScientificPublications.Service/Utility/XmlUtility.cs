using System.IO;
using System.Xml.Serialization;

namespace ScientificPublications.Service.Utility
{
    public static class XmlUtility
    {
        public static T Deserialize<T>(string filename)
        {
            T obj;

            var serializer = new XmlSerializer(typeof(T));
            using (Stream reader = new FileStream(filename, FileMode.Open))
            {
                obj = (T)serializer.Deserialize(reader);
            }

            return obj;
        }
    }
}
