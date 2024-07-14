using Newtonsoft.Json;
using System;
using UnityEngine;

public class BootstrapSessionController : SingletonPersistent<BootstrapSessionController>
{
    [ReadOnly]
    [SerializeField]
    public string address;

    [ReadOnly]
    [SerializeField]
    public int previousBalance = 0;

    [ReadOnly]
    [SerializeField]
    public float previousTotalBonus = 0;

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
    private float timeInterval = 5f;

    public class SavePayload : Payload
    {
        [JsonProperty("balanceChange")]
        public int BalanceChange { get; set; }

        [JsonProperty("bonusChange")]
        public float BonusChange { get; set; }
    }

    public void Save(SavePayload payload)
    {
        previousBalance = balance;
        previousTotalBonus = totalBonus;

        BootstrapBrowserController.Instance.RequestSendPayload("Save", JsonConvert.SerializeObject(payload));
    }

    public class LoadParams
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("balance")]
        public int Balance { get; set; }

        [JsonProperty("totalBonus")]
        public float TotalBonus { get; set; }
    }

    public void Load(string serializedParams)
    {
        var _params = JsonConvert.DeserializeObject<LoadParams>(serializedParams);
        address = _params.Address;
        
        previousBalance = _params.Balance;
        balance = _params.Balance;

        previousTotalBonus = _params.TotalBonus;
        totalBonus = _params.TotalBonus;
    }

    private float timer = 0;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeInterval)
        {
            Save(new()
            {
                BalanceChange = balance - previousBalance,
                BonusChange = totalBonus - previousTotalBonus,
                Timestamp = new DateTime()
            });

            timer -= timeInterval;
        }
    }
}

[Serializable]
public class AutoClickerSession
{
    public int level = 0;
}