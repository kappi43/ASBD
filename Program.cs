using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Crawler
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            if (args.Length < 1)
            {
                throw new ArgumentNullException();
            }
            var url = args[0];
            var httpClient = new HttpClient();
            Uri uriResult;
            var isUrl = Uri.TryCreate(url, UriKind.Absolute, out uriResult);
            if (!isUrl)
            {
                throw new ArgumentException();
            }
            var res = await httpClient.GetAsync(url);
            if (!res.IsSuccessStatusCode)
            {
                throw new Exception("Bład w czasie pobierania strony");
            }
            httpClient.Dispose();
            var content = await res.Content.ReadAsStringAsync();
            var regex = new Regex(@"[a-zA-Z0-9_]+@([a-zA-Z]*\.)*[a-zA-Z]*");
            var matches = regex.Matches(content);
            var set = new HashSet<string>();
            if (matches.Count < 1)
            {
                Console.WriteLine("Nie znaleziono adresow email");
            }
            foreach (var item in matches)
            {
                if (!set.Contains(item.ToString()))
                {
                    Console.WriteLine(item);
                    set.Add(item.ToString());
                }
            }
        }
    }
}
