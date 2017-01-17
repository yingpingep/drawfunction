using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;

namespace DrawFunction
{
    public class Draw : ContentPage
    {
        public Stream GetStream(string imageBase64)
        {
            // Make a image from base64 string to a stream
            MemoryStream ms = new MemoryStream(Convert.FromBase64String(imageBase64));
            return ms;           
        }

        public async Task<string> GetDrawedImageAsync(MyDataType myData)
        {
            try
            {
                // Convert a JSON object to a string
                string mydataJson = JsonConvert.SerializeObject(myData);

                // Create a http client to access web api
                HttpClient client = new HttpClient();
                // string requestUri = "http://localhost:13120/";
                string requestUri;

                if (myData.emoes == null)
                {
                    requestUri = "http://api4ws.azurewebsites.net/api/draw/";
                }
                else
                {
                    requestUri = "http://api4ws.azurewebsites.net/api/edraw/";
                }

                // Send a request with custom content to requestUri
                StringContent content = new StringContent(mydataJson);
                HttpResponseMessage response = await client.PostAsync(requestUri, content);

                // Get the response content and convert it to a string type
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }  
    }

    /* MyDataType
     * Use to reformat the data from cognitive service
     * and add some custom information
     */
    public class MyDataType
    {
        public string imageuri { get; set; }
        public List<Rect> rects = new List<Rect>();
        public List<Age> ages = new List<Age>();        
        public List<string> emoes = new List<string>();
    }

    public class Rect
    {
        public Rect(int x, int y, int len)
        {
            this.x = x;
            this.y = y;
            this.len = len;
        }
        public int x { get; set; }
        public int y { get; set; }
        public int len { get; set; }
    }

    public class Age
    {
        public Age(double age)
        {
            this.age = age;
        }
        public double age { get; set; }
    }
}
