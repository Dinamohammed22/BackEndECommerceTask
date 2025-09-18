using KOG.ECommerce.Common.DTOs;
using Newtonsoft.Json;
using System.Text;

namespace KOG.ECommerce.Helpers
{
    public static class SMSHelper
    {
        private static readonly HttpClient client = new HttpClient();
        public static async Task<SMSResponse> SendSmsAsync(string phone, string message)
        {
            string url = $"https://api.epusheg.com/api/v2/send_bulk?username=kog@epushagency.com&password=8ye2s/c1bzs4g&api_key=Gjg2-VB9U-SAFW-829A&message={message}&from=KOG&to={phone}";
           

            var parameters = new Dictionary<string, string>
             {
                 { "username", "kog@epushagency.com" },
                 { "password", "8ye2s/c1bzs4g" },
                 { "api_key", "Gjg2-VB9U-SAFW-829A" },
                 { "message", message },
                 { "from", "KOG" },
                 { "to", phone }
            };

            string jsonContent = JsonConvert.SerializeObject(parameters);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            try
            {
                // Send POST request
                HttpResponseMessage response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    SMSResponse smsResponse = JsonConvert.DeserializeObject<SMSResponse>(responseData);
                    smsResponse.Success = true;

                    return smsResponse;
                }
                else
                {
                 
                    return new SMSResponse
                    {
                        Success = false,
                        ErrorMessage = response.ReasonPhrase 
                    };
                }
            }
            catch (Exception ex)
            {
             
                return new SMSResponse
                {
                    Success = false,
                    ErrorMessage = ex.Message 
                };
            }
        }
    }
}
