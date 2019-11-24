using ScientificPublications.Common.Utility;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ScientificPublications.Common.Helpers
{
    public static class HttpHelper
    {
        public static async Task Post<T>(string url, T contentValue, string authorization = null)
        {
            using (var client = new HttpClient())
            {
                var content = new StringContent(XmlUtility.Serialize(contentValue), Encoding.UTF8, Constants.XmlContentType);
                var result = await client.PostAsync(url, content);
                result.EnsureSuccessStatusCode();
            }
        }

        public static async Task Put<T>(string url, T stringValue)
        {
            using (var client = new HttpClient())
            {
                var content = new StringContent(XmlUtility.Serialize(stringValue), Encoding.UTF8, Constants.XmlContentType);
                var result = await client.PutAsync(url, content);
                result.EnsureSuccessStatusCode();
            }
        }

        public static async Task<T> Get<T>(string url)
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync(url);
                result.EnsureSuccessStatusCode();
                string resultContentString = await result.Content.ReadAsStringAsync();
                T resultContent = XmlUtility.Deserialize<T>(resultContentString);
                return resultContent;
            }
        }

        public static async Task Delete(string url)
        {
            using (var client = new HttpClient())
            {
                var result = await client.DeleteAsync(url);
                result.EnsureSuccessStatusCode();
            }
        }
    }
}
