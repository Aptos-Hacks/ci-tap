using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayBonusBarController : Singleton<GameplayBonusBarController>
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
            RenderLevel();

            progress -= 100;
        }
        BootstrapSessionController.Instance.balance += level;
        BootstrapSessionController.Instance.totalBonus += bonusGrowthRate;
    }

    [SerializeField]
    private Slider slider;

    [SerializeField]
    private TMP_Text levelText;

    private void RenderLevel()
    {
        levelText.text = $"x{level}";
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
            RenderLevel();

            progress += 100;
        }
        BootstrapSessionController.Instance.totalBonus -= _bonusDeclineRate;
    }

    private void FixedUpdate()
    {
        slider.value = progress / 100f;
    }

    private void Update()
    {
        SubtractBonus();
    }

    private void Start()
    {
        RenderLevel();
    }
}