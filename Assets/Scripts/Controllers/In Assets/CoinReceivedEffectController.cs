using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinReceivedEffectController : MonoBehaviour
{
    [SerializeField]
    private float distance = 100f;

    [SerializeField]
    private float duration = 2f;

    [SerializeField]
    private TMP_Text amountText;

    private CanvasGroup canvasGroup;

    private int amount;
    public int Amount
    {
        get
        {
            return amount;
        }
        set
        {
            amount = value;
            amountText.text = $"+{value}";
        }
    }

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Update()
    {

        ((RectTransform)transform).DOLocalMoveY(transform.localPosition.y + distance, duration).OnStart(() =>
        {
            canvasGroup.DOFade(0f, duration).OnStepComplete(() =>
            {
                Destroy(gameObject);
            });
        });
    }
}
