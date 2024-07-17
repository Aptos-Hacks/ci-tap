using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameplayCoinController : Singleton<GameplayCoinController>, IPointerClickHandler
{
    private const string TAP_TRIGGER = "Tap";

    [SerializeField]
    private Transform coinReceivedEffectPrefab;

    [SerializeField]
    private Transform container;

    public void AddCoinAndBonusByTapping(Vector3 position, AddCoinAndBonusOptions options = null)
    {
        GameplayBonusBarController.Instance.AddBonus();
        BootstrapSessionController.Instance.balance += BootstrapSessionController.Instance.level;

        var coinReceivedEffect = Instantiate(coinReceivedEffectPrefab, container);
        coinReceivedEffect.GetComponent<CoinReceivedEffectController>().Amount = BootstrapSessionController.Instance.level;
        coinReceivedEffect.transform.position = position;

        if (options != null)
        {
            if (options.IsAutoTapped)
            {
                Debug.Log("Auto tapped!");
                //coinReceivedEffect.GetComponentInChildren<TMP_Text>().color = Color.yellow;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AddCoinAndBonusByTapping(eventData.position);
        TriggerUtility.ExecuteTrigger(transform, TAP_TRIGGER);
    }
}

public class AddCoinAndBonusOptions
{
    public bool IsAutoTapped { get; set; } = false;
}