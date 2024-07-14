using Newtonsoft.Json;
using System;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class BootstrapBrowserController : SingletonPersistent<BootstrapBrowserController>
{
    [DllImport("__Internal")]
    private static extern void SendPayload(string action, string payloadMessage);

    public void RequestSendPayload(string action, string payloadMessage)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
    SendPayload (action, payloadMessage);
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