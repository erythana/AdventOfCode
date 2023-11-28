namespace AdventOfCodeLib;

public class WebInputResolver(string sessionCookie)
{
    private const string AdventOfCodeUri = "https://adventofcode.com/{0}/day/{1}/input";

    public async Task<string> GetInputFor(int year, int day)
    {
        using var clientHandler = new HttpClientHandler();
        using var client = new HttpClient(clientHandler);
        client.DefaultRequestHeaders.Add("Cookie", $"session={sessionCookie}");
        return await client.GetStringAsync(string.Format(AdventOfCodeUri, year, day));
    }
}