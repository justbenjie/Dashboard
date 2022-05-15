using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;


namespace TestClient
{
    public class Vacancies
    {
        public Dictionary<string, int> Skills { get; set; }
        public Dictionary<string, double> Salary { get; set; }
        public Dictionary<string, int> Schedule { get; set; }
        public Dictionary<string, int> Experience { get; set; }

    }

    internal class Program
    {
        /*static HttpClient client = new HttpClient();

        static async Task<Vacancies> GetProductAsync(string path)
        {
            Vacancies vacancies = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                vacancies = await response.Content.ReadAsAsync<Vacancies>();
            }
            return vacancies;
        }

        static async Task RunAsync()
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri("http://127.0.0.1:8000/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("vacancies/python"));

        }*/

        static void Main(string[] args)
        {
            string url = "http://127.0.0.1:8000/vacancies/python";

            //  Using web client
            WebClient client = new WebClient();
            string results = Encoding.UTF8.GetString(client.DownloadData(url));

            Vacancies Toons = JsonConvert.DeserializeObject<Vacancies>(results);
            foreach (var key in Toons.Schedule.Keys)
            {
                Console.WriteLine(key);
            }
        }

        
    }
}

