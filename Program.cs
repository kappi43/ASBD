using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Crawler
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var url = args[0];
            var httpClient = new HttpClient();
            var res = await httpClient.GetAsync(url);
            var content = await res.Content.ReadAsStringAsync();
            var regex = new Regex(@"[a-zA-Z0-9]+@([a-zA-Z]*\.)*[a-zA-Z]*");
            var matches = regex.Matches(content);
            foreach(var item in matches)
            {
                Console.WriteLine(item);
            }
        }
    }
}
