using System.Text.RegularExpressions;

namespace AptOnline.Infrastructure.Helpers
{
    public static class FarmasiHelper
    {
        public static (string Signa1, string Signa2) GenSigna(string signa)
        {
            var signa1 = "";
            var signa2 = "";

            if (!string.IsNullOrWhiteSpace(signa))
            {
                var matches = Regex.Matches(signa, @"\d+")
                                   .Cast<Match>()
                                   .Take(2)
                                   .Select(m => m.Value)
                                   .ToArray();

                if (matches.Length > 0) signa1 = matches[0];
                if (matches.Length > 1) signa2 = matches[1];
            }

            return (signa1, signa2);
        }
    }
}
