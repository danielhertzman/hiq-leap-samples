using HiQ.Leap.Samples.RandomIntegration.Contracts;

namespace HiQ.Leap.Samples.RandomIntegration;

public class AdviceMemeIntegration : IRandomIntegration
{
    public async Task InvokeRandomIntegrationAsync()
    {
        var client = new HttpClient();
        var response = await client.GetAsync("https://apimeme.com/meme?meme=Advice-Doge&top=look+up&bottom=tdd");

        if (!response.IsSuccessStatusCode)
        {
            // log not successful
        }

        await using var fileStream = new FileStream("C:\\Users\\DanielHe\\Downloads\\adviceDoge.jpg", FileMode.Create, FileAccess.Write);
        await fileStream.WriteAsync(await response.Content.ReadAsByteArrayAsync());
    }
}