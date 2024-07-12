using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
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

    public bool isLoading = false;
    public bool isSaving = false;

    public async void LoadAsync()
    {
        isLoading = true;
        var response = await GraphQLLoad.LoadAsync(GraphQLLoad.Query.Zero, new()
        {
            Address = "0x1861a495f12af4b16e97381b8eaeda96c261abe557a720dbdc7c871f444e9ee2"
        });
        balance = response.Game.Balance;
        totalBonus = response.Game.TotalBonus;
        isLoading = false;
    }

    public async void SaveAsync()
    {
        isSaving = true;

        await ApiSave.SaveAsync(new()
        {
            PublicKey = "0x1d6e4b37cbda1105618acfa86b10bcc92e74baf05df5afce4f4d0297d8d6427b",
            Signature = "0x0cb1a1e1c8a3762282630449cefeb47d803caddba25f0df61d056dc02642afc9037c69a775e6cf96423f88dd06e193f71b1d1e93d1d842ece9c0c93f0e446e0d",
            Payload = new ()
            {   
                Balance = 55,
                TotalBonus = 66f,
                Timestamp = new DateTime(2024, 12, 5),
            }
        });

        isSaving = false;
    }

    private async void UpdateAsync()
    {
        while (true)
        {
            await Task.Delay(TimeSpan.FromSeconds(30));
            SaveAsync();
        }
    }

    private void Start()
    {
        LoadAsync();
        UpdateAsync();
    }
}

[System.Serializable]
public class AutoClickerSession
{
    public int level = 0;
}