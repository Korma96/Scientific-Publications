namespace ScientificPublications.Common.Extensions
{
    public static class StringExtensions
    {
        public static string UrlCombine(this string baseUrl, string urlSuffix)
        {
            baseUrl = baseUrl.TrimEnd('/');
            urlSuffix = urlSuffix.TrimStart('/');
            return string.Format("{0}/{1}", baseUrl, urlSuffix);
        }
    }
}
