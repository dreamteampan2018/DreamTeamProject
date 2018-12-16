//Poprawione - dodać rzutowanie

using System;
using System.Net.Http.Headers;
using System.Text;
using System.Net.Http;
using System.Web;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;

namespace CSHttpClientSample
{
    static class Program
    {
        static void Main()
        {
            MakeRequest().GetAwaiter().GetResult();
            Console.WriteLine("Hit ENTER to exit...");
            Console.ReadLine();
        }

        static async Task MakeRequest()
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "3b7b8ea3ea42484c9ca8fc9ceae87aae");

            // Request parameters
            queryString["mode"] = "Printed";
            var uri = "https://westcentralus.api.cognitive.microsoft.com/vision/v2.0/recognizeText?" + queryString;

            HttpResponseMessage response;

            // Request body
            byte[] byteData = Encoding.UTF8.GetBytes("{\"url\":\"https://i.ytimg.com/vi/CLazYivtxlo/maxresdefault.jpg\"}");

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response = await client.PostAsync(uri, content);

                IEnumerable<string> str = response.Headers.GetValues("Operation-Location");

                foreach (var item in str)
                {
                    string responseContent;
                    do
                    {
                        HttpResponseMessage message = await client.GetAsync(item);
                        responseContent = await message.Content.ReadAsStringAsync();
                        Console.WriteLine(responseContent);
                        Thread.Sleep(500);
                    }
                    while (responseContent == "{\"status\":\"Running\"}");
                    responseContent.ToString();
                }


            }

        }
    }
}



public class Rootobject
{
    public string status { get; set; }
    public Recognitionresult recognitionResult { get; set; }
}

public class Recognitionresult
{
    public Line[] lines { get; set; }
}

public class Line
{
    public int[] boundingBox { get; set; }
    public string text { get; set; }
    public Word[] words { get; set; }
}

public class Word
{
    public int[] boundingBox { get; set; }
    public string text { get; set; }
}


