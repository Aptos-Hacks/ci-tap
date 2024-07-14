using Michsky.MUIP;
using UnityEngine;

public class TitleUICanvasController : Singleton<TitleUICanvasController>
{
    [SerializeField]
    private ButtonManager playButton;

    private void Start()
    {
        playButton.onClick.AddListener(() => {
            BootstrapLoadingSceneManagerController.Instance.LoadScene(SceneName.Gameplay);    
        });
    }
}
