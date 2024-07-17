using Newtonsoft.Json;
using System;
using UnityEngine;
using UnityEngine.Events;

public class BootstrapSessionController : SingletonPersistent<BootstrapSessionController>
{
    [ReadOnly]
    [SerializeField]
    public string address;

    public UnityEvent updateAddressEvent;
    public string Address
    {
        get { return address; }
        set {
            address = value;
            updateAddressEvent.Invoke();
        }
    }

    [ReadOnly]
    [SerializeField]
    public int previousBalance = 0;

    [ReadOnly]
    [SerializeField]
    public int previousLevel = 1;

    [ReadOnly]
    [SerializeField]
    public float previousProgress = 0;

    [ReadOnly]
    [SerializeField]
    public int previousAutoTapperLevel = 1;

    [ReadOnly]
    [SerializeField]
    public int balance = 0;

    [ReadOnly]
    [SerializeField]
    public int level = 1;
    public UnityEvent updateLevelEvent;
    public int Level
    {
        get { return level; }
        set
        {
            level = value;
            updateLevelEvent.Invoke();
        }
    }

    [ReadOnly]
    [SerializeField]
    public float progress = 0;

    [ReadOnly]
    [SerializeField]
    public int autoTapperLevel = 1;

    public UnityEvent updateAutoTapperLevelEvent;
    public int AutoTapperLevel
    {
        get { return autoTapperLevel; }
        set
        {
            autoTapperLevel = value;
            updateAutoTapperLevelEvent.Invoke();
        }
    }

    [SerializeField]
    private float timeIntervalSeconds = 5f;

    public class SavePayload : Payload
    {
        [JsonProperty("balanceChange")]
        public int BalanceChange { get; set; }

        [JsonProperty("levelChange")]
        public int LevelChange { get; set; }

        [JsonProperty("progressChange")]
        public float ProgressChange { get; set; }

        [JsonProperty("autoTapperLevelChange")]
        public int AutoTapperLevelChange { get; set; }
    }

    public void Save()
    {
        BootstrapBrowserController.Instance.RequestSendPayload("Save", JsonConvert.SerializeObject(new SavePayload()
        {
            BalanceChange = balance - previousBalance,
            LevelChange = level - previousLevel,
            ProgressChange = progress - previousProgress,
            AutoTapperLevelChange = AutoTapperLevel - previousAutoTapperLevel,
            Timestamp = new()
        }));

        previousBalance = balance;
        previousLevel = level;
        previousProgress = progress;
        previousAutoTapperLevel = autoTapperLevel;
    }

    public class LoadParams
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("balance")]
        public int Balance { get; set; }

        [JsonProperty("level")]
        public int Level { get; set; }

        [JsonProperty("progress")]
        public float Progress { get; set; }

        [JsonProperty("autoTapperLevel")]
        public int AutoTapperLevel { get; set; }
    }

    private bool isLoaded = false;
    public void Load(string serializedParams)
    {
        if (isLoaded) return;

        var _params = JsonConvert.DeserializeObject<LoadParams>(serializedParams);
        Address = _params.Address;

        previousBalance = _params.Balance;
        balance = _params.Balance;

        previousLevel = _params.Level;
        level = _params.Level;

        previousProgress = _params.Progress;
        progress = _params.Progress;

        previousAutoTapperLevel = _params.AutoTapperLevel;
        autoTapperLevel = _params.AutoTapperLevel;

        isLoaded = true;
    }

    private float timer = 0;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeIntervalSeconds)
        {
            Save();
            timer -= timeIntervalSeconds;
        }
    }

    private void Start()
    {
        updateAddressEvent = new();
        updateLevelEvent = new();
        updateAutoTapperLevelEvent = new();
    }
}