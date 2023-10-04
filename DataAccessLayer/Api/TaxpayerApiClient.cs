using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace DataAccessLayer.Api
{
    public class TaxpayerApiClient : ITaxpayerApiClient
    {
        private HttpClient _httpClient;
        private string _apiUrl;

        public TaxpayerApiClient()
        {
            _httpClient = new HttpClient();
        }

        public string ApiUrl { set { _apiUrl = value; } }

        public async Task<List<TaxpayerResponse>> SearchTaxpayer(string name)
        {
            try
            {
                var requestBody = new
                {
                    operation = "=",
                    name = name
                };

                var response = await _httpClient.PostAsJsonAsync(_apiUrl, requestBody);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadFromJsonAsync<List<TaxpayerResponse>>();
                    return jsonResponse;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
