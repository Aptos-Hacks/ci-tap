public class BootstrapStartupController : Singleton<BootstrapStartupController>
{
    private void Start()
    {
        // do initialization here
        BootstrapLoadingSceneManagerController.Instance.LoadScene(SceneName.Title);
    }
}