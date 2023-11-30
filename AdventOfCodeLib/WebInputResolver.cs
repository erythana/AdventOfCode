using System.Net.Http;
using System.Threading.Tasks;

namespace AdventOfCodeLib
{
    public class WebInputResolver
    {
        private readonly string _sessionCookie;

        public WebInputResolver(string sessionCookie)
        {
            _sessionCookie = sessionCookie;
        }

        private const string AdventOfCodeUri = "https://adventofcode.com/{0}/day/{1}/input";

        public async Task<string> GetInputFor(int year, int day)
        {
            using var clientHandler = new HttpClientHandler();
            using var client = new HttpClient(clientHandler);
            client.DefaultRequestHeaders.Add("Cookie", $"session={_sessionCookie}");
            return await client.GetStringAsync(string.Format(AdventOfCodeUri, year, day));
        }
    }
}