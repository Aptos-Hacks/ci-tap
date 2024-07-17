using UnityEngine;

public class GameplayCoinFallContainerCanvasController : Singleton<GameplayCoinFallContainerCanvasController>
{
    [SerializeField]
    private RectTransform spawnZone;

    [SerializeField]
    private Transform coinFallPrefab;

    [SerializeField]
    private float spawnIntervalSeconds = 5f;
    private void SpawnCoinFall()
    {
        var size = spawnZone.sizeDelta;
        var randomLocalPosition = new Vector2(
            Random.Range(-size.x / 2, size.x / 2), 
            Random.Range(-size.y / 2, size.y / 2)
            );
        var coinFall = Instantiate(coinFallPrefab, spawnZone);
        coinFall.localPosition = randomLocalPosition;
    }

    private float timer = 0f;
    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnIntervalSeconds)
        {
            SpawnCoinFall();
            timer = 0;
        }
        
    }
}
