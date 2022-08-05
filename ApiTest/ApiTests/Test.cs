using System;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace ApiTest1
{
    class Program
    {
        public string Get(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        public string Post()
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://restful-booker.herokuapp.com/auth");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"username\":\"admin\"," +
                              "\"password\":\"password123\"}";

                streamWriter.Write(json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                return streamReader.ReadToEnd();
            }
        }

        static void Main()
        {
            var execute = new Program();
            Console.WriteLine(execute.Post());
            //Console.WriteLine(execute.Get("https://restful-booker.herokuapp.com/booking"));
        }
    }
}