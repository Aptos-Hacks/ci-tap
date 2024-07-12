using Newtonsoft.Json;
using System;

public abstract class Payload
{
    [JsonProperty("timestamp")]
    public DateTime Timestamp { get; set; }
}