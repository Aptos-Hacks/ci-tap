using UnityEngine;
using UnityEngine.UI;

public class GamplayBottomBarController : Singleton<GamplayBottomBarController>
{
    [SerializeField]
    private Button upgradeButton;

    [SerializeField]
    private Button saveButton;

    [SerializeField]
    private Button settingButton;

    [SerializeField]
    private Button backButton;

    void Start()
    {
        upgradeButton.onClick.AddListener(() => {
            BootstrapLoadingSceneManagerController.Instance.LoadSceneAdditive(SceneName.Upgrade);
        });

        saveButton.onClick.AddListener(() => {
            BootstrapSessionController.Instance.Save();
        });

        backButton.onClick.AddListener(() => {
            BootstrapBrowserController.Instance.RequestBack();
        });
    }
}
