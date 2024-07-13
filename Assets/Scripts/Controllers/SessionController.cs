using Newtonsoft.Json;
using System;
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

    [SerializeField]
    private float timeInterval = 10f;

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

    public void Save(ApiSave.SavePayload payload)
    {
        BrowserController.Instance.RequestSendPayload("Save", JsonConvert.SerializeObject(payload));
    }

    private void Start()
    {
        LoadAsync();
    }

    private float timer = 0;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeInterval)
        {
            Save(new()
            {
                Balance = balance,
                TotalBonus = totalBonus,
                Timestamp = new DateTime()
            });

            timer -= timeInterval;
        }
    }
}

[System.Serializable]
public class AutoClickerSession
{
    public int level = 0;
}