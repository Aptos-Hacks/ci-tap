using Mono.Cecil;
using System;
using UnityEngine;

public class BonusBarController : Singleton<BonusBarController>
{
    [SerializeField]
    private float bonusGrowthRate = 1;

    [SerializeField]
    private float bonusDeclineRate = 2f;

    public int level = 1;

    [Range(0, 100)]
    public float progress = 0;

    public void AddCoinAndBonus()
    {
        progress += bonusGrowthRate / level;
        if (progress > 100f)
        {
            level += 1;
            progress -= 100;
        }
        SessionController.Instance.balance += level;
        SessionController.Instance.totalBonus += bonusGrowthRate;
    }

    public void SubtractBonus()
    {
        var _bonusDeclineRate = bonusDeclineRate * Time.deltaTime;

        if (level == 1 && progress == 0) return;
        progress -= _bonusDeclineRate / level;
        if (progress < 0f)
        {
            if (level == 1)
            {
                progress = 0;
                return;
            }
            level -= 1;
            progress += 100;
        }
        SessionController.Instance.totalBonus -= _bonusDeclineRate;
    }

    private void Update()
    {
        SubtractBonus();
    }
}