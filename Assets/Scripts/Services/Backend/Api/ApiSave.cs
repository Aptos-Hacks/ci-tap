using Newtonsoft.Json;
using System.Threading.Tasks;
public static class ApiSave
{
    public class SavePayload : Payload
    {
        [JsonProperty("balance")]
        public int Balance { get; set; }

        [JsonProperty("totalBonus")]
        public float TotalBonus { get; set; }
    }

    public class SaveRequestBody : ISignature<SavePayload>
    {
        public SavePayload Payload { get; set; }
        public string Signature { get; set; }
        public string PublicKey { get; set; }
    }

    public class SaveResponseData : IBaseApiResponse
    {
        public string Message { get; set; }
    }

    public static async Task<SaveResponseData> SaveAsync(SaveRequestBody body, HandleApiException HandleApiException = null)
    {
        return await ApiUtility.PostAsync<SaveRequestBody, SaveResponseData>($"{Constants.Urls.Backend.apiUrl}/v1/game/save", body, HandleApiException);
    }
}
