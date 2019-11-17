using ScientificPublications.Common.Exceptions;
using System;
using System.IO;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace ScientificPublications.Common.Utility
{
    public static class XmlUtility
    {
        public static T DeserializeFromFile<T>(string filename)
        {
            T obj;

            var serializer = new XmlSerializer(typeof(T));
            using (Stream reader = new FileStream(filename, FileMode.Open))
            {
                obj = (T)serializer.Deserialize(reader);
            }

            return obj;
        }

        public static T Deserialize<T>(string xmlString)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            StringReader rdr = new StringReader(xmlString);
            return (T)serializer.Deserialize(rdr);
        }

        public static string Serialize<T>(T obj)
        {
            var serializer = new XmlSerializer(typeof(T));
            var stringWriter = new StringWriter();
            serializer.Serialize(stringWriter, obj);
            return stringWriter.ToString();
        }

        // wraps exception into validation exception
        public static XDocument Parse(string xmlContent)
        {
            try
            {
                return XDocument.Parse(xmlContent);
            }
            catch(Exception ex)
            {
                throw new ValidationException(ex.Message, ex);
            }
        }

        // throws validation exception if xml is not valid against xsd
        public static void ValidateXmlAgainstXsd(XDocument xDocument, string xsdSchemaPath)
        {
            var schemas = new XmlSchemaSet();
            schemas.Add(null, xsdSchemaPath);

            string message = string.Empty;
            xDocument.Validate(schemas, (o, e) => {
                message += e.Message + Environment.NewLine;
            });

            if (!string.IsNullOrEmpty(message))
                throw new ValidationException(message);
        }
    }
}
