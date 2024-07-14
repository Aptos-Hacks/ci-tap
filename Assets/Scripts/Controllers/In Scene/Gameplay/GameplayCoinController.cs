using System;
using TMPro;
using UnityEngine;
using UnityEngine.Accessibility;

public class GameplayCoinController : Singleton<GameplayCoinController>
{
    private const string TAP_TRIGGER = "Tap";

    [SerializeField]
    private Transform coinReceivedUxPrefab;

    [SerializeField]
    private Transform container;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        MobileUtility.HandleTouch(transform, (touchPostion) =>
        {
            AddCoinAndBonus(touchPostion);
            TriggerUtility.ExecuteTrigger(transform, TAP_TRIGGER);
        });
    }

    public void AddCoinAndBonus(Vector3? touchPosition = null, AddCoinAndBonusOptions options = null)
    {
        GameplayBonusBarController.Instance.AddCoinAndBonus();
        var coinReceivedUx = Instantiate(coinReceivedUxPrefab, container);
        if (touchPosition.HasValue)
        {
            ((RectTransform)coinReceivedUx.transform).position = touchPosition.Value;
        }
        
        if (options != null)
        {
            if (options.IsAutoTapped)
            {
                coinReceivedUx.GetComponentInChildren<TMP_Text>().color = Color.yellow;
            }
        }
    }
}

public class AddCoinAndBonusOptions
{
    public bool IsAutoTapped { get; set; } = false;
}