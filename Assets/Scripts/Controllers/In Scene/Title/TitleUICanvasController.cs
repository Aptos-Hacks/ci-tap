using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleUICanvasController : Singleton<TitleUICanvasController>
{
    [SerializeField]
    private Button playButton;

    private void Start()
    {
        playButton.onClick.AddListener(() => {
            BootstrapLoadingSceneManagerController.Instance.LoadScene(SceneName.Gameplay);    
        });
    }
}
