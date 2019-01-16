using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HomeLibrary.WebApp.HelperClass
{
    public static class CognitiveServiceHelper
    {


        public static async Task<List<string>> MakeOCRRequest(Stream imageFile)
        {
            string subscriptionKey = "3c3ba64a8c744360aff7b3d503a954a1";
            string uriBase = "https://westeurope.api.cognitive.microsoft.com/vision/v1.0/ocr";///vision/v2.0/ocr
            ImageInfoViewModel responeData = new ImageInfoViewModel();
            var errors = new List<string>();
            List<string> tempList = new List<string>();
            try
            {
                string extractedResult = "";
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

                string requestParameters = "language=pl&detectOrientation=true";
                string uri = uriBase + "?" + requestParameters;

                HttpResponseMessage response;
                byte[] byteData = ReadFully(imageFile);

                using (ByteArrayContent content = new ByteArrayContent(byteData))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    response = await client.PostAsync(uri, content);
                }

                string result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    // The JSON response mapped into respective view model.
                    responeData = JsonConvert.DeserializeObject<ImageInfoViewModel>(result
                     , new JsonSerializerSettings
                     {
                         NullValueHandling = NullValueHandling.Include,
                         Error = delegate (object sender, Newtonsoft.Json.Serialization.ErrorEventArgs earg)
                         {
                             errors.Add(earg.ErrorContext.Member.ToString());
                             earg.ErrorContext.Handled = true;
                         }
                     }
                    );

                    var linesCount = responeData.regions[0].lines.Count;
                    for (int i = 0; i < linesCount; i++)
                    {
                        var wordsCount = responeData.regions[0].lines[i].words.Count;
                        for (int j = 0; j < wordsCount; j++)
                        {
                            //Appending all the lines content into one. 
                            extractedResult += responeData.regions[0].lines[i].words[j].text + " ";
                        }
                        tempList.Add(extractedResult);
                        extractedResult = string.Empty;
                    }

                }
                return tempList;
            }
            catch (Exception e)
            {
                return new List<string>();
            }
        }



        private static byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }


        static byte[] GetImageAsByteArray(string imageFilePath)
        {
            using (FileStream fileStream =
                new FileStream(imageFilePath, FileMode.Open, FileAccess.Read))
            {
                BinaryReader binaryReader = new BinaryReader(fileStream);
                return binaryReader.ReadBytes((int)fileStream.Length);
            }
        }

        public static async void MakeRequest()
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "{subscription key}");

            // Request parameters
            queryString["mode"] = "{string}";
            var uri = "https://westus.api.cognitive.microsoft.com/vision/v2.0/recognizeText?" + queryString;

            HttpResponseMessage response;

            // Request body
            byte[] byteData = Encoding.UTF8.GetBytes("{body}");

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("< your content type, i.e. application/json >");
                response = await client.PostAsync(uri, content);
            }

        }
    }
}