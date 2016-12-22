using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DrawFunction
{
    public class DrawFunction : ContentPage
    {
        public DrawFunction()
        {            
        }

        public Stream GetStream(string imageBase64)
        {
            MemoryStream ms = new MemoryStream(Convert.FromBase64String(imageBase64));
            return ms;           
        }

        public async Task<string> GetDrawedBase64String(string imageUri, float x, float y, float len)
        {
            HttpClient client = new HttpClient();
            string requestUri = "http://padaiapi.azurewebsites.net/api/draw/";
            requestUri += String.Format("{0}/{y}/{len}", x, y, len);
            StringContent content = new StringContent(imageUri);
            HttpResponseMessage response = await client.PostAsync(requestUri, content);
            return await response.Content.ReadAsStringAsync();
        }  
    }
}
