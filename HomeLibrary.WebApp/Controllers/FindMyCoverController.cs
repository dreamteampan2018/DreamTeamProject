using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Web;
using System.Threading;
using System.Text;
using HomeLibrary.WebApp.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace HomeLibrary.WebApp.Controllers
{
    public class FindMyCoverController : Controller
    {
        List<string> resoult = new List<string>(); //string list for resoult words list.
        string ImagePath;

        public IActionResult Index()
        {
            return View();
        }

        //Uploag image services:
        private IHostingEnvironment _environment;

        public FindMyCoverController(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        //Open FileOpenDialog to select image file
        [HttpPost]
        public async Task<IActionResult> Index(ICollection<IFormFile> files)
        {
            var uploads = Path.Combine(_environment.WebRootPath, "uploads");
            
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    //using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))   
                    //using (var fileStream = new FileStream(Path.Combine(uploads, "tempImageFile.jpg"), FileMode.Create))
                    ImagePath = Path.Combine(uploads, "tempImageFile.jpg");
                    using (var fileStream = new FileStream(ImagePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }
            }

            MakeRequest().GetAwaiter().GetResult(); //Azure running
            //TODO:
            // Zamiast zwracać "listę słów " - (listę stringów resoult) do wyswietlanego widoku (ViewBag.Resoult =) trzeba ją przesłać do funkcji która sprawdzi w zasobach tytułów i autorów
            //i zwóruci nam listę z ID książek w których występuje słowa z "listy słów". Potem posługując się słownikie wyodrębniamy ID
            //książki / książek, które mają najwięcej słów z listy (najbardziej pasują). Potem trzeba zwrócić listę tych książek do widoku
            // przez ViewBag.Resoult = .... 

            //czyli ViewBag.Resoult = FunkcjaPrzeszukujacaBazeKsiazekKtoraZwracaListKsiązękZawierajacychSlowaZListy(resoult);
            ViewBag.Resoult = resoult; //send resoult list wiew
           return View();
        }


        public IActionResult FindMyCover()
        {
            
            return View();
        }

        
        
        // Use Azure to convert picture to string://////////////////////////////
         async Task MakeRequest() 
        {
            
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "50f12152bbca4893b00fcc7d2731a5fc"); //klucz 50f12152bbca4893b00fcc7d2731a5fc ważny do 14.01.2019 

            // Request parameters
            queryString["mode"] = "Printed";
            var uri = "https://westcentralus.api.cognitive.microsoft.com/vision/v2.0/recognizeText?" + queryString;

            //KROWAA
            HttpResponseMessage response;
            // Request body  !!!!!!!!!!!!!!!!!!!!!!!!!!
            //byte[] byteData = Encoding.UTF8.GetBytes("{\"url\":\"https://i.ytimg.com/vi/CLazYivtxlo/maxresdefault.jpg\"}"); //only for http link                                   
            byte[] byteData = GetImageAsByteArray(ImagePath);
            byte[] GetImageAsByteArray(string imageFilePath)
            {
                // Open a read-only file stream for the specified file.
                using (FileStream fileStream =
                    new FileStream(imageFilePath, FileMode.Open, FileAccess.Read))
                {
                    // Read the file's contents into a byte array.
                    BinaryReader binaryReader = new BinaryReader(fileStream);
                    return binaryReader.ReadBytes((int)fileStream.Length);
                }
            }

            //
            //ImagePath =xerver.MapPath("~/");

          

            using (var content = new ByteArrayContent(byteData))
            {
                //content.Headers.ContentType = new MediaTypeHeaderValue("application/json"); //only for http link
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response = await client.PostAsync(uri, content);

                IEnumerable<string> str = response.Headers.GetValues("Operation-Location");                             
                
                foreach (var item in str)
                {
                    string responseContent;
                    do
                    {
                        HttpResponseMessage message = await client.GetAsync(item);
                        responseContent = await message.Content.ReadAsStringAsync();                        
                        Thread.Sleep(500);
                    }
                    while (responseContent == "{\"status\":\"Running\"}");                    
                    Rootobject @object = Newtonsoft.Json.JsonConvert.DeserializeObject<Rootobject>(responseContent);
                    
                    foreach (Line _line in @object.recognitionResult.lines)
                    {
                        foreach (Word _word in _line.words)
                        { resoult.Add(_word.text); }                        
                    }                                     
                    
                }                
            }
            
        }               
    }
}