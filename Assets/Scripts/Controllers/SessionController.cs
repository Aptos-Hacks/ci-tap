using Newtonsoft.Json;
using UnityEngine;

public class SessionController : SingletonPersistent<SessionController>
{
    [ReadOnly]
    [SerializeField]
    public int balance = 0;

    [ReadOnly]
    [SerializeField]
    public float totalBonus = 0;

    [ReadOnly]
    [SerializeField]
    public AutoClickerSession autoClickerSession;

    private async void Start()
    {
        var response = await GraphQLLoad.LoadAsync(GraphQLLoad.Query.Zero, new()
        {
            Address = "0x1861a495f12af4b16e97381b8eaeda96c261abe557a720dbdc7c871f444e9ee2"
        });
        Debug.Log(JsonConvert.SerializeObject(response));
    }
}

[System.Serializable]
public class AutoClickerSession
{
    public int level = 0;
}