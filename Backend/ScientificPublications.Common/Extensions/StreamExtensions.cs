using System.IO;

namespace ScientificPublications.Common.Extensions
{
    public static class StreamExtensions
    {
        public static string StreamToString(this Stream stream)
        {
            var result = string.Empty;
            using (var reader = new StreamReader(stream))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }
    }
}
