using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Security.Policy;

namespace LanchesMac.Utils
{
    public static class DataExtractions
    {
        public static string[] GetExtractionUrl(string url)
        {
            string[] fields = url.Split('/');
            return fields;
        }
    }
}
