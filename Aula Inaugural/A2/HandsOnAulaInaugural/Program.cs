using Newtonsoft.Json;
using System.Dynamic;
using System.Net.Http.Headers;

using HttpClient client = new();
client.DefaultRequestHeaders.Accept.Clear();
client.DefaultRequestHeaders.Accept.Add(
    new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

await ProcessRepositoriesAsync(client);

static async Task ProcessRepositoriesAsync(HttpClient client)
{
    int statusCode = 0;

    int valorFinal = 750;

    do
    {
        var password1 = valorFinal.ToString() + "a";

        dynamic body = new ExpandoObject();

        body.Key = password1;

        var content = new StringContent(JsonConvert.SerializeObject(body), null, "application/json"); ;

        var json = await client.PostAsync(
            "https://fiap-inaugural.azurewebsites.net/fiap",
            content
            );

        Console.WriteLine(password1);
        Console.WriteLine(json);

        valorFinal--;
    } while (statusCode != 200);

}

