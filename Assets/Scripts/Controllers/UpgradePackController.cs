using UnityEngine;
using UnityEngine.UI;

public class UpgradePackController : Singleton<UpgradePackController>
{
    [SerializeField]
    private Button autoTapButton;

    private void Start()
    {
        autoTapButton.onClick.AddListener(() =>
        {
            AutoTapperController.Instance.Upgrade();
        });
    }
} 