using UnityEngine;
public class AutoTapperController : Singleton<AutoTapperController>
{
    [SerializeField]
    private int unitPrice = 50;

    [SerializeField]
    private float autoClickBaseIntervalInSeconds = 10f;

    public void Upgrade()
    {
        var price = BootstrapSessionController.Instance.autoClickerSession.level * unitPrice;
        if (BootstrapSessionController.Instance.balance < price) return;

        BootstrapSessionController.Instance.balance -= price;
        BootstrapSessionController.Instance.autoClickerSession.level += 1;
    }

    private float timer = 0;
    private void Update()
    {   
        if (BootstrapSessionController.Instance.autoClickerSession.level == 0)
        {
            return;
        }

        timer += Time.deltaTime;

        if (timer >= autoClickBaseIntervalInSeconds / BootstrapSessionController.Instance.autoClickerSession.level)
        {
            GameplayCoinController.Instance.AddCoinAndBonus(default, new()
            {
                IsAutoTapped = true
            });

            timer = 0;
        }
    }
}