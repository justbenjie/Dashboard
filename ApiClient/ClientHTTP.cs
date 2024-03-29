﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;

namespace ApiClient
{
    public static class ClientHTTP
    {
        // Async get vacancies from server by http request 
        public static async Task<VacanciesInfo> GetVacanciesInfoAsync(string name, string host)
        {
            name = name.Replace("#", "%23");
            return await Task.Run(() =>
            {
                string url = $"http://127.0.0.1:8000/vacancies/{name}";

                //  Using web client
                WebClient client = new WebClient();
                
                string results = Encoding.UTF8.GetString(client.DownloadData(url));
                VacanciesInfo vacanciesInfo = JsonConvert.DeserializeObject<VacanciesInfo>(results);
                return vacanciesInfo;

            });

        }

    }
}
 