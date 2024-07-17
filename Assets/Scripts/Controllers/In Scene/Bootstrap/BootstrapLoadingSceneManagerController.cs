using System.Collections;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BootstrapLoadingSceneManagerController : SingletonPersistent<BootstrapLoadingSceneManagerController>
{
    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(OnSceneLoadedCoroutine(scene));
    }

    private IEnumerator OnSceneLoadedCoroutine(Scene scene)
    {
        yield return new WaitUntil(() => BootstrapLoadingFadeEffectCanvasController.EndFadeOut);
        Debug.Log($"Scene {scene.name} loaded.");
    }

    public void LoadScene(SceneName sceneToLoad)
    {
        StartCoroutine(LoadSceneCoroutine(sceneToLoad));
    }

    public void LoadSceneAdditive(SceneName sceneToLoad)
    {
        SceneManager.LoadScene(EnumUtility.GetDescription(sceneToLoad), LoadSceneMode.Additive);
    }

    public void UnloadSceneAdditive(SceneName sceneToLoad)
    {
       SceneManager.UnloadSceneAsync(EnumUtility.GetDescription(sceneToLoad));
    }

    private IEnumerator LoadSceneCoroutine(SceneName sceneToLoad, bool isNetworkSessionActive = false)
    {
        BootstrapLoadingFadeEffectCanvasController.Instance.FadeIn();
        yield return new WaitUntil(() => BootstrapLoadingFadeEffectCanvasController.EndFadeIn);

        SceneManager.LoadScene(EnumUtility.GetDescription(sceneToLoad));
        yield return new WaitForSeconds(0.5f);

        BootstrapLoadingFadeEffectCanvasController.Instance.FadeOut();
    }
}
public enum SceneName
{
    [Description("Bootstrap")]
    Bootstrap,
    [Description("Title")]
    Title,
    [Description("Gameplay")]
    Gameplay,
    [Description("Upgrade")]
    Upgrade
}
