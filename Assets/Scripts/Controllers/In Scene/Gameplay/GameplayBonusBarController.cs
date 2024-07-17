using Michsky.MUIP;
using UnityEngine;

public class GameplayBonusBarController : Singleton<GameplayBonusBarController>
{
    [SerializeField]
    private float bonusGrowthRate = 1;

    [SerializeField]
    private float bonusDeclineRate = 2f;

    public void AddBonus()
    {
        BootstrapSessionController.Instance.progress += bonusGrowthRate / BootstrapSessionController.Instance.level;
        if (BootstrapSessionController.Instance.progress > 100f)
        {
            BootstrapSessionController.Instance.Level += 1;
            BootstrapSessionController.Instance.progress -= 100;
        }
    }
    public void SubtractBonus()
    {
        var _bonusDeclineRate = bonusDeclineRate * Time.deltaTime;

        if (BootstrapSessionController.Instance.level == 1 && BootstrapSessionController.Instance.progress == 0) return;
        BootstrapSessionController.Instance.progress -= _bonusDeclineRate / BootstrapSessionController.Instance.level;
        if (BootstrapSessionController.Instance.progress < 0f)
        {
            if (BootstrapSessionController.Instance.level == 1)
            {
                BootstrapSessionController.Instance.progress = 0;
                return;
            }
            BootstrapSessionController.Instance.Level -= 1;
            BootstrapSessionController.Instance.progress += 100;
        }
    }

    [SerializeField]
    private ProgressBar progressBar;

    private void RenderProgressBar()
    {
        progressBar.currentPercent = BootstrapSessionController.Instance.progress;
    }

    private void FixedUpdate()
    {
        RenderProgressBar();
    }

    private void Update()
    {
        SubtractBonus();
    }
}