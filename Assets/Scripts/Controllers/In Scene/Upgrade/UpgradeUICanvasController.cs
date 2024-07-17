using Michsky.MUIP;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUICanvasController : Singleton<UpgradeUICanvasController>
{
    [SerializeField]
    private TMP_Text autoTapperLevelText;

    [SerializeField]
    private ButtonManager upgradeAutoTapperButton;

    [SerializeField]
    private TMP_Text upgradeAutoTapperPriceText;

    [SerializeField]
    private TMP_Text balanceText;

    [SerializeField]
    private Button closeButton;

    private void UpdateBalance()
    {
        balanceText.text = $"{BootstrapSessionController.Instance.balance} tAptos";
    }

    private void UpdateAutoTapperLevel()
    {
        autoTapperLevelText.text = $"Lv.{BootstrapSessionController.Instance.autoTapperLevel}";
    }

    private void UpdateAutoTapperPrice()
    {
        upgradeAutoTapperPriceText.text = $"{BootstrapSessionController.Instance.autoTapperLevel * GameplayAutoTapperController.Instance.unitPrice} tAptos";
    }
    private IEnumerator Start()
    {
        closeButton.onClick.AddListener(() =>
        {
            BootstrapLoadingSceneManagerController.Instance.UnloadSceneAdditive(SceneName.Upgrade);
        });

        yield return new WaitUntil(() => BootstrapSessionController.Instance.updateAutoTapperLevelEvent != null);
        
        UpdateBalance();
        UpdateAutoTapperLevel();
        UpdateAutoTapperPrice();

        BootstrapSessionController.Instance.updateAutoTapperLevelEvent.AddListener(UpdateAutoTapperLevel);
        BootstrapSessionController.Instance.updateAutoTapperLevelEvent.AddListener(UpdateAutoTapperPrice);

        upgradeAutoTapperButton.onClick.AddListener(() =>
        {
            GameplayAutoTapperController.Instance.Upgrade();
        });
    }

    private void Update()
    {
        var isSucess = int.TryParse(balanceText.text, out int balance);
        balance = isSucess ? balance : 0;

        if (balance != BootstrapSessionController.Instance.balance)
        {
            UpdateBalance();
        }
    }
} 