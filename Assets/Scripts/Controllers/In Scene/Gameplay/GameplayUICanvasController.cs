using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameplayUICanvasController : Singleton<GameplayUICanvasController>
{
    [SerializeField]
    private TMP_Text balanceText;

    [SerializeField]
    private TMP_Text addressText;

    private void FixedUpdate()
    {
        balanceText.text = BootstrapSessionController.Instance.balance.ToString();
    }

    private void Start()
    {
        addressText.text = TruncateUtility.TrucateAddress(BootstrapSessionController.Instance.address);
    }
}

