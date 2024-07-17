using UnityEngine;
public class GameplayAutoTapperController : Singleton<GameplayAutoTapperController>
{
    public int unitPrice = 50;

    [SerializeField]
    private float autoTapperBaseIntervalInSeconds = 60f;

    public void Upgrade()
    {
        var price = BootstrapSessionController.Instance.autoTapperLevel * unitPrice;
        if (BootstrapSessionController.Instance.balance < price) return;

        BootstrapSessionController.Instance.balance -= price;
        BootstrapSessionController.Instance.AutoTapperLevel += 1;
    }

    private float timer = 0;
    private void Update()
    {
        if (BootstrapSessionController.Instance.autoTapperLevel == 0)
        {
            return;
        }

        timer += Time.deltaTime;

        if (timer >= autoTapperBaseIntervalInSeconds - (BootstrapSessionController.Instance.autoTapperLevel - 1) * 0.5f)
        {
            GameplayCoinController.Instance.AddCoinAndBonusByTapping(
                GameplayCoinController.Instance.GetComponent<CircleCollider2D>().bounds.center
                );
            timer = 0;
        }
    }
}