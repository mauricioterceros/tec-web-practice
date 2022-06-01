using BackingServices.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BackingServices.Services
{
    public class IdNumberService
    {
        public async Task<IdNumber> GetIdNumberServiceAsync()
        {
            try
            {
                Console.WriteLine("Pidiendo info de Carnet");
                using (HttpClient client = new HttpClient())
                {
                    string idNumberURL = "https://random-data-api.com/api/id_number/random_id_number";

                    HttpResponseMessage response = await client.GetAsync(idNumberURL);
                    if (response.IsSuccessStatusCode)
                    {
                        string idNumberBody = await response.Content.ReadAsStringAsync();
                        // idNumberBody = "{"id":4470,"uid":"3fb4a9db-213f-45a2-8f3d-b1e1b74b2e31","valid_us_ssn":"198-33-1578","invalid_us_ssn":"666-11-6415"}";
                        // http://jsonviewer.stack.hu
                        IdNumber idNumber = JsonConvert.DeserializeObject<IdNumber>(idNumberBody);
                        return idNumber;
                    }
                    else
                    {
                        throw new Exception("HUBO FALLAS al pedir info de Carnet");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("HUBO FALLAS al pedir info de Carnet");
                Console.WriteLine(ex.Message + ex.StackTrace);
                throw;
            }
        }
    }
}
