using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;

namespace DrawFunction
{
    public class Draw : ContentPage
    {
        public Stream GetStream(string imageBase64)
        {
            MemoryStream ms = new MemoryStream(Convert.FromBase64String(imageBase64));
            return ms;           
        }

        public async Task<string> GetDrawedImage(MyDataType mydata)
        {
            try
            {
                string mydataJson = JsonConvert.SerializeObject(mydata);

                HttpClient client = new HttpClient();
                string requestUri = "http://api4ws.azurewebsites.net/api/draw/";                
                // string requestUri = "http://localhost:13120/api/draw/";
                StringContent content = new StringContent(mydataJson);
                HttpResponseMessage response = await client.PostAsync(requestUri, content);
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }  
    }

    public class MyDataType
    {
        public string imageuri { get; set; }
        public List<Rect> rects = new List<Rect>();
        public List<Age> ages = new List<Age>();
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
