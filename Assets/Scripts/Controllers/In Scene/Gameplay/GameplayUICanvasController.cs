using System.Collections;
using TMPro;
using UnityEngine;

public class GameplayUICanvasController : Singleton<GameplayUICanvasController>
{
    [SerializeField]
    private TMP_Text balanceText;

    [SerializeField]
    private TMP_Text addressText;

    [SerializeField]
    private TMP_Text levelText;

    private void FixedUpdate()
    {
        var isSucess = int.TryParse(balanceText.text, out int balance);
        balance = isSucess ? balance : 0;

        if (balance != BootstrapSessionController.Instance.balance)
        {
            UpdateBalance();
        }
    }

    private void UpdateBalance()
    {
        balanceText.text = BootstrapSessionController.Instance.balance.ToString();
    }
    private void UpdateAddress()
    {
        addressText.text = TruncateUtility.TrucateAddress(BootstrapSessionController.Instance.address);
    }

    private void UpdateLevel()
    {
        levelText.text = $"x{BootstrapSessionController.Instance.level}";
    }

    private IEnumerator Start()
    {
        yield return new WaitUntil(() => BootstrapSessionController.Instance.updateAddressEvent != null || BootstrapSessionController.Instance.updateLevelEvent != null);
        
        UpdateAddress();
        UpdateLevel();
        UpdateBalance();

        BootstrapSessionController.Instance.updateAddressEvent.AddListener(UpdateAddress);
        BootstrapSessionController.Instance.updateLevelEvent.AddListener(UpdateLevel);
    }
}

