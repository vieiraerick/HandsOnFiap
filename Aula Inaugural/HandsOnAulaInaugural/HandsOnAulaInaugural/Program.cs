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

    string charSet = "aeiouáâãàéêíóôõúAEIOUÁÂÃÀÉÊÍÓÔÕÚ";

    do
    {
        Random random = new Random();
        int numAleatorio = random.Next(1, 1000);
        char charAleatorio = charSet[(int)random.Next(0, (int)charSet.Length - 1 )];

        var password1 = numAleatorio.ToString() + charAleatorio.ToString();

        dynamic body = new ExpandoObject();

        body.Key = password1;

        var content = new StringContent(JsonConvert.SerializeObject(body),  null, "application/json"); ;

        var json = await client.PostAsync(
            "https://fiap-inaugural.azurewebsites.net/fiap",
            content
            );

        Console.WriteLine(password1);
        Console.WriteLine(json);

        if ((int)json.StatusCode == 200)
        {
            statusCode = (int)json.StatusCode;
        }
        else
        {

            var password2 = charAleatorio.ToString() + numAleatorio.ToString();

            body.Key = password2;

            content = new StringContent(JsonConvert.SerializeObject(body), null, "application/json"); ;

            json = await client.PostAsync(
                "https://fiap-inaugural.azurewebsites.net/fiap",
                content
                );

            Console.WriteLine(password2);
            Console.WriteLine(json);
        }


    } while (statusCode != 200);

}

    