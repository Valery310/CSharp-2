using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Properties;

namespace WpfApp1
{
    public static class WebAPI
    {
        public static event EventHandler<EventArgsError> Error;
        static HttpClient client = new HttpClient();

        public static void InitUri() 
        {
            client.BaseAddress = new Uri(Settings.Default.connectionString);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static async Task<Departments> GetDepatments() 
        {

            Departments departments = null;
            try
            {
                HttpResponseMessage httpResponse = await client.GetAsync(client.BaseAddress+"api/OrgStructure");
                if (httpResponse.IsSuccessStatusCode)
                {
                    departments = await httpResponse.Content.ReadAsAsync<Departments>();                   
                }
            }
            catch (Exception ex)
            {
                Error(null, new EventArgsError("Ошибка получения данных!"));
            }
            return departments;
        }
    }
}
