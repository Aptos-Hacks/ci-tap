using Newtonsoft.Json;

public interface ISignature<PayloadType> where PayloadType : class, new()
{
    [JsonProperty("payload")]
    public PayloadType Payload { get; set; }

    [JsonProperty("signature")]
    public string Signature { get; set; }

    [JsonProperty("publicKey")]
    public string PublicKey { get; set; }
}

public interface IBaseApiResponse<DataType> where DataType : class, new()
{
    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("data")]
    public DataType Data { get; set; }
}

public interface IBaseApiResponse 
{
    [JsonProperty("message")]
    public string Message { get; set; }
}