using UnityEngine;
using UnityEngine.EventSystems;

public class CoinFallController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private float rotateSpeed = 100f;

    [SerializeField]
    private int amount = 100;

    [SerializeField]
    private Transform coinReceivedEffectPrefab;

    private Transform container;

    private void Start()
    {
        container = FindFirstObjectByType<GameplayCoinReceivedEffectContainerCanvasController>().transform;
    }

    private void Update()
    {
        ((RectTransform)transform).Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        BootstrapSessionController.Instance.balance += amount;

        var coinReceivedEffect = Instantiate(coinReceivedEffectPrefab, container);
        coinReceivedEffect.GetComponent<CoinReceivedEffectController>().Amount = amount;
        coinReceivedEffect.position = eventData.position;

        Destroy(gameObject);
    }
}