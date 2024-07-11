using UnityEngine;
public class AutoTapperController : Singleton<AutoTapperController>
{
    [SerializeField]
    private int unitPrice = 50;

    [SerializeField]
    private float autoClickBaseIntervalInSeconds = 10f;

    public void Upgrade()
    {
        var price = SessionController.Instance.autoClickerSession.level * unitPrice;
        if (SessionController.Instance.balance < price) return;

        SessionController.Instance.balance -= price;
        SessionController.Instance.autoClickerSession.level += 1;
    }

    private float timer = 0;
    private void Update()
    {   
        if (SessionController.Instance.autoClickerSession.level == 0)
        {
            return;
        }

        timer += Time.deltaTime;

        if (timer >= autoClickBaseIntervalInSeconds / SessionController.Instance.autoClickerSession.level)
        {
            CoinController.Instance.AddCoinAndBonus(default, new()
            {
                IsAutoTapped = true
            });

            timer = 0;
        }
    }
}