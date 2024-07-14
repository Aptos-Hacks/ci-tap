using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinReceivedEffectController : MonoBehaviour
{
    [SerializeField]
    private float distance = 100;
    
    [SerializeField]
    private float duration = 2f;

    [SerializeField]
    private TMP_Text amountText;

    private CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        amountText.text = $"+{GameplayBonusBarController.Instance.level}";
    }

    public void Update()
    {
        ((RectTransform)transform).DOAnchorPosY(distance, duration).OnStart(() =>
        {
            canvasGroup.DOFade(0f, duration).OnStepComplete(() =>
            {
                Destroy(gameObject);
            });
        });
    }
}
