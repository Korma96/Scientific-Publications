using System.IO;
using System.Threading.Tasks;

namespace ScientificPublications.Common.Utility
{
    public static class FileUtility
    {
        public static async Task<MemoryStream> ReadAsStreamAsync(string filename)
        {
            var memory = new MemoryStream();

            using (var stream = new FileStream(filename, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return memory;
        }
    }
}
