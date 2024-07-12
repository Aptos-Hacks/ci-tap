using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

public delegate void HandleApiException(HttpRequestException ex);
public static class ApiUtility
{
    public static async Task<TResponseData> PostAsync<TRequestBody, TResponseData>(
        string url,
        TRequestBody body,
        HandleApiException HandleApiException = null
        ) where TRequestBody : class where TResponseData : class
    {
        try
        {
            using var client = new HttpClient();
            string jsonBody = JsonConvert.SerializeObject(body);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(url, content);

            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<TResponseData>(responseBody);

            return responseData;
        }
        catch (HttpRequestException ex)
        {
            if (HandleApiException != null)
            {
                HandleApiException(ex);
            }
            
            return null;
        }
    }
}