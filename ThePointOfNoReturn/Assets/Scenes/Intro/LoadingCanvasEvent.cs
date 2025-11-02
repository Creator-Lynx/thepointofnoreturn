using UnityEngine;

public class LoadingCanvasEvent : MonoBehaviour
{
    [SerializeField]
    IntroLoadingSceneScript script;
    public void CallBackIntroEnd()
    {
        script.AnimationEndCallBack();
    }
}
