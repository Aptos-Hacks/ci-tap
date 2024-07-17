using Newtonsoft.Json;
using System;
using System.Runtime.InteropServices;

public class BootstrapBrowserController : SingletonPersistent<BootstrapBrowserController>
{
    [DllImport("__Internal")]
    private static extern void SendPayload(string action, string payloadMessage);

    [DllImport("__Internal")]
    private static extern void Back();

    public void RequestSendPayload(string action, string payloadMessage)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
    SendPayload (action, payloadMessage);
#endif
    }

    public void RequestBack()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
    Back ();
#endif
    }
}

public class PublicKeyAndSignature
{
    [JsonProperty("publicKey")]
    public string PublicKey { get; set; }

    [JsonProperty("signature")]
    public string Signature { get; set; }
}

public abstract class Payload
{
    [JsonProperty("timestamp")]
    public DateTime Timestamp { get; set; }
}